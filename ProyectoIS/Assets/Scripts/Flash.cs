using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlah;
    [SerializeField] private float restoreMatTime =.2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private Enemigo enemigo;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemigo = GetComponent<Enemigo>();
        defaultMat= spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material=whiteFlah;
        yield return new WaitForSeconds(restoreMatTime);
        spriteRenderer.material=defaultMat;
        
    }

}
