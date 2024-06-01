using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{
    public int vidaMax;
    int vida;
    public int maxAttack;
    int attack;
    public int defensaMax;
    int defensa;
    int netDamage;
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;
    private MusicManagement musicManagement;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerMask;
    private GameObject player;
    public GameObject Character;
    private bool hasLineOfSight = false;
    private bool isFacingRight = true; // Assume the enemy is facing right initially
    public Animator animator;
    private Flash flash;




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        musicManagement = FindObjectOfType<MusicManagement>();
        flash = GetComponent<Flash>();
    }

    void Start(){
        
        vida = vidaMax;
        attack = maxAttack;
        defensa = defensaMax;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        //LÓGICA PARA QUE EL ENEMIGO SIEMPRE MIRE AL PERSONAJE PRINCIPAL
        if (player.transform.position.x < transform.position.x && isFacingRight)
    {
        Flip();
    }
    else if (player.transform.position.x > transform.position.x &&!isFacingRight)
    {
        Flip();
    }
        if (hasLineOfSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

void Flip()
{
    isFacingRight =!isFacingRight;
    Vector3 Scaler = transform.localScale;
    Scaler.x *= -1;
    transform.localScale = Scaler;
}

    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, ~layerMask);
        if (ray.collider != null)
        {
            hasLineOfSight = ray.collider.CompareTag("Player");
            if (hasLineOfSight)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")==false){
                    Ataque();

                }
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
        }
    }

    void Ataque(){
        animator.SetBool("Attack", true);
    }

    
    public void GetDamaged(int damage){
        GetKnockedBackUwu(playerMovement.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
        
        

        netDamage = damage-defensa;
        if(netDamage>0){
            vida -= netDamage;
            animator.SetInteger("life", vida);
            animator.SetBool("Attack", false);
            Debug.Log(vida );
            }
        if(vida<=0){

            if (animator.GetInteger("life")<=0)
            {
                StartCoroutine(OnDieAnimationComplete());

                
            }
            musicManagement.SeleccionAudio(4, 1f);
            Destroy(gameObject);
            // SceneManager.LoadScene(2);
        }
    }

    IEnumerator OnDieAnimationComplete(){
        yield return new
        WaitForSeconds(2);
        Destroy(gameObject);
        }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hola");
            ControladorDeAtaque jugador = other.GetComponent<ControladorDeAtaque>();
            if (jugador != null)
            {
                jugador.GetDamaged(attack);
                Debug.Log("damge doned");
            }
        }
    }


    public void GetKnockedBackUwu(Transform damageSource, float knockBackThrust)
   
    {
        
        gettingKnockedBack = true;
        
        Vector2 diference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(diference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
        if (jugador != null)
        {
            jugador.GetDamaged(attack);
        }
    }
}
