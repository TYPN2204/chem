
//gan script nay cho object chua 2 manh hoa qua, de manh hoa qua ban theo huong khac nhau 


using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FruitInHalf : MonoBehaviour
{
    public List<Rigidbody2D> pieces;
    public GameObject center;
    void Start()
    {
        InHalf();
    }

    void InHalf()
    {
        foreach (var piece in pieces)
        {
            Vector3 direction = -center.transform.position + piece.transform.position;
            Vector3.Normalize(direction);
            piece.AddForce(direction* 100f, ForceMode2D.Impulse);
            piece.transform.DORotate(new Vector3(Random.Range(0,360), Random.Range(0,360), Random.Range(0,360)), 2);
        }
    }
}
