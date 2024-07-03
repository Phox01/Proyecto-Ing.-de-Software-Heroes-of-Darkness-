using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : Enemigo
{
    public GameObject hitBox;
    public GameObject hit;
    private Vector3 previousDirection;
    public BoxCollider2D boxCollider;

    // Update is called once per frame
    protected override void Update()
    {
        // LÓGICA PARA QUE EL ENEMIGO SIEMPRE MIRE AL PERSONAJE PRINCIPAL
        if (player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            if ((direction.x >= 0.0f && previousDirection.x < 0.0f) || (direction.x < 0.0f && previousDirection.x >= 0.0f))
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                Vector3 ScalerLifeBar = sliderVidas.transform.localScale;
                ScalerLifeBar.x *= -1;
                sliderVidas.transform.localScale = ScalerLifeBar;
            }
            previousDirection = direction;

            if (hasLineOfSight && !animator.GetBool("Death"))
            {
                Following();
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= 7f)
            {
                createHitBox();
            }
        }
        //if (vida <= 0)
        //{
        //    animator.SetBool("Death", true);
        //    StartCoroutine(OnDieAnimationComplete());
        //}
    }

    //Crea la hitbox cuando el personaje va a atacar.
    public void createHitBox()
    {
        if (GameObject.Find("Hit(Clone)"))
        {
            return;
        }
        else
        {
            Vector3 position = new Vector3(hitBox.transform.position.x, hitBox.transform.position.y, 0);
            GameObject tempHit = Instantiate(hit, position, Quaternion.identity);
            //hitBoxScript.bombEnemy = this;
            Destroy(tempHit, 2);
        }
    }

    //RayCast para el seguimiento
    protected override void FixedUpdate()
    {
        if (player != null)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position, Mathf.Infinity, ~layerMask);
            if (ray.collider != null)
            {
                hasLineOfSight = ray.collider.CompareTag("Player");
                if (hasLineOfSight)
                {
                    Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
                }
                else
                {
                    if (!animator.GetBool("Death"))
                    {
                        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
                    }
                }
            }
        }
    }

    public void Explode()
    {
        Debug.Log("Entra a la función Explode()");
        //ExpandCollider();
        vida = 0;
        Debug.Log("La vida es: "+vida);
        animator.SetBool("Death", true);
        StartCoroutine(OnDieAnimationComplete());
        //Debug.Log("Llega aquí");
    }

    //private void ExpandCollider()
    //{
    //    // Aumenta el tamaño del BoxCollider
    //    boxCollider.size = new Vector2(boxCollider.size.x * 6, boxCollider.size.y * 6); // Ajusta el factor de escala según sea necesario
    //    boxCollider.offset = new Vector2(boxCollider.offset.x * 6, boxCollider.offset.y * 6);
    //}
}
