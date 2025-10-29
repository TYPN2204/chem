using UnityEngine;
using DG.Tweening;
using System.Collections;

public class SpawnObjects : MonoBehaviour 
{
    public GameObject[] prefab;
    public float spawnRate;
    public GameManager gameManager;
    public CreateCuts frenzybonus;
    private float _previousSpawnRate;
    private Vector2[] spawnPositions = new Vector2[]
    {
        new Vector2(-4, -4.9f),
        new Vector2(-2, -4.9f),
        new Vector2(0, -4.9f),
        new Vector2(2, -4.9f),
        new Vector2(4, -4.9f)
    };

    public void OnFrenzy() 
    {
        _previousSpawnRate = spawnRate;
        InvokeRepeating("SpawnObject", spawnRate, spawnRate);
    }

    void Update()
    {   
        // So sánh nếu spawnRate thay đổi
        if (Mathf.Abs(spawnRate - _previousSpawnRate) > Mathf.Epsilon)
        {
            // Dừng invoke với spawnRate cũ và invoke với spawnRate hiện tại
            CancelInvoke("SpawnObject");
            InvokeRepeating("SpawnObject", spawnRate, spawnRate);
            _previousSpawnRate = spawnRate;
        }
    }

    private void SpawnObject()
    {
        if (!frenzybonus._frenzy && !gameManager.isGameOver)
        {
            int i = Random.Range(1, 6);
            StartCoroutine(SpawnObjectsCoroutine(i));
        }
    }

    private IEnumerator SpawnObjectsCoroutine(int count)
    {
        int k = Random.Range(1, 3);
        for (int j = 0; j < count; j++)
        {
            //Bắn qua thu i
            GameObject newObject = Instantiate(prefab[Random.Range(0, prefab.Length)]);
            newObject.transform.position = spawnPositions[j % spawnPositions.Length];
            newObject.transform.DORotate(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), 2, RotateMode.FastBeyond360);
            if (k == 1)
            {
                yield return new WaitForSeconds(0.8f); // Delay moi lan spawn          //
            }
            else yield return new WaitForSeconds(0.1f);
        }
    }
}