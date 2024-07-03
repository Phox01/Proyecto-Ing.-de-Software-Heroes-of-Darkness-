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
    private bool isFacingRight = true;
    
    //public Animator animator;
    //private GameObject player;
    //public Rigidbody2D rb;
    //protected bool hasLineOfSight = false;
    //[SerializeField] public float speed;
    //[SerializeField] public LayerMask layerMask;
    // Start is called before the first frame update

    protected virtual void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        
    }

    // Update is called once per frame
    private void Update()
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

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        Vector3 ScalerLifeBar = sliderVidas.transform.localScale;
        ScalerLifeBar.x *= -1;
        sliderVidas.transform.localScale = ScalerLifeBar;
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

        // Calcular la dirección de la hilera
        Vector2 direccion = posicionPersonaje.normalized; // Normalizar la dirección para obtener la dirección unitaria

        // Crear la hilera de MuroFuego



        timerMuro += Time.deltaTime;

        if (timerMuro >= 8f)
        {
            animator.SetBool("HacerMuro", true);
            poniendoMuro = true;
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
            poniendoMuro = false;
        }
    }


    protected virtual void FixedUpdate()
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

    void Following()
    {
        
            Vector2 distan = player.transform.position - transform.position;
            float distance = distan.magnitude;

            if (distance < 10f)
            {
           

            animator.SetBool("MovingFront", false);
            Vector2 directionToPlayer = player.transform.position - transform.position;

                // Invierte la dirección para moverse en sentido contrario
                Vector2 oppositeDirection = -directionToPlayer.normalized;

                // Mueve el enemigo en la dirección contraria
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
