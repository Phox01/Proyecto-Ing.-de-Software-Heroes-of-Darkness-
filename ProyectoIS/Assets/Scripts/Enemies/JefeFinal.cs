using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFinal : Enemigo
{
    public int poisonDamage = 10;
    public float poisonDuration = 3f;
    public float poisonInterval = 1f;
    private Color poisonColor = new Color(1f, 0.05f, 0.59f); // Color FF0D97 en formato RGBA
    private Color originalColor = Color.white; // Color FFFFFF

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
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
}
