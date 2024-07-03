using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiones : MonoBehaviour
{
    public ContactFilter2D filtro;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10]; //Numero de colisiones a la vez


    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); //Agarra el boxColider

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        boxCollider.OverlapCollider(filtro, hits); //Devolver en forma de inforamcion
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null){
            continue; //Llega aqui continua
        }
            OnCollide(hits[i]);
            hits[i] = null; //Limpia cache
        }
    }
        protected virtual void OnCollide(Collider2D col)
    {
        
    }
}
