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
    public GameObject hit;

    public float attackCooldown = 3f; // Cooldown de 3 segundos
    private float lastAttackTime; // Tiempo del último ataque

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();

        routePoints = GameObject.FindGameObjectsWithTag("Point");
        player = GameObject.FindGameObjectWithTag("Player");
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
        lastAttackTime = 0;

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
            //LÓGICA DE PERSEGUIR
            if (!animator.GetBool("isAttacking"))
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }

            // LÓGICA DE ATAQUE
            float distanceToPlayer = Vector3.Distance(transform.position, Character.transform.position);
            
            if (distanceToPlayer <= 2) //&& Time.time - lastAttackTime >= attackCooldown 
            {
                Debug.Log(Time.time - lastAttackTime);
                Debug.Log("Si se cumple");
                animator.SetBool("isAttacking", true);
                Debug.Log("Ahi papa");
                StartCoroutine(AttackWithDelay(0.4f)); //Prueba                
                lastAttackTime = Time.time;
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
    public override void GetDamaged(int damage)
    {
        GetKnockedBackUwu(playerMovement.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());



        netDamage = damage - defensa;
        if (netDamage > 0)
        {
            vida -= netDamage;
        }
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    protected override void FixedUpdate()
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

    protected new void OnCollisionEnter2D(Collision2D collision) //Probando, para que no herede esa función del padre
    {
        Debug.Log("Bien");
    }

    public void createHitBox() //Crea la hitBox para el ataque
    {
        if (GameObject.Find("hit(Clone)"))
        {
            return;
        }
        else
        {
            Vector3 positionHit = new Vector3(hitBox.transform.position.x, hitBox.transform.position.y, 0);
            GameObject tempHit = Instantiate(hit, positionHit, Quaternion.identity);
            StartCoroutine(DestroyHitBoxAfterTime(tempHit, 1f)); //Prueba
        }
    }

    private IEnumerator DestroyHitBoxAfterTime(GameObject hitBox, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(hitBox);
    }

    private IEnumerator AttackWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        createHitBox();
    }
}