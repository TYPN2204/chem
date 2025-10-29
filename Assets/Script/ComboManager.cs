using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ComboManager : MonoBehaviour
{
    public GameObject comboPrefab; // Prefab combo chứa 1 Text cha ("fruit combo") và 2 Text con ("+" và số combo)
    public Canvas canvas;
    public float comboWindow = 0.7f;
    private List<float> fruitSliceTimes = new List<float>();

    // Spawn combo UI tại vị trí quả bị chém
    public void AddScore(Vector3 fruitWorldPosition)
    {
        float now = Time.time;
        fruitSliceTimes.Add(now);
        fruitSliceTimes.RemoveAll(t => now - t > comboWindow);

        int currentCombo = fruitSliceTimes.Count;

        // Combo chỉ hiện khi từ 3 đến 8
        if (currentCombo >= 3)
        {
            int showCombo = Mathf.Min(currentCombo, 8);

            // Spawn prefab mới tại vị trí quả vừa chém
            GameObject comboInstance = Instantiate(comboPrefab, canvas.transform);

            // Chuyển vị trí world sang UI (Screen Space Overlay)
            Vector3 comboWorldPos = new Vector3(fruitWorldPosition.x, 0f, fruitWorldPosition.z);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(comboWorldPos);

            RectTransform rectTransform = comboInstance.GetComponent<RectTransform>();
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector2 anchoredPos;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out anchoredPos))
            {
                rectTransform.anchoredPosition = anchoredPos;
            }

            // Setup nội dung cho prefab
            // Giả sử comboPrefab có cấu trúc: Text cha ("fruit combo"), 2 Text con "+" và số combo
            Text[] texts = comboInstance.GetComponentsInChildren<Text>();
            foreach (var text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1f); // reset alpha
            }
            SoundManager.Instance.PlayCombo(showCombo); // comboCount là số combo hiện tại, tối đa 8
            // Tìm Text cha và Text con (sử dụng tên hoặc thứ tự trong prefab)
            // Ví dụ: texts[0] = "fruit combo", texts[1] = "+", texts[2] = số combo
            texts[0].text = showCombo + " fruit combo";
            texts[1].text = "+";
            texts[2].text = showCombo.ToString();

            // Animation: Punch scale, fade out rồi destroy
            rectTransform.localScale = Vector3.one;
            rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 0, 0)
                .OnComplete(() =>
                {
                    foreach (var text in texts)
                    {
                        text.DOFade(0f, 0.4f);
                    }
                    Destroy(comboInstance, 0.5f); // destroy sau khi fade
                });
        }
    }

    public void StopCombo()
    {
        fruitSliceTimes.Clear();
    }
}