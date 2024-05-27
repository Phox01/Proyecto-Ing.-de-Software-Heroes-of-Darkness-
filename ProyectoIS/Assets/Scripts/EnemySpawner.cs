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

    public Transform playerTransform; 
    public float minSpawnDistanceFromPlayer = 5f; 

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
        Vector3 spawnPosition;
        do
        {
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            spawnPosition = new Vector3(randomX, randomY, transform.position.z) + transform.position;
        } while (Vector3.Distance(spawnPosition, playerTransform.position) < minSpawnDistanceFromPlayer);

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

    public void Initialize()
    {
        if (enemies.Length > 0)
        {
            spawnedNumber = new int[enemies.Length];
            SpawnInitialEnemies();
        }
        isInitialized = true; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 bottomLeft = new Vector3(spawnAreaMin.x, spawnAreaMin.y, 0) + transform.position;
        Vector3 topLeft = new Vector3(spawnAreaMin.x, spawnAreaMax.y, 0) + transform.position;
        Vector3 topRight = new Vector3(spawnAreaMax.x, spawnAreaMax.y, 0) + transform.position;
        Vector3 bottomRight = new Vector3(spawnAreaMax.x, spawnAreaMin.y, 0) + transform.position;

        Gizmos.DrawLine(bottomLeft, topLeft);
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
    }
}
