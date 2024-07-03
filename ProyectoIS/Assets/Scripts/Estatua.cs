using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Estatua : Enemigo
{
    [SerializeField] private Sprite[] destructionSprites; // Array de sprites para la destrucción
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateEstatuaSprite();
    }

    private void Update()
    {
        UpdateEstatuaSprite();
    }

    private void FixedUpdate(){
        
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

    protected override IEnumerator OnDieAnimationComplete()
    {
        yield return base.OnDieAnimationComplete();
    }

    // protected override void OnCollisionEnter2D(Collision2D collision)
    // {
    //     ControladorDeAtaque jugador = collision.gameObject.GetComponent<ControladorDeAtaque>();
    //     if (jugador != null)
    //     {
    //         jugador.GetDamaged(attack);
    //     }
    // }
}
