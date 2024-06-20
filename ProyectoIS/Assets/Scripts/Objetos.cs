using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetos : Colisiones //Herencia
{
    public int idObjeto; //Que cada objeto tenga una id distinta
    public int delay;
    private int stopBug;
    protected MusicManagement musicManagement;

    protected override void OnCollide(Collider2D col)
    {
        musicManagement = FindObjectOfType<MusicManagement>();
        if (stopBug == 0)
        {

            if (col.tag == "Player")
            {
                ControladorDeAtaque player = col.gameObject.GetComponent<ControladorDeAtaque>();
                Debug.Log("MrMenu");
                stopBug = 1;
                switch (idObjeto) //Pongan el ID en el script juego EN UNITY
                {
                    case 1: //Que pasa si choca con un objeto de estado 1 para ganar y 0 para perder
                        int x = Random.Range(0, 2);
                        if (x == 1)
                        {
                           //9
                           musicManagement.SeleccionAudio(9, 1f);
                           player.AddHealth(50);
                        }
                        else
                        {
                            //11
                            musicManagement.SeleccionAudio(11, 1f);
                            player.GetDamaged(50);
                        }

                        break;

                    case 2: //Que pasa si choca con una fruta de vida
                        //8
                        musicManagement.SeleccionAudio(8, 1f);
                        player.AddHealth(50);
                        break;

                    case 3: //Que pasa si choca con dinero
                        //10
                        musicManagement.SeleccionAudio(10, 1f);
                        DataJuego.data.dinero += 1;
                        //DataJuego.data.GuardarData();
                        break;

                    case 4: //Que pasa si choca con diamante
                        //10
                        musicManagement.SeleccionAudio(10, 1f);
                        DataJuego.data.dinero += 10;
                        //DataJuego.data.GuardarData();
                        break;

                    case 5: //Que pasa si choca con un objeto de veneno
                        //11
                        musicManagement.SeleccionAudio(11, 1f);
                        player.GetDamaged(50);
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
