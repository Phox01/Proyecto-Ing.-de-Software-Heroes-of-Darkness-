using System.Collections;
using System.Collections.Generic;
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

    protected override void Start()
    {
        base.Start(); 
        animator = GetComponent<Animator>();

        routePoints = GameObject.FindGameObjectsWithTag("Point");
        player = GameObject.FindGameObjectWithTag("Player");
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
    }

    protected override void Update()
    {
        // LÓGICA PARA QUE EL ENEMIGO SIEMPRE MIRE AL PERSONAJE PRINCIPAL
        Vector3 direction = Character.transform.position - transform.position;
        if ((direction.x >= 0.0f && previousDirection.x < 0.0f) || (direction.x < 0.0f && previousDirection.x >= 0.0f))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        previousDirection = direction;

        // LÓGICA DE MOVIMIENTO (Patrullaje o Perseguir)
        if (hasLineOfSight)
        {
            if (!animator.GetBool("isAttacking"))
            { 
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            // LÓGICA DE ATAQUE
            float distanceToPlayer = Vector3.Distance(transform.position, Character.transform.position);
            if (distanceToPlayer <= 2)
            {
                animator.SetBool("isAttacking", true);
            }
            else
            {
                animator.SetBool("isAttacking", false);
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
        }
    }
    public override void GetDamaged(int damage){
        GetKnockedBackUwu(playerMovement.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
        
        

        netDamage = damage-defensa;
        if(netDamage>0){
            vida -= netDamage;
            }
        if(vida<=0){
            Destroy(gameObject);
        }
    }

    protected override void  FixedUpdate()
    {

        // RayCast para perseguir al personaje principal
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
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
        }
    }
}
