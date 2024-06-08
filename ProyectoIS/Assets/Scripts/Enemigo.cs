using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    public int vidaMax;
    protected int vida;
    public int maxAttack;
    public int attack;
    public int defensaMax;
    protected int defensa;
    protected int netDamage;
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;
    private MusicManagement musicManagement;
    [SerializeField] protected float speed;
    [SerializeField] protected LayerMask layerMask;
    protected GameObject player;
    public GameObject Character;
    public Slider sliderVidas;
    protected bool hasLineOfSight = false;
    private bool isFacingRight = true; // Assume the enemy is facing right initially
    public Animator animator;
    protected Flash flash;
    public delegate void EnemyKilledHandler(Enemigo enemy);
    public event EnemyKilledHandler OnEnemyKilled;

    private Color fullHealthColor = Color.green;
    private Color midHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        musicManagement = FindObjectOfType<MusicManagement>();
        flash = GetComponent<Flash>();
    }

    protected virtual void Start(){
        
        vida = vidaMax;
        attack = maxAttack;
        defensa = defensaMax;
        player = GameObject.FindGameObjectWithTag("Player");
        sliderVidas.value = vida;
    }
    protected virtual void Update()
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
        if (hasLineOfSight && !animator.GetBool("Death"))
            {
                //Muévete
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                Ataque();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        if(vida<=0){

            animator.SetBool("Death", true);
            StartCoroutine(OnDieAnimationComplete());
            }
        }

    void Flip()
    {
        isFacingRight =!isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        Vector3 ScalerLifeBar=sliderVidas.transform.localScale;
        ScalerLifeBar.x*=-1;
        sliderVidas.transform.localScale= ScalerLifeBar;
        
    }

    protected virtual void FixedUpdate()
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
                if (!animator.GetBool("Death"))
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);    
                }
            }
        }
    }

    void Ataque(){
        animator.SetBool("Attack", true);
    }

    
    public virtual void GetDamaged(int damage){
        GetKnockedBackUwu(playerMovement.Instance.transform, 15f);
        musicManagement.SeleccionAudio(4, 1f);
        StartCoroutine(flash.FlashRoutine());
        
        

        netDamage = damage-defensa;
        if(netDamage>0){
            vida -= netDamage;
            animator.SetInteger("life", vida);
            Debug.Log(vida );
            sliderVidas.value = vida;
            UpdateHealthColor();
            animator.SetBool("Attack", false);
            }
    }

    protected virtual  IEnumerator OnDieAnimationComplete(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
         Die();
    }

    protected void Die()
    {
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled(this);
        }
        Destroy(gameObject);
    }

    private void UpdateHealthColor()
    {
        if (vida > vidaMax*0.5 )
        {
            sliderVidas.fillRect.GetComponent<Image>().color = fullHealthColor;
        }
        else if (vida > vidaMax*0.25)
        {
            sliderVidas.fillRect.GetComponent<Image>().color = midHealthColor;
        }
        else
        {
            sliderVidas.fillRect.GetComponent<Image>().color = lowHealthColor;
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

    protected virtual void OnCollisionEnter2D(Collision2D collision) //Probando, para que Minotauro no herede onCollisionEnter2D()
    {
        ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
        if (jugador != null)
        {
            jugador.GetDamaged(attack);
        }
    }
}
