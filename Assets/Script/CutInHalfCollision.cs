
//gan script nay cho hoa qua de destroy khi va cham voi "ground" (tag)

using UnityEngine;

public class CutInHalfCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
