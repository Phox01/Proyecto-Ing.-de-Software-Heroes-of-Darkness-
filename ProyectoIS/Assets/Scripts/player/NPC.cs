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
    private bool isFacingRight = true;
    public Vector3 targetPosition;
    public Animator animator;
    private Vector2 lastMoveDirection;
    private Vector2 currentMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
    }

    // Update is called once per frame
    void Update(){
        transform.position = Vector2.MoveTowards(transform.position, routePoints[random].transform.position, patrolSpeed * Time.deltaTime);
        currentMoveDirection = (routePoints[random].transform.position - (Vector3)transform.position).normalized;
        if (currentMoveDirection.x<0 && isFacingRight)
        {
            Flip();
        } else if (currentMoveDirection.x>0 && !isFacingRight)
        {
                Flip();
        }

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
        UpdateAnimator(lastMoveDirection);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
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