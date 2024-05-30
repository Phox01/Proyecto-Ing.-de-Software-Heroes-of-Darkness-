using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
    public bool gettingKnockedBack { get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;
    private MusicManagement musicManagement;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        musicManagement = FindObjectOfType<MusicManagement>();
    }

    void Start(){
        
        vida = vidaMax;
        attack = maxAttack;
        defensa = defensaMax;
    }
    public void GetDamaged(int damage){
        GetKnockedBackUwu(playerMovement.Instance.transform, 15f);
        
        
        
        
        netDamage = damage-defensa;
        if(netDamage>0){
            vida -= netDamage;
            Debug.Log(damage );
            }
        if(vida<=0){
            musicManagement.SeleccionAudio(4, 1f);
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


    public void GetKnockedBackUwu(Transform damageSource, float knockBackThrust)
   
    {
        
        gettingKnockedBack = true;
        
        Vector2 diference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        Debug.Log(diference);
        rb.AddForce(diference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }

}