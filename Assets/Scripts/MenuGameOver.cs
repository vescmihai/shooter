using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuGameOver : MonoBehaviour
{
    public GameObject panel;         // panel de Game Over 
    public GameObject panelVictoria; // panel de Victoria 

    void Start()
    {
        if (panel != null) panel.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);
    }

    // Llamado desde Vida.cs cuando el jugador muere
    public void Mostrar()
    {
        if (panel != null) panel.SetActive(true);
        Pausar();
    }

    // Llamado desde Meta.cs cuando el jugador gana
    public void MostrarVictoria()
    {
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Pausar();
    }

    void Pausar()
    {
        Time.timeScale = 0f;                    // pausa el juego
        Cursor.lockState = CursorLockMode.None; // libera el cursor
        Cursor.visible = true;
    }

    public void Reintentar()
    {
        Time.timeScale = 1f; // reanuda el tiempo antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
