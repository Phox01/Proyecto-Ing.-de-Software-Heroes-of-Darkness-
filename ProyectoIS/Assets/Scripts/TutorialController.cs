using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public Transform playerTransform; 
    void Start()
    {
        InitializeTutorial();
    }

    void InitializeTutorial()
    {
        StartDialogue();
        InitializeSpawners();
    }

    void StartDialogue()
    {
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
