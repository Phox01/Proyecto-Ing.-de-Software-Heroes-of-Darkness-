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
    private MusicManagement musicManagement;
    
    private Vector2 lastMovement;
    private bool isKnockbackActive = false;
    private ManagementMenu managementMenu;
    //private RaycastHit2D bodyBox;
    //private BoxCollider2D hit;
    //private Vector3 movePlry;
    //public float speed;

    public static playerMovement instance;

    //public void Start()
    //{
    //    bodyBox = GetComponenet<BoxCollider2D>();
    //}
    private void Awake()
    {
         if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        musicManagement = FindObjectOfType<MusicManagement>();
        managementMenu = FindObjectOfType<ManagementMenu>();
    }
    void Update()
    {


        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 3") || animator.GetCurrentAnimatorStateInfo(0).IsName("Magic"))
        {
            animator.SetBool("CanMove", false);
            if (!isKnockbackActive)
            {
                rb.velocity=Vector2.zero;
                
            }
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
        
        if (Input.GetKeyDown(KeyCode.Escape) && managementMenu != null)
        {
            managementMenu.PausarPartida();
        }

    }

    void FixedUpdate()
    {
        //float hor = Input.GetAxisRaw("Horizontal");
        //float ver = Input.GetAxisRaw("Vertical");
        //movePlyr = new Vector3(hor * speed, ver * speed, 0);
        //hit = Physics2D.BoxCast(transform.position, bodyBox.size,0,new Vector2(0,)

        if(animator.GetFloat("Speed")==0){
            musicManagement.AudioLoop(3, 0.3f);
        }
        
        if (animator.GetBool("CanMove") &&! isKnockbackActive )
        {
            lastMovement.x= (int)animator.GetFloat("lastMoveX");
            lastMovement.y= (int)animator.GetFloat("lastMoveY");
            if (isDashing)
            {
                rb.velocity = lastMovement.normalized * dashSpeed; 
            }
            else
            {
                rb.velocity = movement.normalized * moveSpeed;
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

    public void SetKnockbackActive(bool value)
    {
        isKnockbackActive = value;
    }


}
