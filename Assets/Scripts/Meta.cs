using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Meta : MonoBehaviour
{
    public AudioClip sonidoVictoria;

    private bool terminado = false;

    void OnTriggerEnter(Collider other)
    {
        if (terminado) return;

        // Solo reacciona al jugador
        if (!other.CompareTag("Player")) return;

        // Cuenta los enemigos que quedan en la escena
        int enemigosVivos = FindObjectsByType<Enemigo>(FindObjectsSortMode.None).Length;

        if (enemigosVivos == 0)
        {
            terminado = true;

            if (sonidoVictoria != null)
                AudioSource.PlayClipAtPoint(sonidoVictoria, transform.position);

            MenuGameOver menu = FindFirstObjectByType<MenuGameOver>();
            if (menu != null) menu.MostrarVictoria();
        }
        else
        {
            Debug.Log("Faltan eliminar " + enemigosVivos + " enemigos");
        }
    }
}
