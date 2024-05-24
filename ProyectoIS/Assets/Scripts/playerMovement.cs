using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    Vector2 movement;
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // check if "bash" is playing...
        {
            animator.SetBool("CanMove", false);
        }
        else
        {
            animator.SetBool("CanMove", true); // "bash" is NOT playing -> walk
        }


        if (animator.GetBool("CanMove") == true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
    void FixedUpdate(){
        if (animator.GetBool("CanMove") == true) { 
            rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        }
        if (movement.x == 1 || movement.x == -1 || movement.y == 1 || movement.y == -1) {
            animator.SetFloat("lastMoveX", movement.x);
            animator.SetFloat("lastMoveY", movement.y);

        }
    }
}
