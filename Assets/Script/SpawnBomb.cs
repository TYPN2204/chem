
//gan script nay cho 1 object de spawn bonus banana/ bomb

using UnityEngine;

public class SpawnBomb : MonoBehaviour {
    public float spawnRate;
    public float objectMinX, objectMaxX, objectY;
    public CreateCuts frenzybonus;
    public GameManager gameManager;
    private GameObject _newObject;
    public GameObject bombPrefab;
    
    void Start () {
        InvokeRepeating ("SpawnObject", this.spawnRate, this.spawnRate);
    }
    public void OnDisable()
    {
        CancelInvoke("SpawnObject");
    }
    private void SpawnObject(){
        if (!frenzybonus._frenzy && !gameManager.isGameOver)
        {
            if (GameObject.FindGameObjectWithTag("Pomegranate") == null)
            { 
                _newObject = Instantiate (bombPrefab);
                SoundManager.Instance.PlayBombFuse();
                SoundManager.Instance.PlayThrowBomb();
                _newObject.transform.position = new Vector2 (Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
            }
        }
    }
}