using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class DanoPantalla : MonoBehaviour
{
    public float alfaInicial = 0.5f;     
    public float velocidadFade = 2f;      

    private Image imagen;
    private float alfa = 0f;

    void Start()
    {
        imagen = GetComponent<Image>();
        AplicarAlfa();
    }

    // Llamado desde Vida.cs cuando el jugador recibe dano
    public void Parpadear()
    {
        alfa = alfaInicial;
        AplicarAlfa();
    }

    void Update()
    {
        // Desvanece el rojo poco a poco hasta volver a transparente
        if (alfa > 0f)
        {
            alfa -= velocidadFade * Time.deltaTime;
            if (alfa < 0f) alfa = 0f;
            AplicarAlfa();
        }
    }

    void AplicarAlfa()
    {
        Color c = imagen.color;
        c.a = alfa;
        imagen.color = c;
    }
}
