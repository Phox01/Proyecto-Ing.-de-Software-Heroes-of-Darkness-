using UnityEngine;

public class Atributos : MonoBehaviour
{
    public int health;
    public int attack;
    public int currentHealth;
    public BarraVida barraDeVida;
    private playerMovement PlayerMovement;

    public void Start()
    {
        // Inicializa currentHealth al valor de health al comienzo del juego
        currentHealth = health;
        barraDeVida.InicializarBarraDeVida(currentHealth);
        
    }

   
}
