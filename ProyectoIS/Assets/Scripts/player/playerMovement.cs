using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int moveDuo = 1;
    PS4 controls;
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

    public static playerMovement instance;
    public int ActualScene;
    public int PreviousScene;

    private bool comprobar = false;
    private bool speedMod = false;
    public bool boss1;
    public bool boss2;

 

    private void Awake(){
        controls= new PS4();
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
        controls.Gameplay.Dash.Enable();
        controls.Gameplay.Pausa.Enable();
    }
    void OnEnable(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnDestroy(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
     private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TeleportPosition();
    }

     private void TeleportPosition()
    { 
        GameObject[] puntos = GameObject.FindGameObjectsWithTag("Teleport");
        foreach (GameObject punto in puntos)
        {
            Teleportation teleportPoint = punto.GetComponent<Teleportation>();
            if (teleportPoint != null && teleportPoint.ActualScene == ActualScene && teleportPoint.PreviousScene == PreviousScene)
            {
                transform.position = punto.transform.position;
                Debug.Log("Teleported to matching teleport point: " + punto.name);
                return;
            }
        }

        //Debug.LogWarning("No matching teleport point found for ActualScene: " + ActualScene + " and PreviousScene: " + PreviousScene);
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
            
            if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || controls.Gameplay.Dash.triggered) && Time.time > lastDashTime + dashCooldown)
            {
                StartDash();
            }
        }

        if (isDashing && Time.time >= dashTime)
        {
            EndDash();
        }
        
        HandleEscapeKey();


    }

    void FixedUpdate()
    {

        if (animator.GetFloat("Speed")==0){
            //musicManagement.AudioLoop(3, 0.3f);
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

    public void ChangeScene(int NewActual, int OldActual){
        ActualScene = NewActual;
        PreviousScene = OldActual;
    }

    private void HandleEscapeKey()
    {
    if (Input.GetKeyDown(KeyCode.Escape) || controls.Gameplay.Pausa.triggered)
    {
        if (managementMenu != null)
        {
            if (comprobar)
            {
                comprobar = false;
                managementMenu.ReanudarPartida();
                }
            else
            {
                comprobar = true;
                managementMenu.PausarPartida();
                }
            }
        }
    }


    public void Velocity(float value)
    {
        
        if(!speedMod){moveSpeed +=value;
       
        StartCoroutine(VolverNormalVelocidad(value));
        speedMod=true;
        }

    }



    public IEnumerator VolverNormalVelocidad(float value)

    {
        
        yield return new WaitForSeconds(10f);
        moveSpeed -=value;
        speedMod= false;
        

    }
}
