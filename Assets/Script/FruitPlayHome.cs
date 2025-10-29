using UnityEngine;
using DG.Tweening;
public class FruitPlayHome : MonoBehaviour
{
    void Start()
    {
        this.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), 4f).SetEase(Ease.InOutExpo).SetUpdate(true);
        this.transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 3).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetUpdate(true);
    }
}
