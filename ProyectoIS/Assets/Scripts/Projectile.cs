using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float speed = 6f;
    public int damage;
    private MusicManagement musicManagement;

    void Start()
    {
        // Encuentra la referencia a MusicManagement
        musicManagement = FindObjectOfType<MusicManagement>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemigo enemigoComponent = collision.gameObject.GetComponent<Enemigo>();
        if (enemigoComponent != null)
        {
            enemigoComponent.GetDamaged(damage);
            musicManagement.SeleccionAudio(5, 1f);
        }
        
        Destroy(gameObject);
    }
}
