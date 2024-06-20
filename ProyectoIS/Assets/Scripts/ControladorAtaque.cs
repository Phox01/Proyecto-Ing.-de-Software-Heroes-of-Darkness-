using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public GameObject projectilePrefab;
    public Transform LaunchOffset;
    public float magicCooldown = 0.5f; 
    private float lastMagicTime;

    public PolygonCollider2D areaAtaque;
    private int playerAttack;
    public int currentHealth;
    public int currentManá;
    private float critChance;
    private float critAttack;
    public Sprite projectileSprite;
    public Atributos atributos;
    private Vector2 direccionMovimiento;
    public Animator animator;
    private MusicManagement musicManagement;
    public Rigidbody2D rb;
    public Slider sliderVidas;
    public Slider sliderManá;

    protected Flash flash;


    private Color fullHealthColor = Color.green;
    private Color midHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;
    private int maxMana;




    private void Awake()
    {

        flash = GetComponent<Flash>();
        sliderVidas = (Slider)GameObject.FindObjectsOfType (typeof(Slider)) [1];
        sliderManá = (Slider)GameObject.FindObjectsOfType (typeof(Slider)) [0];
        musicManagement = FindObjectOfType<MusicManagement>();
    }


    void Start()
    {
        playerAttack = atributos.attack;
        currentHealth = atributos.health;
        currentManá = atributos.maná;
        maxMana = atributos.maná;
        areaAtaque.isTrigger = true;
        if (sliderVidas != null)
        {
            sliderVidas.maxValue = atributos.health; // Asegúrate de que el maxValue del slider sea igual a la salud máxima.
            sliderVidas.value = currentHealth;
        }
        if (sliderManá != null)
        {
            sliderManá.maxValue = atributos.maná; // Asegúrate de que el maxValue del slider sea igual al maná máximo.
            sliderManá.value = currentManá;
        }
        StartCoroutine(RegenerateMana());
    }

    void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") == false)
        {

            Attack();

        }

        if (Input.GetKeyDown(KeyCode.Q) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") == false && currentManá>=10 && Time.time > lastMagicTime + magicCooldown)
        {

            Magic();
            

        }

        ActualizarPuntoAtaque();
    }

    void Magic()
    {
        lastMagicTime = Time.time;
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Magic");
        musicManagement.SeleccionAudio(animator.GetInteger("NumbAtt") - 1, 1f);

        GameObject projectileObject = Instantiate(projectilePrefab, LaunchOffset.position, areaAtaque.transform.rotation);

        SpriteRenderer spriteRenderer = projectileObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = projectileSprite;

        // Get the Collider2D component of the projectile
        CircleCollider2D projectileCollider = projectileObject.GetComponent<CircleCollider2D>();

        // Get the Collider2D component of the player
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();

        // Make the projectile ignore the player collider
        Physics2D.IgnoreCollision(projectileCollider, playerCollider);
        

        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            int damage = CriticalDamage(playerAttack);
            int trueDamage = damage + Random.Range(-3, 4);
            projectileScript.damage = trueDamage;
        }
            currentManá=currentManá-10;
            sliderManá.value = currentManá;

    }

    void Attack()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Attack");
        //musicManagement.SeleccionAudio(animator.GetInteger("NumbAtt")-1, 1f);
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(capaEnemigos);

        List<Collider2D> resultados = new List<Collider2D>();
        areaAtaque.OverlapCollider(filter, resultados);
        foreach (Collider2D enemigo in resultados)
        {
            Enemigo enemigoComponent = enemigo.GetComponent<Enemigo>();
            if (enemigoComponent != null)
            {
                int damage = CriticalDamage(playerAttack);
                int trueDamage = damage + Random.Range(-3, 4);
                enemigoComponent.GetDamaged(trueDamage);
                musicManagement.SeleccionAudio(5, 1f);
            }
        }
        int count = animator.GetInteger("NumbAtt") + 1;
        if (count == 4)
        {
            count = 1;
        }
        animator.SetInteger("NumbAtt", count);

    }
    int CriticalDamage(int attack)
    {
        if (Random.value < critChance)
        {
            int criticalHit = Mathf.RoundToInt(attack * critAttack);
            return criticalHit;
        }
        else
        {
            return attack;
        }
    }
    void ActualizarPuntoAtaque()
    {
        if (direccionMovimiento != Vector2.zero)
        {
            Vector3 nuevaPosicion = transform.position + (Vector3)direccionMovimiento * 0.5f;
            areaAtaque.transform.position = nuevaPosicion;

            float angle = Mathf.Atan2(direccionMovimiento.y, direccionMovimiento.x) * Mathf.Rad2Deg;
            areaAtaque.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }

    public void GetDamaged(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(flash.FlashRoutine());


        sliderVidas.value = currentHealth;
        UpdateHealthColor();

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }
    }

    private void UpdateHealthColor()
    {
        if (currentHealth > atributos.health / 2)
        {
            sliderVidas.fillRect.GetComponent<Image>().color = fullHealthColor;
        }
        else if (currentHealth > atributos.health / 4)
        {
            sliderVidas.fillRect.GetComponent<Image>().color = midHealthColor;
        }
        else
        {
            sliderVidas.fillRect.GetComponent<Image>().color = lowHealthColor;
        }
    }
    public void AddHealth(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, atributos.health);
        sliderVidas.value = currentHealth;
        UpdateHealthColor();
    }
    public void AddManá(int amount)
    {
        currentManá += amount;
        currentManá = Mathf.Clamp(currentManá, 0, atributos.maná);
        sliderManá.value = currentManá;
    }
    IEnumerator RegenerateMana()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); 
            if (currentManá < maxMana)
            {
                currentManá = Mathf.Min(currentManá + 1, maxMana); 
                sliderManá.value = currentManá;
            }
        }
    }
}
