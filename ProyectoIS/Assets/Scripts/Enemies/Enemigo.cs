using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Enemigo : MonoBehaviour
{
    public int vidaMax;
    public int vida;
    public int maxAttack;
    public int attack;
    public int defensaMax;
    protected int defensa;
    protected int netDamage;
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    public Rigidbody2D rb;
    protected MusicManagement musicManagement;
    [SerializeField] protected float speed;
    [SerializeField] protected LayerMask layerMask;
    protected GameObject player;
    public Slider sliderVidas;
    protected bool hasLineOfSight = false;
    private bool isFacingRight = true;
    public Animator animator;
    protected Flash flash;
    public delegate void EnemyKilledHandler(Enemigo enemy);
    public event EnemyKilledHandler OnEnemyKilled;

    private Color fullHealthColor = Color.green;
    private Color midHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;
    public GameObject damageTextPrefab; // Prefab del texto de daño
    protected LootDropper lootDropper;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        flash = GetComponent<Flash>();
        lootDropper = GetComponent<LootDropper>();
    }

    protected virtual void Start()
    {
        vida = vidaMax;
        attack = maxAttack;
        defensa = defensaMax;
        player = GameObject.FindGameObjectWithTag("Player");
        musicManagement = FindObjectOfType<MusicManagement>();
        sliderVidas.maxValue = vidaMax;
        sliderVidas.value = vida;
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            if (player.transform.position.x < transform.position.x && isFacingRight)
            {
                Flip();
            }
            else if (player.transform.position.x > transform.position.x && !isFacingRight)
            {
                Flip();
            }

            if (hasLineOfSight && !animator.GetBool("Death"))
            {
                Following();
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        Vector3 ScalerLifeBar = sliderVidas.transform.localScale;
        ScalerLifeBar.x *= -1;
        sliderVidas.transform.localScale = ScalerLifeBar;
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
                    if (!animator.GetBool("Death"))
                    {
                        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                    }
                }
            }
        }
    }

    protected virtual void Following()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        animator.SetBool("Attack", true);
    }

    public void GetDamaged(int damage, bool isCritical)
    {
        GetKnockedBackUwu(playerMovement.instance.transform, 15f);
        //musicManagement.SeleccionAudio(4, 1f);
        StartCoroutine(flash.FlashRoutine());

        netDamage = damage - defensa;
        if (netDamage > 0)
        {
            vida -= netDamage;
            sliderVidas.value = vida;
            UpdateHealthColor();
            ShowDamage(netDamage, isCritical);
        }
        if (vida <= 0)
        {
            animator.SetBool("Death", true);
            StartCoroutine(OnDieAnimationComplete());
        }
    }

    protected virtual IEnumerator OnDieAnimationComplete()
    {
        yield return new WaitForSeconds(0.5f);
        Die();
    }

    protected virtual void Die()
    {
        if (OnEnemyKilled != null)
        {
            OnEnemyKilled(this);
        }
        if (lootDropper != null)
        {
            lootDropper.DropLoot(transform.position);
        }
        ControladorDeAtaque playercomp = player.GetComponent<ControladorDeAtaque>();
        playercomp.AddManá(10);
        Destroy(gameObject);
    }

    protected void UpdateHealthColor()
    {
        if (vida > vidaMax * 0.5f)
        {
            sliderVidas.fillRect.GetComponent<Image>().color = fullHealthColor;
        }
        else if (vida > vidaMax * 0.25f)
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
        Debug.Log(diference);
        rb.AddForce(diference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
        if (jugador != null)
        {
            jugador.GetDamaged(attack);
        }
    }

    private void ShowDamage(int damage, bool isCritical)
    {
        GameObject damageTextInstance = Instantiate(damageTextPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        damageTextInstance.transform.SetParent(transform);
        TextMeshProUGUI damageText = damageTextInstance.GetComponentInChildren<TextMeshProUGUI>();
        if (damageText == null)
        {
            Debug.LogError("El prefab de texto de daño no tiene un componente TextMeshProUGUI.");
            return;
        }
        damageText.text = damage.ToString();
        damageText.color = isCritical ? Color.red : Color.yellow;
        damageText.fontSize = isCritical ? 72 : 60;

        StartCoroutine(FadeDamageText(damageTextInstance));
    }

    private IEnumerator FadeDamageText(GameObject damageTextInstance)
    {
        TextMeshProUGUI damageText = damageTextInstance.GetComponentInChildren<TextMeshProUGUI>();
        float duration = 1f;
        float elapsedTime = 0f;
        Vector3 originalPosition = damageText.transform.localPosition;

        while (elapsedTime < duration)
        {
            damageText.transform.localPosition = originalPosition + Vector3.up * (elapsedTime / duration);
            damageText.alpha = 1 - (elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(damageTextInstance);
    }
}
