using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Eventos
{
    public int typeCheckPoint;
    //[SerializeField] private GameObject panelConfirmacion;

    protected override void checkpointIteraction()
    {
        if (typeCheckPoint == 2)
        {
            //panelConfirmacion.SetActive(true);
            //DANIEL PASA EL PREFAB DEL MENU DE PAUSA PARA PONERLO COMO CONFIRMACIÓN EN EL PUNTO DE GUARDADO
            DataJuego.data.GuardarData();
            Debug.Log("Guardado El progreso!");
        }
    }
}
