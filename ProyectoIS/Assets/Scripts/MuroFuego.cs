using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroFuego : MonoBehaviour
{

    public int tiempo=4;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", tiempo);

    }

    // Update is called once per frame
    private void DestroyObject()
    {


        // Destruye el objeto
        
        Destroy(gameObject);
    }
}
