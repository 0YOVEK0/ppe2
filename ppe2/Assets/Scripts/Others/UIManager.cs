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

    void Update()
    {
        // Verificar si se debe mostrar la pantalla de muerte
        if (deathScreenCanvas.activeSelf)
        {
            // Detectar la tecla P para reiniciar el nivel
            if (Input.GetKeyDown(KeyCode.P))
            {
                ReloadLevel();
            }

            // Detectar la tecla M para cargar el menú principal
            if (Input.GetKeyDown(KeyCode.M))
            {
                LoadMainMenu();
            }
        }
    }

    // Método para volver a cargar el nivel actual
    public void ReloadLevel()
    {
        // Desactivar la pantalla de muerte
        if (deathScreenCanvas != null)
        {
            deathScreenCanvas.SetActive(false);
        }

        // Volver a cargar el nivel actual
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex); // Cambiado a buildIndex para asegurar la correcta carga
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

        // Asegurarse de que el cursor sea visible y desbloqueado
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
