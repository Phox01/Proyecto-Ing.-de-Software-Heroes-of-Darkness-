using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public GameObject audioControllerPrefab; 
    public GameObject playerCanvas;
    public GameObject pausaCanvas;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (FindObjectOfType<MusicManagement>() == null)
            {
                Instantiate(audioControllerPrefab);
            }
            Instantiate(playerCanvas);
            Instantiate(pausaCanvas);
            if (FindObjectOfType<playerMovement>() == null)
            {
                Instantiate(playerPrefab);
            }

        }
    }
}
