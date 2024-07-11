using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class DataJuego : MonoBehaviour
{
    public static DataJuego data;
    private string rutaArchivo;

    public int dinero = 100;

    // Referencia al script Atributos
    public Atributos atributos;
    public playerMovement jefes;

    [Serializable]
    class DatosGuardar // Guarda todos los elementos
    {
        public int DTdinero; //Data de las variables 
        public int DThealth, DTattack, DTcurrentHealth, DTcurrentMana, DTmana; //Data de las variables 
        public float DTcritChance, DTcritAttack; //Data de las variables 
        public bool DTjefe1, DTjefe2;
        public List<SerializableInventoryItem> inventoryItems;
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
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    atributos = FindObjectOfType<Atributos>();
    jefes = FindObjectOfType<playerMovement>();
    InventoryController inventoryController = FindObjectOfType<InventoryController>();
    
    if (currentSceneIndex == 1)
    {
        EstablecerValoresPorDefecto();
    }
    else if (File.Exists(rutaArchivo)) //Si existe un archivo en esta ruta procedemos
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
        jefes.boss1 = dat.DTjefe1;
        jefes.boss2 = dat.DTjefe2;

        // Load inventory items
        inventoryController.inventoryData.Initialize();
        foreach (var item in dat.inventoryItems)
        {
            ItemSO itemSO = GetItemSOByID(item.itemID); // Implement this method to find the ItemSO by ID
            inventoryController.inventoryData.AddItem(itemSO, item.quantity);
        }

        file.Close();
    }
    else // Si el archivo no existe, establecer valores por defecto
    {
        Debug.Log("No se encontró archivo de datos, estableciendo valores por defecto.");
        EstablecerValoresPorDefecto();
    }
}

private ItemSO GetItemSOByID(int id)
{
    // Assuming you have a list of all ItemSO objects
    ItemSO[] allItems = Resources.FindObjectsOfTypeAll<ItemSO>();
    foreach (ItemSO item in allItems)
    {
        if (item.ID == id)
        {
            return item;
        }
    }
    Debug.LogWarning("ItemSO with ID " + id + " not found.");
    return null;
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
    dat.DTjefe1 = jefes.boss1;
    dat.DTjefe2 = jefes.boss2;

    // Save inventory items
    InventoryController inventoryController = FindObjectOfType<InventoryController>();
    dat.inventoryItems = new List<SerializableInventoryItem>();
    foreach (var item in inventoryController.inventoryData.GetCurrentInventoryState())
    {
        SerializableInventoryItem serializableItem = new SerializableInventoryItem
        {
            itemID = item.Value.item.ID,
            quantity = item.Value.quantity
        };
        dat.inventoryItems.Add(serializableItem);
    }

    bf.Serialize(file, dat); //Serializamos a binario 

    file.Close(); //Se cierra por se crea arriba
}

     private void EstablecerValoresPorDefecto()
    {
        atributos.health = 150;
        atributos.attack = 20;
        atributos.currentHealth = 150;
        atributos.currentManá = 30;
        atributos.maná = 30;
        atributos.critChance = 0.5f;
        atributos.critAttack = 1.5f;
        jefes.boss1 = false;
        jefes.boss2 = false;
    }
    [Serializable]
class SerializableInventoryItem
{
    public int itemID;
    public int quantity;
}
}

