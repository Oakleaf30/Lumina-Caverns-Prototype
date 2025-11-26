using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject slime;
    public GameObject bat;

    public int enemyCount = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        GameObject enemy = RockSpawner.Instance.level <= 10 ? slime : bat;
        Vector3 spawnPos = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 3f), 1);
        Instantiate(enemy, spawnPos, Quaternion.identity);
    }
}
