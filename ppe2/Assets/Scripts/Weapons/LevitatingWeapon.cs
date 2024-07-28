using UnityEngine;

public class LevitatingWeapon : MonoBehaviour
{
    public float levitationHeight = 0.5f; // Altura de la levitación
    public float levitationSpeed = 1f; // Velocidad de la levitación
    public float rotationSpeed = 50f; // Velocidad de rotación

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimiento de levitación
        float newY = startPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotación del objeto
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

