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
    private GameObject playerInstance;
    private GameObject audioControllerInstance;

    private GameObject playerCanvasInstance;
    private GameObject pausaCanvasInstance;
    private GameObject inventoryCanvasInstance;
    private bool init = false;

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
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    public void Init()
    {

        if (audioControllerInstance == null)
        {
            audioControllerInstance = Instantiate(audioControllerPrefab);
            DontDestroyOnLoad(audioControllerInstance);
        }
        if (playerCanvasInstance == null)
        {
            playerCanvasInstance = Instantiate(playerCanvasPrefab);
            DontDestroyOnLoad(playerCanvasInstance);
        }
        if (pausaCanvasInstance == null)
        {
            pausaCanvasInstance = Instantiate(pausaCanvasPrefab);
            DontDestroyOnLoad(pausaCanvasInstance);
        }
        if (inventoryCanvasInstance == null)
        {
            inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(inventoryCanvasInstance);
        }
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance);
        }

        DataJuego.data.CargarData();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0  || scene.buildIndex == 2 || scene.buildIndex == 3 || scene.buildIndex == 5)
        {
            DestroyInstances();
        }
        else
        {
            Init();
        }
    }


    private void DestroyInstances()
    {
        if (audioControllerInstance != null)
        {
            Destroy(audioControllerInstance);
            //audioControllerInstance.gameObject.SetActive(false);
        }

        if (playerCanvasInstance != null)
        {
            Destroy(playerCanvasInstance);
            //playerCanvasInstance.gameObject.SetActive(false);
        }

        if (pausaCanvasInstance != null)
        {
            Destroy(pausaCanvasInstance);
            //pausaCanvasInstance.gameObject.SetActive(false);
        }
        if (inventoryCanvasInstance != null)
        {
            Destroy(inventoryCanvasInstance);
            //inventoryCanvasInstance.gameObject.SetActive(false);
        }
        if (playerInstance != null)
        {
            Destroy(playerInstance);
            //playerInstance.gameObject.SetActive(false);
        }
    }
    private void activate()
    {
        if (!init)
        {
            Init();
            init = true;
        }

        if (playerInstance != null)
        {
            playerInstance.gameObject.SetActive(true);
            DataJuego.data.CargarData();
        }

        if (audioControllerInstance != null)
        {
            audioControllerInstance.gameObject.SetActive(true);
        }

        if (playerCanvasInstance != null)
        {
            playerCanvasInstance.gameObject.SetActive(true);
        }

        if (pausaCanvasInstance != null)
        {
            pausaCanvasInstance.gameObject.SetActive(true);
        }
        if (inventoryCanvasInstance != null)
        {
            inventoryCanvasInstance.gameObject.SetActive(true);
        }
        if (playerInstance == null)
        {
            inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(inventoryCanvasInstance);
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance);
        }
    }

}
