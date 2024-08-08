using UnityEngine;
using UnityEngine.UI; // Usar si estás utilizando UI Text
// using TMPro; // Usar si estás utilizando TextMeshPro

public class DisableTextAfterTime : MonoBehaviour
{
    public float timeToDisable = 60f; // Tiempo en segundos (1 minuto)

    private void Start()
    {
        // Llama a la función DisableText después del tiempo especificado
        Invoke("DisableText", timeToDisable);
    }

    private void DisableText()
    {
        // Desactiva el GameObject que contiene el componente Text o TextMeshProUGUI
        gameObject.SetActive(false);
    }
}
