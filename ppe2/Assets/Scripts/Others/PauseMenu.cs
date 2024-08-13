using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.M)) // Cambia KeyCode.M por la tecla que prefieras
        {
            LoadMenu();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanuda el tiempo del juego
        isPaused = false;
        Cursor.visible = false; // Oculta el cursor si se reanuda el juego
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausa el tiempo del juego
        isPaused = true;
        Cursor.visible = true; // Muestra el cursor cuando el juego está en pausa
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f; // Asegúrate de que el juego no esté pausado al cargar el menú
        Cursor.visible = true; // Muestra el cursor al cambiar a la escena del menú
        Cursor.lockState = CursorLockMode.None; // Desbloquea el cursor
        SceneManager.LoadScene("Menu");
    }
}
