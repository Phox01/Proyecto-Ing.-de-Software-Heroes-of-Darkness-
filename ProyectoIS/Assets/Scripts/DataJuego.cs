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

    public int dinero = 100;

    // Referencia al script Atributos
    public Atributos atributos;

    [Serializable]
    class DatosGuardar // Guarda todos los elementos
    {
        public int DTdinero; //Data de las variables 
        public int DThealth, DTattack, DTcurrentHealth, DTcurrentMana, DTmana; //Data de las variables 
        public float DTcritChance, DTcritAttack; //Data de las variables 
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

    public void CargarData()
    {
        atributos = FindObjectOfType<Atributos>();
        if (File.Exists(rutaArchivo)) //Si existe un archivo en esta ruta procedemos
        {
            Debug.Log("Se ha cargado la data del jugador!");

            BinaryFormatter bf = new BinaryFormatter(); //Definir serializado
            FileStream file = File.Open(rutaArchivo, FileMode.Open); //Ruta y modo que queremos abrir

            DatosGuardar dat = (DatosGuardar)bf.Deserialize(file); //Traducir formato binario y le pasamos todo lo que hay en el archivo

            dinero = dat.DTdinero;
            atributos.health = dat.DThealth;
            atributos.attack = dat.DTattack;
            atributos.currentHealth = dat.DTcurrentHealth;
            atributos.currentManá = dat.DTcurrentMana;
            atributos.maná = dat.DTmana;
            atributos.critChance = dat.DTcritChance;
            atributos.critAttack = dat.DTcritAttack;

            file.Close();
        }
         else // Si el archivo no existe, establecer valores por defecto
        {
            Debug.Log("No se encontró archivo de datos, estableciendo valores por defecto.");
            EstablecerValoresPorDefecto();
        }
    }

    public void GuardarData()
    {
        Debug.Log("Se ha guardado la data del jugador");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo); //Crea un archivo en la ruta
        DatosGuardar dat = new DatosGuardar(); //Limpia y crea otros datos para optimizar

        dat.DTdinero = dinero;
        dat.DThealth = atributos.health;
        dat.DTattack = atributos.attack;
        dat.DTcurrentHealth = atributos.currentHealth;
        dat.DTcurrentMana = atributos.currentManá;
        dat.DTmana = atributos.maná;
        dat.DTcritChance = atributos.critChance;
        dat.DTcritAttack = atributos.critAttack;

        bf.Serialize(file, dat); //Serializamos a binario 

        file.Close(); //Se cierra por se crea arriba
    }
     private void EstablecerValoresPorDefecto()
    {
        Debug.Log("A");
        atributos.health = 150;
         Debug.Log(atributos.health);
        atributos.attack = 20;
        atributos.currentHealth = 150;
        atributos.currentManá = 30;
        atributos.maná = 30;
        atributos.critChance = 0.5f;
        atributos.critAttack = 1.5f;
    }
}
