using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Método para cargar una escena cuando se hace clic en el primer botón
    public void OnButton1Click()
    {
        SceneManager.LoadScene("Tuto");
    }

    // Método para cargar una escena cuando se hace clic en el segundo botón
    public void OnButton2Click()
    {
        Application.Quit();
        Debug.Log("Se hizo clic en el 2bottom botón.");
    }

    // Método para realizar una acción cuando se hace clic en el tercer botón
    public void OnButton3Click()
    {
        SceneManager.LoadScene("Credits");
    }
    public void OnButton4Click()
    {
        SceneManager.LoadScene("TheForbidenLand");
    }
}
