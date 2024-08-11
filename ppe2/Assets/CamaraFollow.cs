using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    public Transform player;  // El objeto del jugador
    public Vector3 offset;  // Offset para ajustar la posición de la cámara

    void LateUpdate()
    {
        // La posición deseada de la cámara es la posición del jugador con el offset aplicado
        Vector3 desiredPosition = player.position + offset;
        // Actualiza la posición de la cámara sin suavizado
        transform.position = desiredPosition;
    }
}
