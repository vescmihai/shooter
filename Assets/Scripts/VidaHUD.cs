using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class VidaHUD : MonoBehaviour
{
    private Text texto;
    private Vida vidaJugador;

    void Start()
    {
        texto = GetComponent<Text>();

        GameObject jugador = GameObject.FindGameObjectWithTag("Player");
        if (jugador != null)
            vidaJugador = jugador.GetComponent<Vida>();
    }

    void Update()
    {
        if (vidaJugador == null) return;

        texto.text = "Vida: " + vidaJugador.VidaActual() + "/" + vidaJugador.vidaMax;
    }
}
