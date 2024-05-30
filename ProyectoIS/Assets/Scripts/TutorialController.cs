using System.Collections;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public Transform playerTransform;
    public Dialogue dialogue;

    void Start()
    {
        InitializeTutorial();
    }

    void InitializeTutorial()
    {
        InitializeSpawners();
    }

    void InitializeSpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.playerTransform = playerTransform; 
            spawner.Initialize();
        }
    }
}
