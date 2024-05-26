using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public GameObject enemyPrefab1; // Asigna estos en el Inspector

    void Start()
    {
        InitializeTutorial();
    }

    void InitializeTutorial()
    {
        // Lógica específica para la escena del tutorial
        StartDialogue();
        SpawnInitialEnemies();
    }

    void StartDialogue()
    {
        // Implementar la lógica del diálogo aquí
        // dialogueManager.StartDialogue(someDialogue); // Ajusta según tu implementación de diálogos
    }

    void SpawnInitialEnemies()
    {
        EnemyConfig[] tutorialEnemies = new EnemyConfig[]
        {
            new EnemyConfig() { enemyPrefab = enemyPrefab1, initialCount = 3, totalCount = 10 },
        };

        enemySpawner.InitializeEnemies(tutorialEnemies);
    }
}
