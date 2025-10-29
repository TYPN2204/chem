
//gan script nay cho effect (bom) de destroy hieu ung sau 1 giay

using UnityEngine;

public class InBomb : MonoBehaviour
{
    public GameObject effect;
    void Start()
    {
        //dung hieu ung sau 1 giay
        Destroy(effect,1);
    }

}
