using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagementMenu : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    
    public void Tutorial()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public void TerminarPartida()
    {
        SceneManager.LoadScene(0);

    }

    public void VolverJugar()
    {
        SceneManager.LoadScene(1);

    }

    public void NuevoJuego()
    {
        SceneManager.LoadScene(4);

    }
    public void PartidaGuardada()
    {
        SceneManager.LoadScene(4);

    }

    public void PausarPartida()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void ReanudarPartida()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        PartidaGuardada();
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Cerrar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        TerminarPartida();
    }

    public void Guardar()
    {
        Debug.Log("Aqui va el proceso de guardado");
        DataJuego.data.GuardarData();
    }
 
}
