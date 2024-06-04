using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitBoxAttack : MonoBehaviour
{
    public int damage = 10;
    private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasCollided && collision.CompareTag("Player"))
        {
            ControladorDeAtaque jugador = collision.GetComponent<ControladorDeAtaque>();
            if (jugador != null)
            {
                jugador.GetDamaged(damage);
                Debug.Log("Player hit by hitbox and took damage: " + damage);
                hasCollided = true;
                // Opcional: desactivar el collider para asegurarse de que no se ejecute de nuevo
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
}
