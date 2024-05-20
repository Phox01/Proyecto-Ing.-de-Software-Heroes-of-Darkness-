using UnityEngine;
using System.Collections.Generic;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public PolygonCollider2D areaAtaque;
    private int ataqueJugador;
    public Atributos atributos;
    private Vector2 direccionMovimiento;

    void Start()
    {
        ataqueJugador = atributos.ataque;
        areaAtaque.isTrigger = true;
    }

    void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
        }

        ActualizarPuntoAtaque();
    }

    void Atacar()
    {
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
            Quaternion nuevaRotacion = Quaternion.identity;

            if (direccionMovimiento == Vector2.up)
            {
                nuevaRotacion.z = 1;
                nuevaRotacion.w = 0;
            }
            else if (direccionMovimiento == Vector2.down)
            {
                nuevaRotacion.z = 0;
                nuevaRotacion.w = 1;
            }
            else if (direccionMovimiento == Vector2.right)
            {
                nuevaRotacion.z = Mathf.Sqrt(0.5f);
                nuevaRotacion.w = Mathf.Sqrt(0.5f);
            }
            else if (direccionMovimiento == Vector2.left)
            {
                nuevaRotacion.z = -Mathf.Sqrt(0.5f);
                nuevaRotacion.w = Mathf.Sqrt(0.5f);
            }
            areaAtaque.transform.localRotation = nuevaRotacion;
        }
    }
}
