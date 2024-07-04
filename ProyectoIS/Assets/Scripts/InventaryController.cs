using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaryController : MonoBehaviour
{

    [SerializeField] InvetaryPage inventoryUI;
    // Start is called before the first frame update
    public int numeroInv=10;
    private void Start()
    {
        inventoryUI.InitilizeInventoryUi(numeroInv);
    }
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (inventoryUI.isActiveAndEnabled==false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
       
    }
}
