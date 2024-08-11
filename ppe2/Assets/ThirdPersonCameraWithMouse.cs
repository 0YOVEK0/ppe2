using UnityEngine;

public class ThirdPersonCameraWithMouse : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset; // Offset de la cámara con respecto al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public float mouseSensitivity = 10f; // Sensibilidad del mouse para la rotación
    public float verticalRotationLimit = 80f; // Límite para la rotación vertical

    private float yaw = 0f; // Rotación horizontal
    private float pitch = 0f; // Rotación vertical

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquear el cursor al centro de la pantalla
        Cursor.visible = false; // Hacer el cursor invisible
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Obtener la entrada del mouse
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            // Ajustar la rotación horizontal y vertical
            yaw += mouseX * mouseSensitivity;
            pitch -= mouseY * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -verticalRotationLimit, verticalRotationLimit);

            // Aplicar rotación a la cámara
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);

            // Calcular la posición deseada de la cámara
            Vector3 desiredPosition = player.position + offset;
            // Suavizar el movimiento de la cámara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Actualizar la posición de la cámara
            transform.position = smoothedPosition;
        }
    }
}
