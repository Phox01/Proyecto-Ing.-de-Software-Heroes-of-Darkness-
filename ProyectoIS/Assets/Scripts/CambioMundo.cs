using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioMundo : Colisiones
{
    public GameObject player;
    public int TargetScene;
    public GameObject punto;
    private void Awake(){
        
    }
    protected override void OnCollide(Collider2D col){
        if(col.tag == "Player")
        {
            playerMovement playercomp = col.gameObject.GetComponent<playerMovement>();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            playercomp.ChangeScene(currentSceneIndex);
            SceneManager.LoadScene(TargetScene);
            
        }
    }
}