using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Golem : Enemigo
{

    public GameObject[] routePoints;
    public int random;
    public float patrolSpeed;
    private Vector3 previousDirection;
    public float time;
    private bool isMoving;
    public Vector3 targetPosition;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        //routePoints = GameObject.FindGameObjectsWithTag("Point"); Para que el patrullaje solo sea en puntos espec�ficos
        random = Random.Range(0, routePoints.Length);
        patrolSpeed = 3;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (player!=null){// LÓGICA PARA QUE EL ENEMIGO SIEMPRE MIRE AL PERSONAJE PRINCIPAL
        Vector3 direction = player.transform.position - transform.position;
        if ((direction.x >= 0.0f && previousDirection.x < 0.0f) || (direction.x < 0.0f && previousDirection.x >= 0.0f))
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            Vector3 ScalerLifeBar = sliderVidas.transform.localScale;
            ScalerLifeBar.x *= -1;
            sliderVidas.transform.localScale = ScalerLifeBar;
        }
        previousDirection = direction;

        // L�GICA DE HACER EL PATRULLAJE
        transform.position = Vector2.MoveTowards(transform.position, routePoints[random].transform.position, patrolSpeed * Time.deltaTime);
        time += Time.deltaTime;
        if (time >= 2)
        {
            random = Random.Range(0, routePoints.Length);
            time = 0;
        }
        targetPosition = routePoints[random].transform.position;
        if (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            isMoving = true;
            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
        }
    }}


}
