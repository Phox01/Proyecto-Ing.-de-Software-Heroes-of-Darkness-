using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventos : Colisiones
{
    public int idEevent = 0;
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
                switch (idEevent)
                {
                    case 1:
                        chestInteraction();
                        break;

                    case 2:
                        checkpointIteraction();
                        break;

                    default:
                        break;
                }
            }
        }
    }

    protected virtual void chestInteraction() {
        Debug.Log("Funciona");
    }

    protected virtual void checkpointIteraction()
    {
        Debug.Log("Funciona");
    }
}
