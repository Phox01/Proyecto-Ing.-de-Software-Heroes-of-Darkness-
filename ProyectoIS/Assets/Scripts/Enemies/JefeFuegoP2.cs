using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefeFuegoP2 : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject circleCollider1;
    private float timer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Activar();
    }

    private void Activar()
    {
        timer += Time.deltaTime;
        
        if (timer >= 10f)
        {


            StartCoroutine(ActivateDeactivateCoroutine());

            timer = 0f;
           
        }
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
