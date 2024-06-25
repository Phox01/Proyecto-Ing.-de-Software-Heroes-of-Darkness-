using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Minotaurus : Enemigo
{
    public GameObject[] routePoints;
    public int random;
    public float patrolSpeed;
    public float time;
    private bool isMoving;
    public Vector3 targetPosition;
    private Vector3 previousDirection;
    public GameObject hitBox;

    public float attackCooldown = 3f; // Cooldown de 3 segundos
    private float lastAttackTime; // Tiempo del último ataque

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        routePoints = GameObject.FindGameObjectsWithTag("Point");
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
        lastAttackTime = -attackCooldown; // Inicializa para que pueda atacar inmediatamente
        hitBox.SetActive(false);
    }

    protected override void Update()
    {
        // LÓGICA PARA QUE EL ENEMIGO SIEMPRE MIRE AL PERSONAJE PRINCIPAL
        if (player!=null){Vector3 direction = player.transform.position - transform.position;
        if ((direction.x >= 0.0f && previousDirection.x < 0.0f) || (direction.x < 0.0f && previousDirection.x >= 0.0f))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            Vector3 ScalerLifeBar = sliderVidas.transform.localScale;
            ScalerLifeBar.x *= -1;
            sliderVidas.transform.localScale = ScalerLifeBar;
        }
        previousDirection = direction;

        // LÓGICA DE MOVIMIENTO (Patrullaje o Perseguir)
        if (hasLineOfSight)
        {
            // LÓGICA DE PERSEGUIR
            if (!animator.GetBool("isAttacking"))
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            // LÓGICA DE ATAQUE
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= 2 && Time.time - lastAttackTime >= attackCooldown)
            {
                StartCoroutine(Attack());
                lastAttackTime = Time.time;
            }
        }
        else
        {
            // LÓGICA DE HACER EL PATRULLAJE
            transform.position = Vector2.MoveTowards(transform.position, routePoints[random].transform.position, patrolSpeed * Time.deltaTime);
            time += Time.deltaTime;
            if (time >= 3)
            {
                random = Random.Range(0, routePoints.Length);
                time = 0;
            }
            targetPosition = routePoints[random].transform.position;
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                isMoving = true;
                animator.SetBool("isMoving", isMoving);
            }
            else
            {
                isMoving = false;
                animator.SetBool("isMoving", isMoving);
            }
        }}
    }

    private IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        musicManagement.SeleccionAudio(7, 1f);
        yield return new WaitForSeconds(0.4f); // Tiempo para la animación de ataque
        ActivateHitBox();
        yield return new WaitForSeconds(1f); // Tiempo para la hitbox
        animator.SetBool("isAttacking", false);
    }


    protected override IEnumerator OnDieAnimationComplete()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Die();

    }

    protected override void OnCollisionEnter2D(Collision2D collision) // Probando, para que no herede esa función del padre
    {
        
    }

    private void ActivateHitBox()
    {
        hitBox.SetActive(true);
        StartCoroutine(DeactivateHitBoxAfterTime(1f));
    }

    private IEnumerator DeactivateHitBoxAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        hitBox.SetActive(false);

    }


}

