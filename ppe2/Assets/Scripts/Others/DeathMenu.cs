using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathScreenCanvas; // Referencia al Canvas de la pantalla de muerte

    void Start()
    {
        // Asegurarse de que el Canvas de la pantalla de muerte esté desactivado al inicio
        if (deathScreenCanvas != null)
        {
            deathScreenCanvas.SetActive(false);
        }
    }

    // Método para reiniciar el nivel
    public void RestartLevel()
    {
        // Desactivar la pantalla de muerte
        if (deathScreenCanvas != null)
        {
            deathScreenCanvas.SetActive(false);
        }

        // Reiniciar el nivel actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Método para cargar el menú principal
    public void LoadMainMenu()
    {
        // Desactivar la pantalla de muerte
        if (deathScreenCanvas != null)
        {
            deathScreenCanvas.SetActive(false);
        }

        // Cargar la escena del menú principal
        SceneManager.LoadScene("MainMenu");
    }

    // Método para mostrar la pantalla de muerte y activar el cursor
    public void ShowDeathScreen()
    {
        if (deathScreenCanvas != null)
        {
            deathScreenCanvas.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
