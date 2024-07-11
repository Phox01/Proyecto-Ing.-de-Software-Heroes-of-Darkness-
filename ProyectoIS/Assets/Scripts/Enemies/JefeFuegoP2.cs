using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFuegoP2 : Enemigo
{
    // Start is called before the first frame update

    public GameObject circleCollider1;
    private float timer = 0f;
    private Vector3 previousDirection;
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        Pisoton();
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
        }
    }

    private void Pisoton()
    {
        timer += Time.deltaTime;
        
        if (timer >= 15f)
        {
            
            timer = 0f;
            StartCoroutine(ActivateDeactivateCoroutine());
            
        }
    }

    IEnumerator ActivateDeactivateCoroutine()
    {
        // Activate the object after 5 seconds
        
        animator.SetBool("isMoving", true);
        yield return new WaitForSeconds(1.8f);
        animator.SetBool("isMoving", false);
        circleCollider1.SetActive(true);
        
        // Wait for another 5 seconds before deactivating
        yield return new WaitForSeconds(0.1f);
        circleCollider1.SetActive(false);
        


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
}
