using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaMax;
    int vida;
    public int maxAttack;
    int attack;
    public int defensaMax;
    int defensa;
    int netDamage;
    void Start(){
        vida = vidaMax;
        attack = maxAttack;
        defensa = defensaMax;
    }
    public void GetDamaged(int damage){
        netDamage = damage-defensa;
        if(netDamage>0){
            vida -= netDamage;
            Debug.Log(damage );
            }
        if(vida<=0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hola");
            ControladorDeAtaque jugador = other.GetComponent<ControladorDeAtaque>();
            if (jugador != null)
            {
                Debug.Log("damge doned");
                jugador.GetDamaged(attack);
            }
        }
    }
}