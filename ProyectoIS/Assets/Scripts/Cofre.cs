using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : Eventos
{
    public int typeChest;
    //private RaycastHit2D hit;
    public List<GameObject> objetos = new List<GameObject>();

    //void FixedUpdate() { 
    //    hit = Physics2D.BoxCast(transform.position,bod);
    //}
    protected override void chestInteraction()
    {
        if (typeChest == 0) { //Cofre aleatorio
            int x = Random.Range(0, objetos.Count);
            Instantiate(objetos[x], transform.position, transform.rotation); //lo que esta en lista, posicion que se genere el objeto, rotacion del objeto


        }
        else
        { //Cofre Especifico
            Instantiate(objetos[typeChest], transform.position, transform.rotation); //Cofre especifico

        }
    }

}
