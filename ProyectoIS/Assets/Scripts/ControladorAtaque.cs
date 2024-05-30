using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public PolygonCollider2D areaAtaque;
    private int playerAttack;
    private int currentHealth;
    public Atributos atributos;
    private Vector2 direccionMovimiento;
    public Animator animator;
    private MusicManagement musicManagement;


    void Start()
    {
        playerAttack = atributos.attack;
        currentHealth = atributos.health;
        areaAtaque.isTrigger = true;
        musicManagement = FindObjectOfType<MusicManagement>();
    }

    void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1")==false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2")==false && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3")==false)
        {
            
            Attack();
        }

        ActualizarPuntoAtaque();
    }

    void Attack()
    {
        
        animator.SetTrigger("Attack");
        musicManagement.SeleccionAudio(0, 1f);
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(capaEnemigos);

        List<Collider2D> resultados = new List<Collider2D>();
        areaAtaque.OverlapCollider(filter, resultados);
        foreach (Collider2D enemigo in resultados)
        {
            Enemigo enemigoComponent = enemigo.GetComponent<Enemigo>();
            if (enemigoComponent != null)
            {
                int damage = playerAttack + Random.Range(-3, 4);
                enemigoComponent.GetDamaged(damage);
                musicManagement.SeleccionAudio(1, 1f);
                Debug.Log("Enemigo recibió " + damage + " puntos de daño.");
            }
        }
        int count=animator.GetInteger("NumbAtt")+1;
        if (count==4){
            count=1;
        }
        animator.SetInteger("NumbAtt", count);


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
        Debug.Log(damage);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
