using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBoxAttack : MonoBehaviour
{   private int damage;
    public Minotaurus minotaurus;
    void Update()
    {
        damage = minotaurus.attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ControladorDeAtaque jugador = collision.GetComponent<ControladorDeAtaque>();
            BounceBack bounceBack = collision.GetComponent<BounceBack>();
            if (jugador != null)
            {
                jugador.GetDamaged(damage);
                bounceBack.EcharAtras(transform);
                Debug.Log("Player hit by hitbox and took damage: " + damage);
            }
        }
    }
}
