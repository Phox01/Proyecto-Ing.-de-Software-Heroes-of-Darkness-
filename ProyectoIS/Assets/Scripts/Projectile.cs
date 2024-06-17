using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class Projectile : MonoBehaviour
{
    public float speed=6f;
    void Update()
    {
        
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag=="Murcielago"){
            Destroy(collision.gameObject);
    }
        Destroy(gameObject);
    }
}
