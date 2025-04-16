using System.Collections;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private bool direction = false;
    private float random;
    [SerializeField] private GameObject enemyObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (direction)
            {
                direction = false;
                random = Random.Range(-3f, 3f);
                Instantiate(enemyObject, new Vector3(-8f, random, random), new Quaternion(0, 0, 0, 0));
            }
            else
            {
                direction = true;
                random = Random.Range(-3f, 3f);
                Instantiate(enemyObject, new Vector3(8f, random, random), new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
