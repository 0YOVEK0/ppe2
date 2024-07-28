using UnityEngine;

public class ComputerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public GameObject lorePanel; // El panel que contiene el lore
    public Transform player; // El transform del jugador
    public AudioSource loreAudio; // El componente AudioSource con la narración del lore

    private bool isPlayerNearby = false;

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
            lorePanel.SetActive(!lorePanel.activeSelf);
            
            // Reproducir o detener el audio del lore
            if (lorePanel.activeSelf)
            {
                loreAudio.Play();
            }
            else
            {
                loreAudio.Stop();
            }
        }
    }
}
