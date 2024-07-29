using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public int enemiesToKill = 5; // La cantidad de enemigos que el jugador necesita matar
    private int currentKills = 0;
    public GameObject door; // La puerta que se abrirá

    // Este método debe llamarse cada vez que un enemigo es matado
    public void EnemyKilled()
    {
        currentKills++;
        if (currentKills >= enemiesToKill)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        // Aquí puedes desactivar el collider de la puerta para abrirla
        door.GetComponent<Collider>().enabled = false;
        // También puedes agregar una animación o cualquier otra lógica para abrir la puerta
    }
}
