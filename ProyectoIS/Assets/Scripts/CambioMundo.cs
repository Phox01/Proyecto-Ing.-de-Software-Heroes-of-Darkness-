using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioMundo : Colisiones
{
    public GameObject player;
    protected override void OnCollide(Collider2D col){
        if(col.tag == "Player")
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            
            if(currentSceneIndex == 4){
            //Cordenadas (-7, 16)
            SceneManager.LoadScene(6);
            player.transform.position = new Vector2(-7, 16);
            }

            if(currentSceneIndex == 6){
            //Cordenadas (-2.6, -30.8)
            SceneManager.LoadScene(4);
            player.transform.position = new Vector2(-2.6f, -30.8f); 
            }


        }
    }
}