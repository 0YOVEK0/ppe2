using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform target; // El objetivo a seguir (tu personaje)
    public float moveSpeed = 5f; // Velocidad de movimiento del enemigo
    public float followRange = Mathf.Infinity; // Rango de seguimiento, se puede modificar en el inspector

    void Update()
    {
        // Calcula la distancia entre el enemigo y el objetivo
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Si la distancia es menor o igual al rango de seguimiento, sigue al objetivo
        if (distanceToTarget <= followRange)
        {
            // Direccion hacia el objetivo
            Vector3 direction = (target.position - transform.position).normalized;

            // Mueve al enemigo hacia el objetivo
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }
}
