using System.Collections;
using System.Collections.Generic;
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
        dialogue.localizationController.OnLocalizationReady += OnLocalizationReady;
    }

    void InitializeSpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.playerTransform = playerTransform; 
            spawner.Initialize();
        }
    }

    void OnLocalizationReady()
    {
        dialogue.SetupDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            dialogue.StartDialogue(0, false); // Ejemplo para iniciar diálogo no repetible 
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            dialogue.StartDialogue(1, true); // Ejemplo para iniciar diálogo repetible 
        }
    }
}
