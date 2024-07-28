using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target; // El objetivo a seguir (tu personaje)
    public float moveSpeed = 5f; // Velocidad de movimiento del enemigo
    public float followRange = Mathf.Infinity; // Rango de seguimiento, se puede modificar en el inspector
    public float attackRange = 2f; // Rango de ataque del enemigo
    public int attackDamage = 10; // Daño del ataque
    public float attackCooldown = 1f; // Tiempo de espera entre ataques
    public Animator animator; // Referencia al componente Animator
    public float rotationSpeed = 5f; // Velocidad de rotación del enemigo

    private float lastAttackTime;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Intenta obtener el componente Animator
        }
    }

    void Update()
    {
        // Calcula la distancia entre el enemigo y el objetivo
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Si la distancia es menor o igual al rango de seguimiento, sigue al objetivo
        if (distanceToTarget <= followRange)
        {
            // Gira hacia el objetivo
            RotateTowardsTarget();

            // Si la distancia es menor o igual al rango de ataque, ataca
            if (distanceToTarget <= attackRange)
            {
                // Comprueba si el cooldown del ataque ha terminado
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    // Realiza el ataque
                    Attack();
                    // Actualiza el tiempo del último ataque
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                // Dirección hacia el objetivo
                Vector3 direction = (target.position - transform.position).normalized;

                // Mueve al enemigo hacia el objetivo
                transform.position += direction * moveSpeed * Time.deltaTime;

                // Actualiza la animación a "Run"
                animator.SetBool("isRunning", true);
                animator.SetBool("IsAttacking", false);
            }
        }
        else
        {
            // Si no está en rango, detén la animación de correr
            animator.SetBool("isRunning", false);
            animator.SetBool("IsAttacking", false);
        }
    }

    void RotateTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void Attack()
    {
        // Actualiza la animación a "Attack"
        animator.SetTrigger("Attack");
        animator.SetBool("isRunning", false);
        animator.SetBool("IsAttacking", true);

        // Asume que el jugador tiene un componente PlayerHealth
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
