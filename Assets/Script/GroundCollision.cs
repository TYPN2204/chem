
//Gan script nay cho ground, khi va cham voi tag "fruit" se tru 1 mang

using UnityEngine;
public class GroundCollision : MonoBehaviour
{
    //public Image loseFruitPoint;
    public GameManager gameManager;
    public CreateCuts frenzybonus;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (frenzybonus._frenzyreset == false)
        {
            if (gameManager.gamemode != GameManager.GameMode.Modetime && !gameManager.isGameOver)
            {
                if (collision.gameObject.CompareTag("Fruit"))
                {
                    FindObjectOfType<GameManager>().DecreaseLive(-1);
                }
            }
            else if (gameManager.gamemode == GameManager.GameMode.Modetime)
            {
                if (collision.gameObject.CompareTag("Pomegranate"))
                {
                    FindObjectOfType<GameManager>().GameOver();
                }
            }
        }
            // //tao vector3 chua toa do cua vat va cham, dat y=-4 roi gan toa do cho loseFruitPoint
            // Vector3 newPosition = collision.transform.position;
            // if (newPosition.x < -8.8)
            // {
            //     loseFruitPoint.rectTransform.anchoredPosition.x = -880;
            //     loseFruitPoint.rectTransform.anchoredPosition.y = -480;
            //
            // }
            // if(OnBananaBonus)
            //     return;
        
    }
}