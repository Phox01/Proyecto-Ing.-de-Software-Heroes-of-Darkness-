using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : Eventos
{
    public int typeChest;
    public int usado;
    public List<GameObject> objetos = new List<GameObject>();
    public Animator anim;

    protected override void chestInteraction()
    {
        Debug.Log("No Entro");
        if (usado == 0)
        {
            Debug.Log("Entro");
            usado = 1;
            anim.SetInteger("Usado", usado);
            if (typeChest == 0)
            { //Cofre aleatorio
                int x = Random.Range(0, objetos.Count);
                Instantiate(objetos[x], transform.position, transform.rotation); //lo que esta en lista, posicion que se genere el objeto, rotacion del objeto


            }
            else
            { //Cofre Especifico
                Instantiate(objetos[typeChest], transform.position, transform.rotation); //Cofre especifico

            }
        }

    }

}
