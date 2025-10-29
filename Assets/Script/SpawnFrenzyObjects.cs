//gan script nay cho 1 object de spawn frenzy fruit

using UnityEngine;
using DG.Tweening;

public class SpawnFrenzyObjects : MonoBehaviour 
{
    public GameObject[] prefab;
    public float spawnRate;
    public float objectMinX, objectMaxX, objectY;
    private float _previousSpawnRate;
    public CreateCuts frenzybonus;
    
    void Update()
    {   //so sanh neu spawnrate thay doi 
        if (Mathf.Abs(spawnRate - _previousSpawnRate) > Mathf.Epsilon)
        {
            //dung invoke voi spawnrate cu va invoke voi spawnrate hien tai
            CancelInvoke("SpawnObject");
            InvokeRepeating("SpawnObject", spawnRate, spawnRate);
            _previousSpawnRate = spawnRate;
        }
    }
    private void SpawnObject()
    {
        if (frenzybonus._frenzy)
        {
            GameObject newObject = Instantiate(this.prefab[Random.Range(0, prefab.Length)]);
            newObject.transform.position = new Vector2(Random.Range(this.objectMinX, this.objectMaxX), this.objectY);
            newObject.transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 2, RotateMode.FastBeyond360);
        }
    }
}