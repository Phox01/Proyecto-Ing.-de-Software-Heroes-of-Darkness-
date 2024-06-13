using UnityEngine;

public class InitializeGameManager : MonoBehaviour
{
    public GameObject gameManagerPrefab;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}
