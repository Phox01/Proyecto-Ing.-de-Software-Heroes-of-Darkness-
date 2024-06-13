using UnityEngine;

public class DontDestroyCanvas: MonoBehaviour
{
    public static DontDestroyCanvas instance;
    void Awake(){
        
        DontDestroyOnLoad(gameObject);
    }
}