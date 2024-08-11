using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI1 : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float delayTime = 3f; // Tiempo de retraso en segundos, editable desde el Inspector
    public float attackRange = 2f; // Rango de ataque en unidades
    public float chaseRange = 10f; // Rango de persecuci�n en unidades
    public float moveSpeed = 3.5f; // Velocidad de movimiento del enemigo, editable desde el Inspector
    public int attackDamage = 10; // Da�o que el enemigo inflige
    public float attackInterval = 1f; // Intervalo entre ataques en segundos

    private NavMeshAgent agent;
    private Animator animator; // Referencia al componente Animator
    private PlayerStats playerStats; // Referencia al componente PlayerStats en el jugador
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator

        // Configurar la velocidad del NavMeshAgent
        agent.speed = moveSpeed;

        // Usa 'delayTime' en lugar de un valor fijo
        Invoke("StartFollowingPlayer", delayTime);

        // Obtener la referencia al componente PlayerStats
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
            if (playerStats == null)
            {
                Debug.LogError("PlayerStats component is missing on the player object!");
            }
        }
    }

    void StartFollowingPlayer()
    {
        // Este m�todo se llama despu�s del tiempo especificado en 'delayTime'
        if (player != null)
        {
            agent.SetDestination(player.position);

            // Cambiar el par�metro de animaci�n para iniciar la persecuci�n
            if (animator != null)
            {
                animator.SetBool("IsChasing", true); // Cambiar el par�metro de animaci�n a correr
            }
        }
    }

    void Update()
    {
        if (player != null && playerStats != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                // En el rango de ataque, cambiar a la animaci�n de ataque
                if (!isAttacking)
                {
                    isAttacking = true;
                    if (animator != null)
                    {
                        animator.SetBool("IsAttacking", true);
                    }
                    StartCoroutine(AttackPlayer());
                }
            }
            else if (distanceToPlayer <= chaseRange)
            {
                // Fuera del rango de ataque pero dentro del rango de persecuci�n, continuar persiguiendo
                if (isAttacking)
                {
                    isAttacking = false;
                    if (animator != null)
                    {
                        animator.SetBool("IsAttacking", false);
                    }
                }

                agent.SetDestination(player.position);
            }
            else
            {
                // Fuera del rango de persecuci�n, detener el movimiento
                if (animator != null)
                {
                    animator.SetBool("IsChasing", false);
                }
                agent.ResetPath();
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        while (isAttacking)
        {
            // Hacer da�o constante al escudo primero
            playerStats.ConsumeEnergy(attackDamage);
            if (playerStats.currentEnergy <= 0)
            {
                int remainingDamage = attackDamage - (int)playerStats.currentEnergy;
                playerStats.TakeDamage(remainingDamage);
            }
            yield return new WaitForSeconds(attackInterval);
        }
    }

    void OnDrawGizmos()
    {
        // Dibujar el rango de ataque en la vista de escena
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Dibujar el rango de persecuci�n en la vista de escena
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
