using UnityEngine;

public class Atributos : MonoBehaviour
{
    public int health;
    public int attack;
    public int currentHealth;
    public float critChance;
    public float critAttack;

    public void Start()
    {
        currentHealth = health;
    }
}
