using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candado : Colisiones
{
    public GameObject llave;
    PS4 controls;

    protected void Awake()
    {
        controls=new PS4();
    }
    protected override void Start()
    {
        base.Start();
        controls.Gameplay.Attack.Enable();
    }

    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(Input.GetAxisRaw("Space") == 1 || controls.Gameplay.Attack.triggered)
            {
                playerMovement playercomp = col.gameObject.GetComponent<playerMovement>();

                if (playercomp.boss1 && playercomp.boss2)
                {
                        llave.SetActive(false);
                }
            }
        }
    }
}

