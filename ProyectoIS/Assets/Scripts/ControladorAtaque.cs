using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public PolygonCollider2D areaAtaque;
    private int ataqueJugador;
    public Atributos atributos;
    private Vector2 direccionMovimiento;
    public Animator animator;


    void Start()
    {
        ataqueJugador = atributos.ataque;
        areaAtaque.isTrigger = true;
    }

    void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space) && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")==false)
        {
            
            Atacar();
        }

        ActualizarPuntoAtaque();
    }

    void Atacar()
    {
        animator.SetTrigger("Attack");
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(capaEnemigos);

        List<Collider2D> resultados = new List<Collider2D>();
        areaAtaque.OverlapCollider(filter, resultados);

        foreach (Collider2D enemigo in resultados)
        {
            Enemigo enemigoComponent = enemigo.GetComponent<Enemigo>();
            if (enemigoComponent != null)
            {
                int dano = ataqueJugador + Random.Range(-3, 4);
                enemigoComponent.RecibirDano(dano);
                Debug.Log("Enemigo recibió " + dano + " puntos de daño.");
            }
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

}
