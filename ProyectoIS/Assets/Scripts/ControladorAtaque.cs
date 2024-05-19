using UnityEngine;

public class ControladorDeAtaque : MonoBehaviour
{
    public LayerMask capaEnemigos;
    public float rangoAtaque = 1f;
    public float anguloAbanico = 90f; // El ángulo del abanico en grados
    private int ataqueJugador;
    private Transform jugadorTransform;

    void Start()
    {
        capaEnemigos = LayerMask.GetMask("Enemigo");
        Atributos atributosJugador = GetComponent<Atributos>();
        if (atributosJugador != null)
        {
            ataqueJugador = atributosJugador.ataque;
        }
        jugadorTransform = transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(jugadorTransform.position, rangoAtaque, capaEnemigos);

            foreach (Collider2D collider in colliders)
            {
                Vector2 direccionEnemigo = (collider.transform.position - jugadorTransform.position).normalized;
                float angulo = Vector2.Angle(jugadorTransform.up, direccionEnemigo);

                if (angulo <= anguloAbanico / 2f)
                {
                    Atributos atributosEnemigo = collider.GetComponent<Atributos>();
                    if (atributosEnemigo != null)
                    {
                        int dano = ataqueJugador + Random.Range(-3, 4);
                        atributosEnemigo.vida -= dano;
                        if (atributosEnemigo.vida <= 0)
                        {
                            Destroy(collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);

        Vector3 directionRight = Quaternion.AngleAxis(anguloAbanico / 2f, Vector3.forward) * transform.up;
        Vector3 directionLeft = Quaternion.AngleAxis(-anguloAbanico / 2f, Vector3.forward) * transform.up;

        Gizmos.DrawLine(transform.position, transform.position + directionRight * rangoAtaque);
        Gizmos.DrawLine(transform.position, transform.position + directionLeft * rangoAtaque);
    }
}
