using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject deathScreenCanvas; // Referencia al Canvas de la pantalla de muerte

    void Start()
    {
        // Asegurarse de que el Canvas de la pantalla de muerte est� desactivado al inicio
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

            // Detectar la tecla M para cargar el men� principal
            if (Input.GetKeyDown(KeyCode.M))
            {
                LoadMainMenu();
            }
        }
    }

    // M�todo para volver a cargar el nivel actual
    public void ReloadLevel()
    {
        // Volver a cargar el nivel actual completamente
         SceneManager.LoadScene("TheForbidenLand");
    }

    // M�todo para cargar el men� principal
    public void LoadMainMenu()
    {
        // Cargar la escena del men� principal
        SceneManager.LoadScene("Menu");
    }

    // M�todo para mostrar la pantalla de muerte y activar el cursor
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
