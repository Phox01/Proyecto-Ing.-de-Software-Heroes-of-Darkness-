using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : Colisiones //Herencia
{
    public int idObjeto; //Que cada objeto tenga una id distinta
    public int delay;
    private int stopBug;

    protected override void OnCollide(Collider2D col)
    {
        if (stopBug == 0)
        {

            if (col.tag == "Player")
            {
                Debug.Log("MrMenu");
                stopBug = 1;
                switch (idObjeto) //Pongan el ID en el script juego EN UNITY
                {
                    case 1: //Que pasa si choca con un objeto de estado 1 para ganar y 0 para perder
                        int x = Random.Range(0, 2);
                        if (x == 1)
                        {
                            DataJuego.data.vida += 25;
                        }
                        else
                        {
                            DataJuego.data.vida -= 25;
                        }

                        break;

                    case 2: //Que pasa si choca con una fruta de vida
                        DataJuego.data.vida += 50;
                        break;

                    case 3: //Que pasa si choca con dinero
                        DataJuego.data.dinero += 1;
                        //DataJuego.data.GuardarData();
                        break;

                    case 4: //Que pasa si choca con diamante
                        DataJuego.data.dinero += 10;
                        //DataJuego.data.GuardarData();
                        break;

                    case 5: //Que pasa si choca con un objeto de magia
                        DataJuego.data.vida -= 20;
                        break;


                    default:
                        break;
                }

                if (delay == 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(waitTime());
                }
            }
        }
    }

        

    public IEnumerator waitTime()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
