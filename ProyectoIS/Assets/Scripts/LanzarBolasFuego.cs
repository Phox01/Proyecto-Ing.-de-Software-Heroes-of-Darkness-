using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LanzarBolasFuego : Enemigo
{
    private float timer = 0f;
    private float timerMuro = 0f;
    public int cantidadMurosFuego;
    public GameObject FuegoPrefab;
    public BolaFuegoAzul projectilePrefab;
    public Spowner spowner;
    private bool isFacingRight = true;


    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (player.transform.position.x < transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x > transform.position.x && !isFacingRight)
        {
            Flip();
        }

        if (hasLineOfSight && !animator.GetBool("Muerte"))
        {
            Following();
        }

        LanzarFuego();
        HacerMuro();
    }

    void LanzarFuego()
    {
        rb.velocity = Vector2.zero;
        //animator.SetTrigger("Magic");
        //musicManagement.SeleccionAudio(animator.GetInteger("NumbAtt") - 1, 1f)



        timer += Time.deltaTime;

        if (timer >= 5f)
        {
            BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

            // Desactivar el BoxCollider2D
            boxCollider.enabled = false;


            BolaFuegoAzul bolaFuegoAzul = Instantiate(projectilePrefab, transform.position, transform.rotation);



            timer = 0f;
            boxCollider.enabled = true;
        }


    }

    public void HacerMuro()
    {

        Vector2 posicionPersonaje = player.transform.position;

        // Calcular la direcci�n de la hilera
        Vector2 direccion = posicionPersonaje.normalized; // Normalizar la direcci�n para obtener la direcci�n unitaria

        // Crear la hilera de MuroFuego



        timerMuro += Time.deltaTime;

        if (timerMuro >= 8f)
        {
            animator.SetBool("HacerMuro", true);
            Vector2 posicionMuroFuego = (player.transform.position - transform.position).normalized;


            Spowner donde = Instantiate(spowner, transform.position, Quaternion.identity);



            timerMuro = 0f;




        }
        cambiarAnimacion();

    }

    private void cambiarAnimacion()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HacerMuro"))
        {
            Debug.Log("termino de hacer muro");
            animator.SetBool("HacerMuro", false);
        }
    }


    protected override void FixedUpdate()
    {
        if (player != null)
        {

            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, ~layerMask);

            if (ray.collider != null)
            {

                hasLineOfSight = ray.collider.CompareTag("Player");
                if (hasLineOfSight)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                }
                else
                {

                    //Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);

                }
            }
        }
    }

    protected override void Following()
    {

        Vector2 distan = player.transform.position - transform.position;
        float distance = distan.magnitude;

        if (distance < 10f)
        {


            animator.SetBool("MovingFront", false);
            Vector2 directionToPlayer = player.transform.position - transform.position;

            // Invierte la direcci�n para moverse en sentido contrario
            Vector2 oppositeDirection = -directionToPlayer.normalized;

            // Mueve el enemigo en la direcci�n contraria
            transform.Translate(oppositeDirection * speed * Time.deltaTime);
        }
        else if (distance > 11f)
        {
            animator.SetBool("MovingFront", true);

            //animator.SetBool("MovingBack", false);
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        }



        //animator.SetBool("Attack", true);




    }

}
