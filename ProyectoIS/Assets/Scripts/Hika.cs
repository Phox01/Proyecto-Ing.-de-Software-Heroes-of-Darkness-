using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAction : MonoBehaviour, IPointerClickHandler
{
    // Esta función se llama cuando se hace clic en el objeto de la imagen
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Hola"); // Imprime "Hola" en la consola
    }
}
