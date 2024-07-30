using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject lorePanel; // El panel que contiene el lore
    public Transform player; // El transform del jugador
    public AudioSource loreAudio; // El componente AudioSource con la narración del lore
    public float loreDisplayTime = 5f; // Tiempo en segundos para mostrar el panel

    private bool isPlayerNearby = false;
    private float loreTimer = 0f;

    void Update()
    {
        // Comprobar la distancia entre el jugador y la computadora
        if (Vector3.Distance(player.position, transform.position) < interactionDistance)
        {
            isPlayerNearby = true;
            // Mostrar un mensaje para interactuar (opcional)
            Debug.Log("Press 'E' to interact");
        }
        else
        {
            isPlayerNearby = false;
        }

        // Detectar si el jugador presiona la tecla "E"
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Alternar el estado del panel del lore
            if (!lorePanel.activeSelf)
            {
                lorePanel.SetActive(true);
                loreAudio.Play();
                loreTimer = loreDisplayTime; // Iniciar el temporizador
            }
            else
            {
                lorePanel.SetActive(false);
                loreAudio.Stop();
            }
        }

        // Manejar el temporizador para ocultar el panel
        if (lorePanel.activeSelf)
        {
            loreTimer -= Time.deltaTime;
            if (loreTimer <= 0f)
            {
                lorePanel.SetActive(false);
                loreAudio.Stop();
            }
        }
    }
}
