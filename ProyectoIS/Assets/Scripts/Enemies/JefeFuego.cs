using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JefeFuego : Enemigo
{
    public List<GameObject> objetos = new List<GameObject>();
    public BolaFuegoAzul projectilePrefab; // Prefab de la bola de fuego
    public float initialLaunchInterval = 2.0f; // Intervalo de lanzamiento inicial en segundos
    private float currentLaunchInterval;

    public JefeFuegoP2 jefe2;
    
    public GameObject dialoguePanel;
    public TextMeshProUGUI textComponent;
    protected override void Start()
    {
        base.Start();
        currentLaunchInterval = initialLaunchInterval;
        StartCoroutine(LanzarBolasFuego());
    }

    private IEnumerator LanzarBolasFuego()
    {
        while (true)
        {
            // Remover objetos nulos (missing) de la lista
            objetos.RemoveAll(item => item == null);

            // Ajustar el intervalo de lanzamiento basado en la cantidad de objetos restantes
            if (objetos.Count > 0)
            {
                currentLaunchInterval = objetos.Count / (1.5f);
            }
            else
            {
                JefeFuegoP2 instanciaJefe2 = Instantiate(jefe2, transform.position, Quaternion.identity);
                instanciaJefe2.dialogue.dialoguePanel = dialoguePanel;
                instanciaJefe2.dialogue.textComponent = textComponent;
                // Destruir al enemigo cuando ya no queden objetos
                Destroy(gameObject);
                yield break; // Terminar la coroutine
            }

            LanzarEnTodasDirecciones();
            yield return new WaitForSeconds(currentLaunchInterval);
        }
    }

    private void LanzarEnTodasDirecciones()
    {
        float speed = 5f * (5f - objetos.Count);

        Vector2[] directions = {
            Vector2.up,
            Vector2.down,
            Vector2.left,
            Vector2.right,
            new Vector2(1, 1).normalized,  // Diagonal arriba derecha
            new Vector2(-1, 1).normalized, // Diagonal arriba izquierda
            new Vector2(1, -1).normalized, // Diagonal abajo derecha
            new Vector2(-1, -1).normalized // Diagonal abajo izquierda
        };

        foreach (Vector2 direction in directions)
        {
            BolaFuegoAzul bolaFuegoAzul = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            bolaFuegoAzul.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}
