using UnityEngine;

public class Girar : MonoBehaviour
{
    public float velocidadGiro = 60f;   // grados por segundo
    public float alturaFlote = 0.25f;   // cuanto sube y baja
    public float velocidadFlote = 2f;   // que tan rapido flota

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        // Giro constante sobre el eje Y
        transform.Rotate(0f, velocidadGiro * Time.deltaTime, 0f);

        // Flote suave con una onda seno
        float y = Mathf.Sin(Time.time * velocidadFlote) * alturaFlote;
        transform.localPosition = posicionInicial + Vector3.up * y;
    }
}