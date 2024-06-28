using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroFuego : MonoBehaviour
{
    
   
    public int tiempo=4;
    public int damage = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", tiempo);

    }

    // Update is called once per frame
    private void DestroyObject()
    {


        // Destruye el objeto
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Player") )
        {

            Debug.Log("es el player");
            collision.collider.GetComponent<ControladorDeAtaque>().GetDamaged(damage);

        }
        
    }
}
