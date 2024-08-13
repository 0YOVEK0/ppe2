using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadCapitulosScene();
        }
    }

    private void LoadCapitulosScene()
    {
        // Asegúrate de que el cursor sea visible al regresar al menú
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Level Complete"); // Carga la escena llamada "Capitulos"
    }
}
