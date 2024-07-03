using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox1 : MonoBehaviour
{
    // Start is called before the first frame update
    private CircleCollider2D circleCollider;
    public int Damage = 5;
    void Start()
    {
         circleCollider = this.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("lelgo aqui");
            BounceBack bounce = collision.gameObject.GetComponent<BounceBack>();
            ControladorDeAtaque player = collision.gameObject.GetComponent<ControladorDeAtaque>();
            //bounce.force = 30;


            
            //StartCoroutine(ActivateDeactivateCoroutine(other));


            //player.GetDamaged(5);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        


        
    }

    IEnumerator ActivateDeactivateCoroutine(Collider2D other)
    {
        // Activate the object after 5 seconds

        Debug.Log("lelgo aqui 2");
        
        // Wait for another 5 seconds before deactivating
        yield return new WaitForSeconds(1f);
        //BounceBack bounce = other.gameObject.GetComponent<BounceBack>();
        //circleCollider.isTrigger = true;
        //Debug.Log("lelgo aqui");
        //bounce.force = 15;
    }
}
