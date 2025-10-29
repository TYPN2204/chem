
//gan script nay cho 1 object de spawn bonus banana/ bomb

using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class SpawnBonusBanana : MonoBehaviour {
    //public GameObject[] prefab;
    public float spawnRate;
    public float objectMinX, objectMaxX, objectY;
    public CreateCuts frenzybonus;
    public GameManager gameManager;
    private GameObject _newObject;
    private List<int> _list;

    
    public GameObject bananaFreeze, bananaDouble, bananaFrenzy;
    [Range(0, 100)]
    public int bananaFreezeDropRate, bananaDoubleDropRate, bananaFrenzyDropRate;
    // public int randomNumber;
    
    void Start () {
        InvokeRepeating ("SpawnObject", this.spawnRate, this.spawnRate);
        _list = new List<int>();

        for (int i = 0; i < bananaDoubleDropRate; i++)
        {
            _list.Add(1); //double banana
        }                 
        for (int i = 0; i < bananaFreezeDropRate; i++)
        {
            _list.Add(2); //freeze banana
        }                 
        for (int i = 0; i < bananaFrenzyDropRate; i++)
        {
            _list.Add(3); //frenzy banana
        } 
    }
    public void OnDisable()
    {
        CancelInvoke("SpawnObject");
    }
    private void SpawnObject(){
        SoundManager.Instance.PlayPowerupDeflect();
        if (!frenzybonus._frenzy && !gameManager.isGameOver)
        {
            if (GameObject.FindGameObjectWithTag("Pomegranate") == null)
            {
                int randomBanana = _list[Random.Range(0, _list.Count)];
                switch (randomBanana)
                {
                    case 1:
                        _newObject = Instantiate(bananaDouble);
                        break;                    
                    case 2:
                        _newObject = Instantiate(bananaFreeze);
                        break;                    
                    case 3:
                        _newObject = Instantiate(bananaFrenzy);
                        break;
                }

                _newObject.transform.position = new Vector2 (Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
                
                // if (_newObject == bananaDouble)
                // {
                //     Debug.Log("banana Double spawn");
                // }
                // else if (_newObject == bananaFreeze)
                // {
                //     Debug.Log("banana Freeze spawn");
                // }
                // else if (_newObject == bananaFrenzy)
                // {
                //     Debug.Log("banana Frenzy spawn");
                // }
            }
        }
    }
}