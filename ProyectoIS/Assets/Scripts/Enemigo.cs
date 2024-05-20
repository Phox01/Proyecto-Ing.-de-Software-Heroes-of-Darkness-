using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public int vidaMax = 10000;
    int vida;
    void Start(){
        vida = vidaMax;
    }
    public void RecibirDano(int dano){
        vida -= dano;
        if(vida<=0){
            Destroy(gameObject);
        }
    }
}