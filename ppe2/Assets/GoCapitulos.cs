using UnityEngine;
using UnityEngine.SceneManagement;

public class GoCapitulos : MonoBehaviour
{
    public void OnReturnButtonClicked()
    {
        // Asegúrate de que el cursor sea visible al regresar al menú
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Carga la escena del menú
        SceneManager.LoadScene("Menu");
    }
}
