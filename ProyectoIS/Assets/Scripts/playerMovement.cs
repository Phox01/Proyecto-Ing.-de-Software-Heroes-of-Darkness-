using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    public int moveDuo = 1;
    public float dashSpeed = 3f; 
    public float attackImpulse= 2f; 
    public float dashDuration = 0.2f; 
    public float dashCooldown = 1f; 
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 movement;
    private bool isDashing = false;
    private float dashTime;
    private float lastDashTime;
    public static playerMovement Instance;
    private MusicManagement musicManagement;
    private Atributos mtributos;

    private Vector2 lastMovement;


    private void Awake()
    {
        Instance = this;
        musicManagement = FindObjectOfType<MusicManagement>();
    }
    void Update()
    {


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3"))
        {
            animator.SetBool("CanMove", false);
        }
        else
        {
            animator.SetBool("CanMove", true); 
        }

        if (animator.GetBool("CanMove"))
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
            
            if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time > lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }

        if (isDashing && Time.time >= dashTime)
        {
            EndDash();
        }

        
    }

    void FixedUpdate()
    {
        if(animator.GetFloat("Speed")==0){
            musicManagement.AudioLoop(3, 0.3f);
        }
        if (animator.GetBool("CanMove"))
        {
            lastMovement.x= (int)animator.GetFloat("lastMoveX");
            lastMovement.y= (int)animator.GetFloat("lastMoveY");
            if (isDashing)
            {
                rb.MovePosition(rb.position + dashSpeed* lastMovement.normalized * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
                if (movement.x != 0 || movement.y != 0)
                {

                    animator.SetFloat("lastMoveX", movement.x);
                    animator.SetFloat("lastMoveY", movement.y);

                }
            }

        }
        
    }

    private void StartDash()
    {
        musicManagement.SeleccionAudio(4, 0.3f);
        animator.SetBool("Dash", true);
        isDashing = true;
        dashTime = Time.time + dashDuration;
        lastDashTime = Time.time;
    }

    private void EndDash()
    {
        isDashing = false;
        animator.SetBool("Dash", false);
    }


    

}

    

 
