using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public GameObject audioControllerPrefab; 
    public GameObject playerCanvasPrefab;
    public GameObject pausaCanvasPrefab;
    public GameObject inventoryCanvasPrefab;

    private GameObject playerCanvasInstance;
    private GameObject pausaCanvasInstance;
    private GameObject inventoryCanvasInstance;

    private void Awake()
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

            playerCanvasInstance = Instantiate(playerCanvasPrefab);
            DontDestroyOnLoad(playerCanvasInstance);

            pausaCanvasInstance = Instantiate(pausaCanvasPrefab);
            DontDestroyOnLoad(pausaCanvasInstance);
            inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(inventoryCanvasInstance);

            if (FindObjectOfType<playerMovement>() == null)
            {
                Instantiate(playerPrefab);
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0|| scene.buildIndex == 2 || scene.buildIndex == 3 || scene.buildIndex == 5) 
        {
            DestroyInstances();
        }
    }

    private void DestroyInstances()
    {
        var player = FindObjectOfType<playerMovement>();
        if (player != null)
        {
            Destroy(player.gameObject);
        }

        var musicManager = FindObjectOfType<MusicManagement>();
        if (musicManager != null)
        {
            Destroy(musicManager.gameObject);
        }

        if (playerCanvasInstance != null)
        {
            Destroy(playerCanvasInstance);
        }

        if (pausaCanvasInstance != null)
        {
            Destroy(pausaCanvasInstance);
        }
        if (inventoryCanvasInstance != null)
        {
            Destroy(inventoryCanvasInstance);
        }
        Destroy(gameObject); 
    }
    
}
