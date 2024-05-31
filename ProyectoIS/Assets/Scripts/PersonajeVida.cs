using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersonajeVida : MonoBehaviour
{
    [SerializeField] int vidas;
    [SerializeField] Slider sliderVidas;

    private void OnCollisionEnter2D(Collision2D otro)
    {
        if (otro.gameObject.CompareTag("Jefe0"))
        {
            vidas = vidas - 10;
            sliderVidas.value = vidas;

            if(vidas <= 0) {
                Destroy(this.gameObject);
                SceneManager.LoadScene(3);

            }
        }

        if (otro.gameObject.CompareTag("Murcielago"))
        {
            vidas = vidas - 5;
            sliderVidas.value = vidas;

            if (vidas <= 0)
            {
                Destroy(this.gameObject);
                SceneManager.LoadScene(3);

            }
        }
    }
}
