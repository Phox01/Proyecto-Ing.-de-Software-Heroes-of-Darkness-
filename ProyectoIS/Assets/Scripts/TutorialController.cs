using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public Transform playerTransform;
    public Dialogue dialogue;
    private bool localizationReady = false;
    private Dictionary<int, bool> dialogueStarted; 

    void Start()
    {
        InitializeTutorial();
    }

    void InitializeTutorial()
    {
        InitializeSpawners();
        dialogueStarted = new Dictionary<int, bool>();
        dialogue.localizationController.OnLocalizationReady += OnLocalizationReady;
        dialogue.localizationController.InitializeKeys();
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
        localizationReady = true;
    }

    void Update()
    {
        if (localizationReady && Input.GetKeyDown(KeyCode.Space))
        {
            StartDialogue(2);
        }
    }

    public void StartDialogue(int dialogueIndex)
    {
        if (!dialogueStarted.ContainsKey(dialogueIndex))
        {
            dialogueStarted[dialogueIndex] = false;
        }

        if (!dialogueStarted[dialogueIndex])
        {
            if (dialogue.IsDialogueIndexValid(dialogueIndex))
            {
                dialogue.InitializeDialogue(dialogueIndex);
                dialogueStarted[dialogueIndex] = true;
            }
            else
            {
                Debug.LogError("Invalid dialogue index.");
            }
        }
    }
}
