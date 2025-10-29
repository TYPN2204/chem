using UnityEngine;
using DG.Tweening;
public class CircleFruitHome : MonoBehaviour
{
    void Start()
    {
        this.transform.DOScale(new Vector3(1.8309f, 1.8309f, 1.8309f), 4f).SetEase(Ease.InOutExpo).SetUpdate(true);
        this.transform.DORotate(new Vector3(0,0,180), 3).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetUpdate(true);
    }
}