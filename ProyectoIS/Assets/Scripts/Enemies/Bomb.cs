using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    Animator animator;
    private bool isMoving;
    Vector3 initialPosition;
    Vector3 currentPosition;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = transform.position;
        if (currentPosition != initialPosition)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
