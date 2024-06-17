using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGrande : Enemigo
{
    // Start is called before the first frame update
    private float timer = 0f;
    
    public GameObject rocaHieloPrefab;
    private LayerMask capaEnemigos;
    public PolygonCollider2D hitbox;
    public Rigidbody2D rb;
    private Vector2 direccionMovimiento;
    protected override void Update()
    {
        direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        Invoke("EmpiezaGenerar", 1f);
        EjecutarAtaque();
        ActualizarPuntoAtaque();
    }


     // Intervalo de tiempo entre spawns (en segundos)

    private void EmpiezaGenerar()
    {
        Invoke("GenerarRoca", 1f);
        
    }

    private void GenerarRoca()
    {
        timer += Time.deltaTime;
        

        if (timer >= 9f)
        {
            
            GameObject rocaHielo = Instantiate(rocaHieloPrefab, player.transform.position, player.transform.rotation);
            
            
            timer = 0f;
        }
    }

    // Eliminar Start() y Update()
 

    private void EjecutarAtaque()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layerMask);

        List<Collider2D> resultados = new List<Collider2D>();
        hitbox.OverlapCollider(filter, resultados);
        foreach (Collider2D player in resultados)
        {

            if (player != null)
            {
                
                Invoke("Attack",4);
            }
        }
    }

    
    void Attack()
    {
        rb.velocity = Vector2.zero;
        
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(layerMask);

        List<Collider2D> resultados = new List<Collider2D>();
        hitbox.OverlapCollider(filter, resultados);
        foreach (Collider2D player in resultados)
        {
            
            if (player != null)
            {
                Debug.Log("ejecutar");
                int damage = 50;
                int trueDamage = damage + Random.Range(-3, 4);
                ControladorDeAtaque prueba=player.GetComponent<ControladorDeAtaque>();
                prueba.GetDamaged(trueDamage);
            }
        }
        

    }

    void ActualizarPuntoAtaque()
    {
        if (direccionMovimiento != Vector2.zero)
        {
            Vector3 nuevaPosicion = transform.position + (Vector3)direccionMovimiento * 0.5f;
            hitbox.transform.position = nuevaPosicion;

            float angle = Mathf.Atan2(direccionMovimiento.y, direccionMovimiento.x) * Mathf.Rad2Deg;
            hitbox.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }
}
