using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera topDownCamera; // C�mara en vista superior
    public Camera thirdPersonCamera; // C�mara en tercera persona
    public KeyCode switchKey = KeyCode.C; // Tecla para alternar c�maras

    private bool isTopDown = true; // Estado inicial, puedes cambiarlo seg�n tu necesidad

    void Start()
    {
        // Aseg�rate de que solo una c�mara est� activa al inicio
        SwitchToTopDown();
    }

    void Update()
    {
        // Alternar c�maras cuando se presione la tecla
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
