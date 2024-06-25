using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class Puerta : MonoBehaviour
{

    public int TargetScene;

    public TMP_Text texto;
    [SerializeField]
    private bool Chocando=false;
    public playerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IrAmundo();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null)
        {
            texto.gameObject.SetActive(true);

            Chocando = true;
            
            
        }
       
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        
            
            texto.gameObject.SetActive(false);
        Chocando = false;
    }


    private void IrAmundo()
    {
        if (Chocando==true)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                texto.gameObject.SetActive(false);
                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                player.ChangeScene(currentSceneIndex);
                SceneManager.LoadScene(TargetScene);
                DataJuego.data.dinero += 100;
                Debug.Log(DataJuego.data.dinero);

            }
        }
        
    }
    
   
}
