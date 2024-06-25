using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataJuego : MonoBehaviour
{
    public static DataJuego data;
    private string rutaArchivo;

    public int dinero=100;
    public float estado, vida, magia;

    public float maxEstado = 100f, maxVida = 100f; //Maximo de vida y estado (estado revisar)


    [Serializable]
    class DatosGuardar // Guarda todos los elementos de una 
    {
        public int DTdinero; //Data de las variables 
        public float DTestado, DTvida, DTmagia; //Data de las variables 
    }


    private void Awake()
    {
        rutaArchivo = Application.dataPath + "/data.mrmenu";

        if (data == null)
        {
            data = this;
            DontDestroyOnLoad(gameObject); //Cuando se cambie a otra escena no se destruya la escena
        }
        else if (data != this) // Borrar el objeto si se repite
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CargarData();
    }

    // Update is called once per frame
    public void CargarData()
    {
        if (File.Exists(rutaArchivo)) //Si existe un archivo en esta ruta procedemos
        {
            Debug.Log("Se ha cargado la data del jugador!");

            BinaryFormatter bf = new BinaryFormatter(); //Definir serializado
            FileStream file = File.Open(rutaArchivo, FileMode.Open); //Ruta y modo que queremos abrir

            DatosGuardar dat = (DatosGuardar)bf.Deserialize(file); //Traducir formato binario y le pasamos todo lo que hay en el archivo

            estado = dat.DTestado;
            vida = dat.DTvida;
            dinero = dat.DTdinero;
            magia = dat.DTmagia;

            file.Close();
        }

    }

    public void GuardarData()
    {
        Debug.Log("Se ha guardado la data del jugador");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo); //Crea un archivo en la ruta
        DatosGuardar dat = new DatosGuardar(); //Limpia y crea otros datos para optimizar

        dat.DTestado = estado;
        dat.DTvida = vida;
        dat.DTdinero = dinero;
        dat.DTmagia = magia;

        bf.Serialize(file, dat); //Serializamos a binario 

        file.Close(); //Se cierra por se crea arriba
    }
}
