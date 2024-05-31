using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Minotaurus : MonoBehaviour
{
    public GameObject Character;
    public GameObject[] routePoints;
    public int random;
    public float speed;
    public float time;
    private Animator animator;
    private bool isMoving;
    public Vector3 targetPosition;
    private Vector3 previousDirection;
    private GameObject player;
    private bool hasLineOfSight = false;
    [SerializeField] private LayerMask layerMask;
    private bool isAttacking;

    void Start()
    {
        animator = GetComponent<Animator>();

        routePoints = GameObject.FindGameObjectsWithTag("Point");
        player = GameObject.FindGameObjectWithTag("Player");
        random = Random.Range(0, routePoints.Length);
        speed = 3;
    }

    void Update()
    {
        //LÓGICA PARA QUE EL ENEMIGO SIEMPRE MIRE AL PERSONAJE PRINCIPAL
        Vector3 direction = Character.transform.position - transform.position;
        if ((direction.x >= 0.0f && previousDirection.x < 0.0f) || (direction.x < 0.0f && previousDirection.x >= 0.0f))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        previousDirection = direction;

        //LÓGICA DE MOVIMIENTO (Patrullaje o Perseguir)
        if (hasLineOfSight)
        {
            Debug.Log(hasLineOfSight);
            Debug.Log(speed);
            if (!animator.GetBool("isAttacking")) { 
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            
            }

            //LÓGICA DE ATAQUE
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
            //LÓGICA DE HACER EL PATRULLAJE
            transform.position = Vector2.MoveTowards(transform.position, routePoints[random].transform.position, speed * Time.deltaTime);
            time += Time.deltaTime;
            if (time >= 3)
            {
                random = Random.Range(0, routePoints.Length);
                time = 0;
            }
            targetPosition = routePoints[random].transform.position;
            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) //Hay que arreglar la condición para que el personaje reproduzca la animación de Idle
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

    //RayCast para perseguir al personaje principal
    private void FixedUpdate()
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
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
        }
    }
}
