using UnityEngine;

public class Atributos : MonoBehaviour
{
    public int health;
    public int attack;
    public int currentHealth;

    public void Start()
    {
        // Inicializa currentHealth al valor de health al comienzo del juego
        currentHealth = health;
    }
}
