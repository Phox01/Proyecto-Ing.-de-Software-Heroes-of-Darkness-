using UnityEngine;

[System.Serializable]
public class EnemyConfig
{
    public GameObject enemyPrefab;
    public int initialCount;
    public int totalCount;
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyConfig[] enemies;
    private float spawnRate = 2f;
    private float timer = 0f;

    public Vector2 spawnAreaMin;
    public Vector2 spawnAreaMax;

    private int[] spawnedNumber;
    private bool isInitialized = false;

    void Start()
    {
    }

    void Update()
    {
        if (!isInitialized)
            return;
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
            for (int i = 0; i < enemies.Length; i++)
            {
                if (spawnedNumber[i] < enemies[i].totalCount)
                {
                    SpawnEnemy(i);
                    spawnedNumber[i]++;
                    break;
                }
            }
        }
    }

    void SpawnEnemy(int enemyIndex)
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        Vector3 spawnPosition = new Vector3(randomX, randomY, transform.position.z);

        Instantiate(enemies[enemyIndex].enemyPrefab, spawnPosition, transform.rotation);
    }
    void SpawnInitialEnemies()
    {
        spawnedNumber = new int[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            for (int j = 0; j < enemies[i].initialCount; j++)
            {
                SpawnEnemy(i);
                spawnedNumber[i]++;
            }
        }
    }
    
    public void InitializeEnemies(EnemyConfig[] configs)
    {
        enemies = configs;
        isInitialized = true;
        SpawnInitialEnemies();
    }
}
