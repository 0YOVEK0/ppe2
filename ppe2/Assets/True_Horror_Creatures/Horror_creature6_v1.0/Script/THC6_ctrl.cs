using UnityEngine;
using System.Collections;

public class THC6_ctrl : MonoBehaviour {

    private Animator anim;
    private CharacterController controller;
    private int battle_state = 0;
    public float speed = 6.0f;
    public float runSpeed = 3.0f;
    public float turnSpeed = 60.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float w_sp = 0.0f;
    private float r_sp = 0.0f;

    public Transform player; // Referencia al jugador
    public float chaseRange = 10.0f; // Rango de persecución

    public int maxHealth = 100; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo

    void Start () 
    {                       
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController> ();
        w_sp = speed; //read walk speed
        r_sp = runSpeed; //read run speed
        battle_state = 0;
        runSpeed = 1;
        currentHealth = maxHealth; // Inicializar la vida del enemigo
    }

    void Update () 
    {       
        if (currentHealth <= 0) 
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < chaseRange) 
        {
            // Perseguir al jugador
            anim.SetInteger("battle", 1);
            anim.SetInteger("moving", 1); //walk/run/moving
            battle_state = 1;
            runSpeed = r_sp;

            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Mantener al enemigo en el plano XZ
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * turnSpeed);

            if (controller.isGrounded) 
            {
                moveDirection = transform.forward * speed * runSpeed;
            }
        }
        else
        {
            // Dejar de perseguir al jugador
            anim.SetInteger("battle", 0);
            anim.SetInteger("moving", 0); // idle
            battle_state = 0;
            runSpeed = 1;
            moveDirection = Vector3.zero;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void TakeDamage(int damage) 
    {
        currentHealth -= damage;
        if (currentHealth <= 0) 
        {
            Die();
        }
    }

    private void Die() 
    {
        anim.SetInteger("moving", 13); // Animación de morir
        // Aquí puedes agregar lógica adicional para cuando el enemigo muera
        // Desactivar el enemigo después de un corto tiempo
        Destroy(gameObject, 3f); // Destruir el objeto después de 3 segundos
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStats>().TakeDamage(10); // Ajusta el daño según sea necesario
        }
    }
}
