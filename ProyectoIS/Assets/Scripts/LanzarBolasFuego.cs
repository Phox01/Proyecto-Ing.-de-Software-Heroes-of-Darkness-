using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LanzarBolasFuego : Enemigo
{
    private float timer = 0f;
    private float timerMuro = 0f;
    private float lastMagicTime;
    public int cantidadMurosFuego;
    public GameObject FuegoPrefab;
    public BolaFuegoAzul projectilePrefab;
    public Spowner spowner;
    private bool poniendoMuro;
    public Animator animator;
    // Start is called before the first frame update
    

    // Update is called once per frame
    protected override void Update()
    {
        LanzarFuego();
        HacerMuro();
    }


    void LanzarFuego()
    {
        lastMagicTime = Time.time;
        rb.velocity = Vector2.zero;
        //animator.SetTrigger("Magic");
        //musicManagement.SeleccionAudio(animator.GetInteger("NumbAtt") - 1, 1f)



        timer += Time.deltaTime;

        if (timer >= 5f )
        {


            BolaFuegoAzul bolaFuegoAzul = Instantiate(projectilePrefab, transform.position, transform.rotation);



            timer = 0f;
        }
        
        
    }

    public void HacerMuro()
    {
        
        Vector2 posicionPersonaje = player.transform.position;

        // Calcular la dirección de la hilera
        Vector2 direccion = posicionPersonaje.normalized; // Normalizar la dirección para obtener la dirección unitaria

        // Crear la hilera de MuroFuego



        timerMuro += Time.deltaTime;

        if (timerMuro >= 8f)
        {
            animator.SetBool("HacerMuro", true);
            poniendoMuro = true;
            Vector2 posicionMuroFuego = (player.transform.position - transform.position).normalized;
            Debug.Log(transform.position);

            Spowner donde = Instantiate(spowner, transform.position, Quaternion.identity);
            


            timerMuro = 0f;


            

        }
        cambiarAnimacion();

    }
    
    private void cambiarAnimacion()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HacerMuro"))
        {
            
            animator.SetBool("HacerMuro", false);
        }
    }
    

}
