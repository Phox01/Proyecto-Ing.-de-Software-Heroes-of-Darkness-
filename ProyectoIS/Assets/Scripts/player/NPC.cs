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
    public Animator animator;
    private Vector2 lastMoveDirection = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
    }

    // Update is called once per frame
    void Update(){
        Vector2 currentMoveDirection = (routePoints[random].transform.position - (Vector3)transform.position).normalized;

        // Check if there's a significant change in movement direction
        if (currentMoveDirection != Vector2.zero)
        {
            lastMoveDirection = currentMoveDirection;
        }

        // Update time and change route point after 4 seconds
        time += Time.deltaTime;
        if (time >= 4)
        {
            random = Random.Range(0, routePoints.Length);
            time = 0;
        }

        // Use lastMoveDirection to set animator parameters or assign sprites
        UpdateAnimator(lastMoveDirection);
    }

    void UpdateAnimator(Vector2 moveDirection)
    {
        // Assuming you have parameters in your Animator controller to control NPC animations based on direction
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);

        // You can also set bools or triggers based on conditions
        // animator.SetBool("IsMoving", moveDirection != Vector2.zero);
    }
}