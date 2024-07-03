using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int attack;
    public BombEnemy bombEnemy;

    private void Start()
    {
        attack = bombEnemy.attack;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("El script HitBox sí funciona");
            bombEnemy.Explode();
        }
        ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
        if (jugador != null)
        {
            jugador.GetDamaged(attack);
        }
    }
}
