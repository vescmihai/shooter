using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ContadorEnemigos : MonoBehaviour
{
    private Text texto;

    void Start()
    {
        texto = GetComponent<Text>();
    }

    void Update()
    {
        int vivos = FindObjectsByType<Enemigo>(FindObjectsSortMode.None).Length;
        texto.text = "Enemigos: " + vivos;
    }
}
