using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    void Start()
    {
        animator = GetComponent<Animator>();

        routePoints = GameObject.FindGameObjectsWithTag("Point");
        random = Random.Range(0, routePoints.Length);
        speed = 2;
    }

    void Update()
    {
        Vector3 direction = Character.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        transform.position = Vector2.MoveTowards(transform.position, routePoints[random].transform.position, speed*Time.deltaTime);
        time += Time.deltaTime;
        if(time >= 3)
        {
            random = Random.Range(0, routePoints.Length);
            time = 0;
        }

        targetPosition = routePoints[random].transform.position;
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f) //Hay que arreglar la condición para que el personaje reproduzca la animación de Idle
        {
            isMoving = true;
            animator.SetBool("isMoving", true);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", false);

        }

    }
}
