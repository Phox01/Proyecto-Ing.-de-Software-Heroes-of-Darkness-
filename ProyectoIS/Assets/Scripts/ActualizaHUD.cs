using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActualizaHUD : MonoBehaviour
{
    public Text txtDinero;
    public float vidaTotal;
    //public Slider sliderVidas;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtDinero.text = "" + DataJuego.data.dinero; //Convierte el int en string
        //calcularVida();
    }


    // private void calcularVida()
    // {
    //     vidaTotal = (float)DataJuego.data.vida;
    //     sliderVidas.value = vidaTotal;
    //     UpdateHealthColor();
    // }

    // private void UpdateHealthColor()
    // {
    //     if (vidaTotal > DataJuego.data.maxVida / 2)
    //     {
    //         sliderVidas.fillRect.GetComponent<Image>().color = Color.green;
    //     }
    //     else if (vidaTotal > DataJuego.data.maxVida / 4)
    //     {
    //         sliderVidas.fillRect.GetComponent<Image>().color = Color.yellow;
    //     }
    //     else
    //     {
    //         sliderVidas.fillRect.GetComponent<Image>().color = Color.red;
    //     }
    // }
}
