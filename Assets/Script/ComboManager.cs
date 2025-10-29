using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ComboManager : MonoBehaviour
{
    public Text comboText, comboIndex;
    private int _combo;
    private float _timeRemaining =5f;
    public GameObject prefabCombo;

    private void Start()
    {
        prefabCombo.SetActive(false);
    }

    private void Update()
    {
        _timeRemaining -= Time.deltaTime;
        //neu het thoi gian dem nguoc thi reset combo va thoi gian dem nguoc 
        if (_timeRemaining <= 0)
        {
            _timeRemaining = 5f;
            _combo = 0;
        }
    }
    
    //Neu chem trung se tang combo va reset thoi gian dem nguoc
    public void AddScore()
    {
        if (_timeRemaining > 0)
        {
            DOTween.Kill("ComboScale");
            prefabCombo.SetActive(true);
            _timeRemaining = 5f;
            _combo++;
            comboText.text = _combo.ToString() + " FRUIT COMBO";
            comboIndex.text = _combo.ToString();
            comboIndex.transform.localScale = Vector3.one;
            comboIndex.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.2f, 0, 0).SetId("ComboScale");
        }
    }

    //Reset combo neu mat mang (Ham decreaseLive trong gamemanager)
    public void StopCombo()
    {
        _combo = 0;
        DOTween.Kill("ComboScale");
        prefabCombo.SetActive(false);
    }
}
