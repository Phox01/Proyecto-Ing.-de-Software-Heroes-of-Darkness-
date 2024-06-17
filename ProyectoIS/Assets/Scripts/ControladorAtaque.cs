using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Security.Cryptography.X509Certificates;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public PolygonCollider2D areaAtaque;
    private int playerAttack;
    public int currentHealth;
    private float critChance;
    private float critAttack;
    public Atributos atributos;
    private Vector2 direccionMovimiento;
    public Animator animator;
    private MusicManagement musicManagement;
    public Rigidbody2D rb;
    public Slider sliderVidas;

    protected Flash flash;


    private Color fullHealthColor = Color.green;
    private Color midHealthColor = Color.yellow;
    private Color lowHealthColor = Color.red;




    private void Awake()
    {

        flash = GetComponent<Flash>();
        sliderVidas = FindObjectOfType<Slider>();

        musicManagement = FindObjectOfType<MusicManagement>();
    }


    void Start()
    {
        playerAttack = atributos.attack;
        currentHealth = atributos.health;
        areaAtaque.isTrigger = true;
        if (sliderVidas != null)
        {
            sliderVidas.maxValue = atributos.health; // Asegúrate de que el maxValue del slider sea igual a la salud máxima.
            sliderVidas.value = currentHealth;
        }
    }

    void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") == false)
        {

            Attack();

        }

        if (Input.GetKeyDown(KeyCode.Q) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") == false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") == false)
        {

            Magic();

        }

        ActualizarPuntoAtaque();
    }

    void Magic()
    {
        rb.velocity = Vector2.zero;
        animator.SetTrigger("Magic");
        musicManagement.SeleccionAudio(animator.GetInteger("NumbAtt") - 1, 1f);
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


}
