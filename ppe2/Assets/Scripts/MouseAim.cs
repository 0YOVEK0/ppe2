using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public Camera mainCamera;  // La cámara principal
    public Transform playerBody;  // El cuerpo del jugador o el objeto que debe rotar

    void Update()
    {
        AimTowardsMouse();
    }

    void AimTowardsMouse()
    {
        // Crear un plano a la altura del jugador
        Plane playerPlane = new Plane(Vector3.up, playerBody.position);

        // Crear un rayo desde la posición de la cámara hacia el mouse
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Verificar dónde el rayo intersecta con el plano del jugador
        if (playerPlane.Raycast(ray, out float hitDist))
        {
            // Obtener el punto de intersección
            Vector3 targetPoint = ray.GetPoint(hitDist);

            // Calcular la dirección hacia el punto de intersección
            Vector3 direction = (targetPoint - playerBody.position).normalized;

            // Rotar el cuerpo del jugador hacia esa dirección
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            playerBody.rotation = Quaternion.Slerp(playerBody.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}
