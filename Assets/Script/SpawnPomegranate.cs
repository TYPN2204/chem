//gan script nay cho 1 object de spawn qua luu

using System;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
public class SpawnPomegranate : MonoBehaviour {
    public GameObject[] prefab;
    public float spawnRate;
    public float objectMinX, objectMaxX, objectY;
    public CreateCuts frenzybonus;
    public GameManager gameManager;
    void Start () {
        InvokeRepeating ("SpawnObject", this.spawnRate, this.spawnRate);
    }

    public void OnDisable()
    {
        CancelInvoke("SpawnObject");
    }
    private void SpawnObject(){
        if (frenzybonus._frenzy == false)
        {
            if (gameManager.isGameOver == false)
            {
                if (GameObject.FindGameObjectWithTag("Bomb") == null) 
                {
                    GameObject newObject = Instantiate (this.prefab[Random.Range(0, prefab.Length)]);
                    newObject.transform.position = new Vector2 (Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
                    newObject.transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 2, RotateMode.FastBeyond360);
                }
            }
        }
    }
}