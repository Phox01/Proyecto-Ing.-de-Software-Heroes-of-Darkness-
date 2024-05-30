using System.Collections;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public Transform playerTransform;
    public Dialogue dialogue;
    public DialogueData[] dialogues; // Array of DialogueData to assign from the inspector

    void Start()
    {
        InitializeTutorial();
    }

    void InitializeTutorial()
    {
        string[][] dialoguesKeys = new string[dialogues.Length][];
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialoguesKeys[i] = dialogues[i].localizationKeys;
        }
        dialogue.localizationKeys = dialoguesKeys;
        dialogue.Start(); // Initialize the dialogue
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
