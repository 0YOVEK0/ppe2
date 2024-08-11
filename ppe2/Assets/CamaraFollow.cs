using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform player;  // El objeto del jugador
    public Vector3 offset;  // Offset para ajustar la posici�n de la c�mara

    void LateUpdate()
    {
        // La posici�n deseada de la c�mara es la posici�n del jugador con el offset aplicado
        Vector3 desiredPosition = player.position + offset;
        // Actualiza la posici�n de la c�mara sin suavizado
        transform.position = desiredPosition;
    }
}
