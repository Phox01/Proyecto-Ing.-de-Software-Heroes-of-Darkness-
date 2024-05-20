using UnityEngine;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public float rangoAtaque = 1f;
    public Transform puntoAtaque;
     private int ataqueJugador;
     public Atributos atributos;
    void Start()
    {
        capaEnemigos = LayerMask.GetMask("Enemigo");
        ataqueJugador = atributos.ataque;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Atacar();
        }
        
        ActualizarPuntoAtaque();
    }

    void Atacar(){
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque, capaEnemigos);
        foreach(Collider2D enemigo in enemigos){
             int dano = ataqueJugador + Random.Range(-3, 4);
             enemigo.GetComponent<Enemigo>().RecibirDano(dano);
                        
        }

    }

    void ActualizarPuntoAtaque(){

    }
    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
    }
}
