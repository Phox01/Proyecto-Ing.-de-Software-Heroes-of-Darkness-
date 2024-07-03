using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject[] routePoints;
    private int random;
    private float patrolSpeed;
    private float time;
    private bool isMoving;
    public Vector3 targetPosition;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        // LÓGICA DE HACER EL PATRULLAJE
        transform.position = Vector2.MoveTowards(transform.position, routePoints[random].transform.position, patrolSpeed * Time.deltaTime);
        time += Time.deltaTime;
        if (time >= 4)
        {
            random = Random.Range(0, routePoints.Length);
            time = 0;
        }
        //targetPosition = routePoints[random].transform.position;
        //if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        //{
        //    isMoving = true;
        //    animator.SetBool("isMoving", isMoving);
        //}
        //else
        //{
        //    isMoving = false;
        //    animator.SetBool("isMoving", isMoving);
        //}
    }
}
