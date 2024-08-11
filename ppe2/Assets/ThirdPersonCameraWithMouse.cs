using UnityEngine;

public class ThirdPersonCameraWithMouse : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset; // Offset de la c�mara con respecto al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado
    public float mouseSensitivity = 10f; // Sensibilidad del mouse para la rotaci�n
    public float verticalRotationLimit = 80f; // L�mite para la rotaci�n vertical

    private float yaw = 0f; // Rotaci�n horizontal
    private float pitch = 0f; // Rotaci�n vertical

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

            // Ajustar la rotaci�n horizontal y vertical
            yaw += mouseX * mouseSensitivity;
            pitch -= mouseY * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -verticalRotationLimit, verticalRotationLimit);

            // Aplicar rotaci�n a la c�mara
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);

            // Calcular la posici�n deseada de la c�mara
            Vector3 desiredPosition = player.position + offset;
            // Suavizar el movimiento de la c�mara
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // Actualizar la posici�n de la c�mara
            transform.position = smoothedPosition;
        }
    }
}
