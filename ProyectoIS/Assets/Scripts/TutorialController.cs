using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public Transform playerTransform;
    public Dialogue dialogue;
    public Minotaurus minotaurus;
    private bool wPressed = false;
    private bool aPressed = false;
    private bool sPressed = false;
    private bool dPressed = false;
    private bool lShiftPressed = false;
    private bool spacePressed = false;
    private bool dialogue1 = false;
    private bool dialogue2 = false;
    private bool dialogue3 = false;
    private bool dialogue4 = false;
    private bool bats = false;
    private bool minotaurusOn = false;
    private bool minotaurusKilled = false;
    private List<Enemigo> activeEnemies = new List<Enemigo>();

    void Start()
    {
        InitializeTutorial();
    }

    void InitializeTutorial()
    {
        dialogue.localizationController.OnLocalizationReady += OnLocalizationReady;
    }

    void InitializeSpawners()
    {
        foreach (var spawner in enemySpawners)
        {
            spawner.playerTransform = playerTransform; 
            spawner.OnEnemySpawned += HandleEnemySpawned;
            spawner.Initialize();
        }
    }

    void OnLocalizationReady()
    {
        dialogue.SetupDialogue();
        dialogue.StartDialogue(0,false);
    }

    void Update()
    {
        CheckKeyPresses();
        if(dialogue3 && !bats){
        InitializeSpawners();
        bats = true;
        }
        if(dialogue4 && !minotaurusOn){
        minotaurus.gameObject.SetActive(true);
        minotaurus.OnEnemyKilled += HandleEnemyKilled;
        minotaurusOn = true;
        Debug.Log("Minotaurus activated.");
        }
        Debug.Log("Number of active enemies: " + activeEnemies.Count);
    }

    void CheckKeyPresses()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            wPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            aPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            sPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && dialogue1)
        {
            lShiftPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && dialogue2)
        {
            spacePressed = true;
        }

        if (wPressed && aPressed && sPressed && dPressed)
        {
            dialogue.StartDialogue(1, false);;
            dialogue1 = true;
        }
        if (lShiftPressed)
        {
            StartCoroutine(ActivateDashDialogue());
            dialogue2 = true;
        }
        if (lShiftPressed)
        {
            StartCoroutine(ActivateDashDialogue());
            dialogue2 = true;
        }
        if (spacePressed)
        {
            StartCoroutine(ActivateAttackDialogue());
            dialogue3 = true;
        }
    }

    IEnumerator ActivateDashDialogue()
    {
        yield return new WaitForSeconds(1); 
            dialogue.StartDialogue(2, false);
    }
    IEnumerator ActivateAttackDialogue()
    {
        yield return new WaitForSeconds(1); 
            dialogue.StartDialogue(3, false);
    }
    IEnumerator ActivateFinalDialogue()
    {
        yield return new WaitForSeconds(8); 
            
            SceneManager.LoadScene(2);
    }

    public void HandleEnemySpawned(Enemigo enemy)
    {
        activeEnemies.Add(enemy);
        enemy.OnEnemyKilled += HandleEnemyKilled;
    }

    void HandleEnemyKilled(Enemigo enemy)
    {
        activeEnemies.Remove(enemy);
        if (enemy == minotaurus)
        {
            minotaurusKilled = true;
            Debug.Log("Minotaurus killed.");
        }
        if (activeEnemies.Count == 0 && !dialogue4)
        {
            dialogue.StartDialogue(4, false);
            dialogue4 = true;
        }
        if (minotaurusKilled)
        {
            
            dialogue.StartDialogue(5, false);
           StartCoroutine(ActivateFinalDialogue());
        }
        
    }
}

