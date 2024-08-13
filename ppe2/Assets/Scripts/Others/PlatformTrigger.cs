using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformTrigger : MonoBehaviour
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
        SceneManager.LoadScene("navmeshtest"); // Asegúrate de que el nombre del menú principal es correcto
    }
}
