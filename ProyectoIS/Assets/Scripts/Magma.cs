using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using UnityEngine;

public class ZonaDeDaño : Enemigo
{
    private float damageInterval = 1.0f; // Intervalo de daño en segundos
    private int damageAmount = 10; // Cantidad de daño por intervalo
    private Coroutine damageCoroutine;

    protected override void Start()
    {
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            damageCoroutine = StartCoroutine(DamagePlayerOverTime(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator DamagePlayerOverTime(Collider2D player)
    {
        ControladorDeAtaque playerComponent = player.GetComponent<ControladorDeAtaque>();
        while (playerComponent != null)
        {
            playerComponent.GetDamaged(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}

