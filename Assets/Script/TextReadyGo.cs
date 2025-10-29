using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextReadyGo : MonoBehaviour
{
    public Transform textReadyGoPrefab, textParent;
    private Text text;
    public GameObject textReadyBottom, textReadyMiddle, textReadyUp;

    public void SetTextReady(string getText)
    {
        Transform textObject = Instantiate(textReadyGoPrefab, textReadyBottom.transform.position, Quaternion.identity);
        textObject.transform.SetParent(textParent);

        text = textObject.GetComponent<Text>();
        text.text = getText;

        if (getText == "Go!!!")
        {
            text.color = Color.green;
        }

        StartCoroutine(TextRun());
    }

    IEnumerator TextRun()
    {
        text.transform.DOMove(textReadyMiddle.transform.position, 1).SetEase(Ease.OutExpo).SetUpdate(true);
        text.DOFade(1, 1).SetEase(Ease.OutExpo).SetUpdate(true);

        yield return new WaitForSecondsRealtime(1);

        text.transform.DOMove(textReadyUp.transform.position, 1).SetEase(Ease.OutExpo).SetUpdate(true).SetDelay(2);
        text.DOFade(0, 1).SetEase(Ease.OutExpo).SetUpdate(true);
    }
}