
//gan script nay cho fruit de dieu chinh van toc bay

using UnityEngine;

public class MoveObject : MonoBehaviour {
    public float minXSpeed, maxXSpeed, minYSpeed, maxYSpeed;
    public Vector2 velocityobject;
    void Start () { 
        float spawnPosX = this.transform.position.x;
        //frenzy spawn
        if (spawnPosX < -5) {
            velocityobject = new Vector2(8, Random.Range(9,11));
        } else if (spawnPosX > 5) {
            velocityobject = new Vector2(-8, Random.Range(9,11));
        } 
        //normal spawn
        else {
            velocityobject = new Vector2(Random.Range(-1.2f, 1.2f), Random.Range(10,13));
        }
        this.gameObject.GetComponent<Rigidbody2D>().velocity = velocityobject;
    }
}