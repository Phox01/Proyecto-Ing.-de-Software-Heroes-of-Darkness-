using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : Enemigo
{
    public int poisonDamage = 10;
    public float poisonDuration = 3f;
    public float poisonInterval = 1f;
    public bool move=true;
    private Color poisonColor = new Color(1f, 0.05f, 0.59f); // Color FF0D97 en formato RGBA
    private Color originalColor = Color.white; // Color FFFFFF
    public float forceDash = 35;
    public float dashInterval = 10.0f;

    private Vector2 direccionMovimiento;
    // Duración del aturdimiento antes del dash
    public float stunDuration = 1.5f;

    // Velocidad del dash
    public float dashSpeed = 10.0f;
    private float nextDashTime;
    private Vector2 direction;

    private bool isDashing = false;
    
    public float dashTime = 1f;
    public PolygonCollider2D hitbox;




    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Start();
        
        
        ResetTimer();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (move)
        {
            base.Update();
        }
        

        if (IsTimeToDash())
        {

            if (!isDashing)
            {
                DashTowardsPlayer();
            }
            
        }

        ActualizarPuntoAtaque();
    }

    protected override void Following()
    {
        base.Following();
        // Comienza a atacar si está cerca del jugador
        if (hasLineOfSight && !animator.GetBool("Death"))
        {
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Verifica si el jugador está en rango de ataque
        if (Vector2.Distance(transform.position, player.transform.position) <= 1.5f)
        {
            // Aplicar daño por veneno al jugador
            StartCoroutine(ApplyPoisonDamage(player.GetComponent<ControladorDeAtaque>()));
        }
    }

    private IEnumerator ApplyPoisonDamage(ControladorDeAtaque jugador)
    {
        float elapsed = 0f;
        SpriteRenderer spriteRenderer = jugador.GetComponent<SpriteRenderer>();
        Flash flash = jugador.GetComponent<Flash>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = poisonColor;
        }

        while (elapsed < poisonDuration)
        {
            if (flash != null)
            {
                StartCoroutine(flash.FlashRoutine());
            }
            jugador.GetDamaged(poisonDamage);
            elapsed += poisonInterval;
            yield return new WaitForSeconds(poisonInterval);
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
        if (jugador != null)
        {
            jugador.GetDamaged(attack);
            StartCoroutine(ApplyPoisonDamage(jugador));
        }
    }

    //private void StartDash()
    //{

    //    //animator.SetBool("Dash", true);
    //    isDashing = true;
    //    dashTime = Time.time + dashDuration;
    //    lastDashTime = Time.time;
    //}

    //private void EndDash()
    //{
    //    isDashing = false;
    //    //animator.SetBool("Dash", false);
    //}


    private void ResetTimer()
    {
        nextDashTime = Time.time + dashInterval;
    }

    private bool IsTimeToDash()
    {
        return Time.time >= nextDashTime;
    }

    private void ExecuteDash()
    {
        
        // Obtener la dirección hacia el personaje


        // Aplicar la velocidad del dash a la dirección
        

        isDashing = true;

        

        rb.velocity = direction * dashSpeed;
        StartCoroutine(StopDash());
        // Esperar un tiempo breve para que el dash sea visible
        
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashTime);
        rb.velocity = Vector2.zero;
        isDashing = false;
        move = true;
        
        ResetTimer();
    }
    private void DashTowardsPlayer()
    {
         direction = (player.transform.position - transform.position).normalized;
        Debug.Log(rb.mass);
        Vector2 diference = direction * forceDash* rb.mass;
        Debug.Log(diference);
        move = false;
        isDashing = true;
        
        Invoke("ExecuteDash", stunDuration);
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
