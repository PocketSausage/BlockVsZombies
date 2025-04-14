using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnRate = 50f; // 每秒生成数量
    public Vector2 spawnXRange = new Vector2(-5f, 5f);
    public Vector2 spawnZRange = new Vector2(-5f, 0f);
    public float spawnY = 0.5f;

    void Start()
    {
        StartCoroutine(SpawnZombiesContinuously());
    }

    IEnumerator SpawnZombiesContinuously()
    {
        float interval = 1f / spawnRate;

        while (true)
        {
            Vector3 pos = new Vector3(
                Random.Range(spawnXRange.x, spawnXRange.y),
                spawnY,
                Random.Range(spawnZRange.x, spawnZRange.y)
            );

            Instantiate(zombiePrefab, pos, Quaternion.identity);
            yield return new WaitForSeconds(interval);
        }
    }
    

}