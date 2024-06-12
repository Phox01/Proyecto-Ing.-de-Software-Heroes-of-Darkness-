using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eventos : Colisiones
{
    public int idEevent = 0;
    
    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Player")
        {
            if(Input.GetAxisRaw("X") == 1)
            {
                switch (idEevent)
                {
                    case 1:
                        chestInteraction();
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
}
