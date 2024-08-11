using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera topDownCamera; // Cámara en vista superior
    public Camera thirdPersonCamera; // Cámara en tercera persona
    public KeyCode switchKey = KeyCode.C; // Tecla para alternar cámaras

    private bool isTopDown = true; // Estado inicial, puedes cambiarlo según tu necesidad

    void Start()
    {
        // Asegúrate de que solo una cámara esté activa al inicio
        SwitchToTopDown();
    }

    void Update()
    {
        // Alternar cámaras cuando se presione la tecla
        if (Input.GetKeyDown(switchKey))
        {
            if (isTopDown)
            {
                SwitchToThirdPerson();
            }
            else
            {
                SwitchToTopDown();
            }
        }
    }

    void SwitchToTopDown()
    {
        topDownCamera.gameObject.SetActive(true);
        thirdPersonCamera.gameObject.SetActive(false);
        isTopDown = true;
    }

    void SwitchToThirdPerson()
    {
        topDownCamera.gameObject.SetActive(false);
        thirdPersonCamera.gameObject.SetActive(true);
        isTopDown = false;
    }
}
