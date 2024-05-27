using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTerminar : MonoBehaviour
{
    public void TerminarPartida()
    {
        SceneManager.LoadScene(0);

    }
}
