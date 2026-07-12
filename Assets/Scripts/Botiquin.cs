using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Botiquin : MonoBehaviour
{
    public int curacion = 1;        // cuanta vida recupera
    public AudioClip sonidoCurar;

    void OnTriggerEnter(Collider other)
    {
        // Solo reacciona al jugador
        if (!other.CompareTag("Player")) return;

        Vida vida = other.GetComponentInParent<Vida>();
        if (vida == null) return;

        // Curar devuelve false si la vida ya estaba al maximo
        if (vida.Curar(curacion))
        {
            if (sonidoCurar != null)
                AudioSource.PlayClipAtPoint(sonidoCurar, transform.position);

            Destroy(gameObject);
        }
    }
}
