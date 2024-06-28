using System.Collections;
using UnityEngine;

public class Estatua : MonoBehaviour
{
    public int vidaMax;
    private int vida;
    public Sprite[] destructionSprites; // Array de sprites para la destrucción
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        vida = vidaMax;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateEstatuaSprite();
    }

    public void ApplyDamage(int damage)
    {
        vida -= damage;
        if (vida <= 0)
        {
            Die();
        }
        else
        {
            UpdateEstatuaSprite();
        }
    }

    private void UpdateEstatuaSprite()
    {
        float healthPercentage = (float)vida / vidaMax;

        if (healthPercentage > 0.66f)
        {
            spriteRenderer.sprite = destructionSprites[0]; // Sprite de estado intacto
        }
        else if (healthPercentage > 0.33f)
        {
            spriteRenderer.sprite = destructionSprites[1]; // Sprite de estado medio
        }
        else
        {
            spriteRenderer.sprite = destructionSprites[2]; // Sprite de estado destruido
        }
    }

    private void Die()
    {
        // Añadir lógica específica para la destrucción de la estatua si es necesario
        Destroy(gameObject);
    }
}
