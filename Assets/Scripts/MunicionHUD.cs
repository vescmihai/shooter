using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MunicionHUD : MonoBehaviour
{
    private Text texto;
    private Disparar disparar;

    void Start()
    {
        texto = GetComponent<Text>();
        disparar = FindFirstObjectByType<Disparar>();
    }

    void Update()
    {
        if (disparar == null) return;

        if (disparar.Recargando())
            texto.text = "RECARGANDO...";
        else if (disparar.BalasActuales() == 0 && disparar.BalasReserva() == 0)
            texto.text = "SIN MUNICION";
        else
            texto.text = "Balas: " + disparar.BalasActuales() + "/" + disparar.BalasMax()
                       + "  Reserva: " + disparar.BalasReserva();
    }
}