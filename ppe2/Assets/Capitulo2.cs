using UnityEngine;
using UnityEngine.SceneManagement;

public class Capitulo2 : MonoBehaviour
{
    public void OnReturnButtonClicked()
    {
        // Aseg�rate de que el cursor sea visible al regresar al men�
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Carga la escena del men�
        SceneManager.LoadScene("Navmeshtest");
    }
}
