using UnityEngine;

public class Atributos : MonoBehaviour
{
    public int health;
    public int attack;
    public int currentHealth;
    public int currentManá;
    public float critChance;
    public float critAttack;
    public int maná;

    public void Start()
    {
        currentHealth = health;
        currentManá=maná;
    }
}
