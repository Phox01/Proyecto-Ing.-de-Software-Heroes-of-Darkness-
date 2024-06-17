using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocaHielo : MonoBehaviour
{
    // Start is called before the first frame update


    private float timer = 0f;
    public float destroyTime = 1f; // Tiempo de destrucción (en segundos)

   
    void Start()
    {
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    

    private void DestroyObject()
    {
        // Destruye el objeto
        Destroy(gameObject);
    }


}
