using UnityEngine;
using System.Collections;


public class BolaFuegoAzul : MonoBehaviour
{
    public float speed = 6f;
    public int damage;
    private MusicManagement musicManagement;
   
    private ControladorDeAtaque player;
    private Rigidbody2D rb;
    void Start()
    {
        player =FindObjectOfType<ControladorDeAtaque>();
        // Encuentra la referencia a MusicManagement
        musicManagement = FindObjectOfType<MusicManagement>();
        rb= GetComponent<Rigidbody2D>();
        Ir();
    }

    public void Ir()
    {
        Vector2 diference = (player.transform.position - transform.position).normalized * 9 * rb.mass;

        rb.AddForce(diference, ForceMode2D.Impulse);


    }

    void Update()
    {

        //transform.Translate(player.transform.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (player != null)
        {
            player.GetDamaged( 5);
            //musicManagement.SeleccionAudio(5, 1f);
        }

        Destroy(gameObject);
    }
}

