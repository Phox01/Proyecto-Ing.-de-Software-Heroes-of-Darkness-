using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spowner : MonoBehaviour
{
    public float speed = 6f;
    public GameObject FuegoPrefab;
    private float timer = 0f;
    private ControladorDeAtaque player;
    private Rigidbody2D rb;
    void Start()
    {
        player = FindObjectOfType<ControladorDeAtaque>();
        // Encuentra la referencia a MusicManagement
        rb = GetComponent<Rigidbody2D>();
        Ir();
        Invoke("DestroyObject", 2);
    }

    public void Ir()
    {
        Vector2 diference = (player.transform.position - transform.position).normalized * 9 * rb.mass;

        rb.AddForce(diference, ForceMode2D.Impulse);


    }

    void Update()
    {
        PonerMuro();

        //transform.Translate(player.transform.position);
    }

    private void PonerMuro()

    {
        timer += Time.deltaTime;

        if (timer >= 0.2f)
        {

            GameObject muroFuego = Instantiate(FuegoPrefab, transform.position, Quaternion.identity);


            timer = 0f;
        }

    }

    private void DestroyObject()
    {


        // Destruye el objeto
        Debug.Log("destruye");
        Destroy(gameObject);
    }

}


