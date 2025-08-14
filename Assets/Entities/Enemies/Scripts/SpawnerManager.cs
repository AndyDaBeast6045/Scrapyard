using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private bool direction = false;
    private float random;
    private int enemiesSpawned;
    private float spawnSpeed;
    [SerializeField] private float fastRatio;
    [SerializeField] private float spawnScaling;
    [SerializeField] private GameObject enemyObject;
    [SerializeField] private float startingSpawnSpeed;
    [SerializeField] private bool endless;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnSpeed = startingSpawnSpeed;
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (enemiesSpawned <= 30)
        {
            yield return new WaitForSeconds(spawnSpeed);
            SpawnEnemy();
            enemiesSpawned++;
            if (enemiesSpawned == 15)
            {
                spawnSpeed -= spawnScaling;
            }
        }
        while (endless)
        {
            yield return new WaitForSeconds(spawnSpeed);
            SpawnEnemy();
            enemiesSpawned++;
            if (enemiesSpawned == 31)
            {
                spawnSpeed -= spawnScaling;
                enemiesSpawned = 0;
            }
        }

    }
    public float GetRatio()
    {
        return fastRatio;
    }

    public bool GetEndless()
    {
        return endless;
    }
    public int GetSpawned()
    {
        return enemiesSpawned;
    }
    public void SpawnEnemy()
    {
        if (direction)
        {
            direction = false;
            random = Random.Range(-3.5f, 0.3f);
            Instantiate(enemyObject, new Vector3(-8f, random, random), new Quaternion(0, 0, 0, 0));
        }
        else
        {
            direction = true;
            random = Random.Range(-3.5f, 0.3f);
            Instantiate(enemyObject, new Vector3(8f, random, random), new Quaternion(0, 0, 0, 0));
        }
    }
}
