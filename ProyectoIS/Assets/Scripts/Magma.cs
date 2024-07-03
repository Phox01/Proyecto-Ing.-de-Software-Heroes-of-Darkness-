using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magma : MonoBehaviour
{
    private Coroutine holaCoroutine;
    public int damage = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



 // Declare holaCoroutine variable

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            holaCoroutine = StartCoroutine(PrintHolaWhileInContact(other));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (holaCoroutine != null)
            {
                StopCoroutine(holaCoroutine);
                holaCoroutine = null;

                playerMovement slow = other.gameObject.GetComponent<playerMovement>();
                //player.GetDamaged(damage);
                slow.moveSpeed = 6f;
            }
        }
    }

    IEnumerator PrintHolaWhileInContact(Collider2D other)
    {
        while (true)
        {

            ControladorDeAtaque player= other.gameObject.GetComponent<ControladorDeAtaque>();
            playerMovement slow = other.gameObject.GetComponent<playerMovement>();
            player.GetDamaged(damage);
            slow.moveSpeed = 3f;
            Debug.Log("hola");
            yield return new WaitForSeconds(1f);
        }
    }
}



