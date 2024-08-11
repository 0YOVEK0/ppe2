using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float delayTime = 3f; // Tiempo de retraso en segundos, editable desde el Inspector
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Usa 'delayTime' en lugar de un valor fijo
        Invoke("StartFollowingPlayer", delayTime);
    }

    void StartFollowingPlayer()
    {
        // Este método se llama después del tiempo especificado en 'delayTime'
        if (player != null)
        {
            agent.SetDestination(player.position);
        }
    }

    void Update()
    {
        // Moverse hacia el jugador si se ha llamado a StartFollowingPlayer
        if (agent.hasPath)
        {
            agent.SetDestination(player.position);
        }
    }
}
