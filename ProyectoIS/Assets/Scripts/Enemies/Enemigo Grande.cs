using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoGrande : Enemigo
{
    // Start is called before the first frame update
    private float timer = 0f;
    public GameObject rocaHieloPrefab;
    public PolygonCollider2D hitbox;

    private bool movimiento = true;
    private Vector2 direccionMovimiento;
    private bool ataque = true;
    protected override void Update()
    {
        if(player!=null){
            if (movimiento)
        {
            base.Update();
            

            direccionMovimiento = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            if ((player.transform.position-this.transform.position).magnitude<=30f)
            {
                GenerarRoca();
                
            }
            EjecutarAtaque();
            ActualizarPuntoAtaque();
        }}
    }


     // Intervalo de tiempo entre spawns (en segundos)

    private void EmpiezaGenerar()
    {
        
        GameObject rocaHielo = Instantiate(rocaHieloPrefab, player.transform.position, player.transform.rotation);

        movimiento = true;
    }

    private void GenerarRoca()
    {
        timer += Time.deltaTime;
        

        if (timer >= 7f && ataque!=false)
        {
            animator.SetTrigger("isThrowing");
            movimiento=false;
            Invoke("EmpiezaGenerar", 1f);


            timer = 0f;
        }
    }

 

    private void EjecutarAtaque()
{

        if (ataque)
        {
            List<Collider2D> resultados = new List<Collider2D>();
            hitbox.OverlapCollider(new ContactFilter2D(), resultados);
            bool playerDetected = false;

            foreach (Collider2D player in resultados)
            {
                if (player != null && player.CompareTag("Player"))
                {
                    animator.SetTrigger("isPreparing");
                    playerDetected = true;
                    break;
                }
            }

            if (playerDetected)
            {
                //aqui empieza animacion
                ataque = false;
                movimiento = false;
                Invoke("Attack", 2);
            }
        }
    
}

void Attack()
{
    animator.SetTrigger("isAttacking");
    rb.velocity = Vector2.zero;

    List<Collider2D> resultados = new List<Collider2D>();
    hitbox.OverlapCollider(new ContactFilter2D(), resultados);

    foreach (Collider2D player in resultados)
    {
        if (player != null && player.CompareTag("Player"))
        {
            Debug.Log("ejecutar");
            int damage = 50;
            int trueDamage = damage + Random.Range(-3, 4);
            ControladorDeAtaque prueba = player.GetComponent<ControladorDeAtaque>();
            if (prueba != null)
            {
                prueba.GetDamaged(trueDamage);
                
            }
        }
    }
        ataque = true;
        movimiento = true;
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
