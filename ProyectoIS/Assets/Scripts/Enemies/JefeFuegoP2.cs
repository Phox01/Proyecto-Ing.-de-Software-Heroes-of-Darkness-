using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFuegoP2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject circleCollider1;
    void Start()
    {
        Invoke("Activar",5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Activar()
    {
        StartCoroutine(ActivateDeactivateCoroutine());
    }

    IEnumerator ActivateDeactivateCoroutine()
    {
        // Activate the object after 5 seconds
        
        circleCollider1.SetActive(true);

        // Wait for another 5 seconds before deactivating
        yield return new WaitForSeconds(1f);
        
        circleCollider1.SetActive(false);
    }
}
