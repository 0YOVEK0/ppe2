using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI1 : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float delayTime = 3f; // Tiempo de retraso en segundos, editable desde el Inspector
    public float attackRange = 2f; // Rango de ataque en unidades
    public float moveSpeed = 3.5f; // Velocidad de movimiento del enemigo, editable desde el Inspector
    private NavMeshAgent agent;
    private Animator animator; // Referencia al componente Animator

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator

        // Configurar la velocidad del NavMeshAgent
        agent.speed = moveSpeed;

        // Usa 'delayTime' en lugar de un valor fijo
        Invoke("StartFollowingPlayer", delayTime);
    }

    void StartFollowingPlayer()
    {
        // Este método se llama después del tiempo especificado en 'delayTime'
        if (player != null)
        {
            agent.SetDestination(player.position);

            // Cambiar el parámetro de animación para iniciar la persecución
            if (animator != null)
            {
                animator.SetBool("IsChasing", true); // Cambiar el parámetro de animación a correr
            }
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                // En el rango de ataque, cambiar a la animación de ataque
                if (animator != null)
                {
                    animator.SetBool("IsAttacking", true);
                }
            }
            else
            {
                // Fuera del rango de ataque, volver a la animación de correr
                if (animator != null)
                {
                    animator.SetBool("IsAttacking", false);
                }
            }

            // Moverse hacia el jugador si se ha llamado a StartFollowingPlayer
            if (agent.hasPath)
            {
                agent.SetDestination(player.position);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Dibujar el rango de ataque en la vista de escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
