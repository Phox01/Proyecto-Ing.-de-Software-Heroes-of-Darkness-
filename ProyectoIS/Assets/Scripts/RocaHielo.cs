using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaHielo : MonoBehaviour
{
    // Start is called before the first frame update

    private BoxCollider2D boxCollider;
    private float timer = 0f;
    public float destroyTime = 1f; // Tiempo de destrucción (en segundos)

   
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        Invoke("Cambia", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void PilarHielo()
    {
        Debug.Log("Ahora es un pilar");
        boxCollider.enabled = true;
        Invoke("DestroyObject", 3f);
    }


    private void Cambia()
    {
        Debug.Log("PilarHielo");
        Invoke("PilarHielo", 1f);
    }

    private void DestroyObject()
    {


        // Destruye el objeto
        Debug.Log("destruye");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) //Probando, para que Minotauro no herede onCollisionEnter2D()
    {

        ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
        if (jugador != null)
        {
            jugador.GetDamaged(10);
        }
    }
}
