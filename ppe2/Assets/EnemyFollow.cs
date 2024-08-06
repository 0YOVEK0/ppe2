using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public string playerTag = "Player"; 
    public float moveSpeed = 5f; 
    public float followRange = 50f; 
    public float attackRange = 2f; 
    public int attackDamage = 10; 
    public float attackCooldown = 1f; 
    public Animator animator; 
    public float rotationSpeed = 5f; 
    public int health = 100; 
    public int bulletDamage = 25; 

    private Transform target; 
    private float lastAttackTime;
    private bool isDead = false; 

    public GameCore gameCore; // Referencia al GameCore

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (player != null)
        {
            target = player.transform;
        }

        // Buscar el GameCore en la escena
        if (gameCore == null)
        {
            gameCore = FindObjectOfType<GameCore>();
        }

        Debug.Log("Enemy initialized. Health: " + health);
    }

    void Update()
    {
        if (isDead || target == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= followRange)
        {
            RotateTowardsTarget();

            if (distanceToTarget <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
            else
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * moveSpeed * Time.deltaTime;
                animator.SetBool("isRunning", true);
                animator.SetBool("IsAttacking", false);
            }
        }
        else
        {
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
        animator.SetTrigger("Attack");
        animator.SetBool("isRunning", false);
        animator.SetBool("IsAttacking", true);

        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            TakeDamage(bulletDamage);
            Debug.Log("Enemy hit by Bullet. Health reduced by " + bulletDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Current health: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;

        Debug.Log("Enemy died.");
        isDead = true;

        if (animator != null)
        {
            animator.SetTrigger("Die");
            Invoke(nameof(Deactivate), 2.03f);
        }
        else
        {
            Deactivate();
        }

        if (gameCore != null)
        {
            gameCore.EnemyKilled();
            Debug.Log("Notifying GameCore of enemy death.");
        }
    }

    public void Disable()
    {
        foreach (var component in GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }
        foreach (var collider in GetComponents<Collider>())
        {
            collider.enabled = false;
        }
        foreach (var rigidbody in GetComponents<Rigidbody>())
        {
            rigidbody.isKinematic = true;
        }
        gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
