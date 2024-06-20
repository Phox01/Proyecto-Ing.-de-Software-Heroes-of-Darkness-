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
    public GameObject playerInstance;
    public GameObject audioControllerInstance; 

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

            // if (FindObjectOfType<MusicManagement>() == null)
            // {
            //     Instantiate(audioControllerPrefab);
            // }
            audioControllerInstance = Instantiate(audioControllerPrefab);
            DontDestroyOnLoad(audioControllerInstance);
            playerCanvasInstance = Instantiate(playerCanvasPrefab);
            DontDestroyOnLoad(playerCanvasInstance);

            pausaCanvasInstance = Instantiate(pausaCanvasPrefab);
            DontDestroyOnLoad(pausaCanvasInstance);
            inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(inventoryCanvasInstance);

            // if (FindObjectOfType<playerMovement>() == null)
            // {
            //     Instantiate(playerPrefab);
            // }
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance);

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
        else{
            activate();
        }
    }

    private void DestroyInstances()
    {

        //var musicManager = FindObjectOfType<MusicManagement>();
        if (audioControllerInstance != null)
        {
            //Destroy(musicManager.gameObject);
            audioControllerInstance.gameObject.SetActive(false);
        }

        if (playerCanvasInstance != null)
        {
            //Destroy(playerCanvasInstance);
            playerCanvasInstance.gameObject.SetActive(false);
        }

        if (pausaCanvasInstance != null)
        {
            //Destroy(pausaCanvasInstance);
            pausaCanvasInstance.gameObject.SetActive(false);
        }
        if (inventoryCanvasInstance != null)
        {
            //Destroy(inventoryCanvasInstance);
            inventoryCanvasInstance.gameObject.SetActive(false);
        }
        //Destroy(gameObject); 
        //gameObject.SetActive(false);
        
        //var player = FindObjectOfType<playerMovement>();
        if (playerInstance != null)
        {
            //Destroy(player.gameObject);
            playerInstance.gameObject.SetActive(false);
        }
    }
    private void activate()
    {
        
        //gameObject.SetActive(true);
        //var player = FindObjectOfType<playerMovement>();
        
        
        if (playerInstance != null)
        {
            //Destroy(player.gameObject);
            playerInstance.gameObject.SetActive(true);
        }

        //var musicManager = FindObjectOfType<MusicManagement>();
        if (audioControllerInstance != null)
        {
            //Destroy(musicManager.gameObject);
            audioControllerInstance.gameObject.SetActive(true);
        }

        if (playerCanvasInstance != null)
        {
            //Destroy(playerCanvasInstance);
            playerCanvasInstance.gameObject.SetActive(true);
        }

        if (pausaCanvasInstance != null)
        {
            //Destroy(pausaCanvasInstance);
            pausaCanvasInstance.gameObject.SetActive(true);
        }
        if (inventoryCanvasInstance != null)
        {
            //Destroy(inventoryCanvasInstance);
            inventoryCanvasInstance.gameObject.SetActive(true);
        }
        // if (inventoryCanvasInstance == null)
        // {
        //     inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
        //     DontDestroyOnLoad(playerInstance);
        // }
        if (playerInstance == null)
        {   
            inventoryCanvasInstance = Instantiate(inventoryCanvasPrefab);
            DontDestroyOnLoad(inventoryCanvasInstance);
            playerInstance = Instantiate(playerPrefab);
            DontDestroyOnLoad(playerInstance);
        }
        //Destroy(gameObject); 
    }
    
}
