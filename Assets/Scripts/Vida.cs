using UnityEngine;

public class Vida : MonoBehaviour
{

    public int vidaMax = 3;
    public bool esJugador = false;
    public AudioClip sonidoDano; // sonido al recibir dano (jugador_dano o enemigo_dano)
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMax;
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;

        // Sonido de dano (se reproduce en la posicion del personaje,
        // asi sigue sonando aunque el objeto se destruya al morir)
        if (sonidoDano != null)
            AudioSource.PlayClipAtPoint(sonidoDano, transform.position);

        if (esJugador)
        {
            DanoPantalla efecto = FindFirstObjectByType<DanoPantalla>();
            if (efecto != null) efecto.Parpadear();

            PrimeraPersona movimiento = GetComponent<PrimeraPersona>();
            if (movimiento != null) movimiento.Ralentizar();
        }

        if (vidaActual <= 0) Morir();
    }

    void Morir()
    {
        if (esJugador)
        {
            // Muestra el menu de Game Over 
            MenuGameOver menu = FindFirstObjectByType<MenuGameOver>();
            if (menu != null) menu.Mostrar();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Cura al personaje sin pasar el maximo.
    // Devuelve true si realmente curo algo (false si ya estaba lleno).
    public bool Curar(int cantidad)
    {
        if (vidaActual >= vidaMax) return false;

        vidaActual += cantidad;
        if (vidaActual > vidaMax) vidaActual = vidaMax;
        return true;
    }

    public int VidaActual()
    {
        return vidaActual;
    }
}