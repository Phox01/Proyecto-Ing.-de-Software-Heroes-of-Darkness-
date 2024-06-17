using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : Eventos
{
    public int typeCheckPoint;

    protected override void checkpointIteraction()
    {
        DataJuego.data.GuardarData();
    }
}
