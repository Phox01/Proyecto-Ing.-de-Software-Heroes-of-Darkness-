using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cosa : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryController Inventario;
    public int precio;
    
    public TMP_Text texto;
    [SerializeField]

    public ItemSO Item;
    
    private bool Chocando = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Comprar( Inventario);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            texto.gameObject.SetActive(true);

            Chocando = true;


        }
        Debug.Log(Inventario.initialItems[0].quantity);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {


        texto.gameObject.SetActive(false);
        Chocando = false;

    }

    public void Comprar(InventoryController player)
    {
        if (Chocando == true)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                texto.gameObject.SetActive(false);
              
               
                DataJuego.data.dinero -=precio;
                Inventario.inventoryData.AddItem(Item,1);
                Debug.Log(DataJuego.data.dinero);

            }
        }
    }
}
