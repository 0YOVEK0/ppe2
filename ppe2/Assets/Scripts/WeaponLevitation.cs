using UnityEngine;

public class WeaponLevitation : MonoBehaviour
{
    // Variables para ajustar la intensidad del temblor
    public float positionShakeIntensity = 0.05f;
    public float rotationShakeIntensity = 2f;
    public float shakeSpeed = 1f;

    // Posición y rotación originales
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    void Start()
    {
        // Guardamos la posición y rotación originales del arma
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    void Update()
    {
        // Calculamos una pequeña variación aleatoria para la posición
        Vector3 positionOffset = new Vector3(
            Mathf.PerlinNoise(Time.time * shakeSpeed, 0) * 2 - 1,
            Mathf.PerlinNoise(0, Time.time * shakeSpeed) * 2 - 1,
            Mathf.PerlinNoise(Time.time * shakeSpeed, Time.time * shakeSpeed) * 2 - 1
        ) * positionShakeIntensity;

        // Calculamos una pequeña variación aleatoria para la rotación
        Vector3 rotationOffset = new Vector3(
            Mathf.PerlinNoise(Time.time * shakeSpeed + 1, 0) * 2 - 1,
            Mathf.PerlinNoise(0, Time.time * shakeSpeed + 1) * 2 - 1,
            Mathf.PerlinNoise(Time.time * shakeSpeed + 1, Time.time * shakeSpeed + 1) * 2 - 1
        ) * rotationShakeIntensity;

        // Aplicamos las variaciones a la posición y rotación del arma
        transform.localPosition = originalPosition + positionOffset;
        transform.localRotation = originalRotation * Quaternion.Euler(rotationOffset);
    }
}
