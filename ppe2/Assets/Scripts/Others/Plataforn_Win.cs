using UnityEngine;
using UnityEngine.SceneManagement;

public class Plataform_Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReturnToMainMenu();
        }
    }

    private void ReturnToMainMenu()
    {
        // Configura el cursor para que sea visible y no esté bloqueado
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Carga la escena llamada "MenuGracias"
        SceneManager.LoadScene("MenuGracias");
    }
}
