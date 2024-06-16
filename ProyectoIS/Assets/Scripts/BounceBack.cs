using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBack : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 23)
        {
            EcharAtras(collision.collider.transform);

        }

        


    }

    //

    public void EcharAtras(Transform vector)
    {
        playerMovement.instance.SetKnockbackActive(true);



        Vector2 diference = (transform.position - vector.position).normalized * 15 * rb.mass;

        rb.AddForce(diference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutinePlayer());

    }
    private IEnumerator KnockRoutinePlayer()
    {
       
        yield return new WaitForSeconds(.2f);
        rb.velocity = Vector2.zero;
        playerMovement.instance.SetKnockbackActive(false);

    }
}
