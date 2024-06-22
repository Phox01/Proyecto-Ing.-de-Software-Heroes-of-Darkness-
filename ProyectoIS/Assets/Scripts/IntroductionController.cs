using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{

    public GameManager gameManager;
    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.localizationController.OnLocalizationReady += OnLocalizationReady;    
        GameManager gameManager = FindObjectOfType<GameManager>();
    }

    void OnLocalizationReady()
    {
        dialogue.SetupDialogue();
        dialogue.StartDialogue(0, false);
        dialogue.StartDialogue(1, false);
        dialogue.StartDialogue(2, false);
        dialogue.StartDialogue(3, false);
        SceneManager.LoadScene(4);
        gameManager.Init();
    }
}
