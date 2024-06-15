using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroductionController : MonoBehaviour
{

    public Dialogue dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.localizationController.OnLocalizationReady += OnLocalizationReady;
    }

    void OnLocalizationReady()
    {
        dialogue.SetupDialogue();
        dialogue.StartDialogue(0, false);
        dialogue.StartDialogue(1, false);
        dialogue.StartDialogue(2, false);
        dialogue.StartDialogue(3, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
