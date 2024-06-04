using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagementMenu : MonoBehaviour
{
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
}
