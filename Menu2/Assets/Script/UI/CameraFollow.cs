using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Arrastra aquí a tu planta
    [SerializeField] private float smoothSpeed = 0.125f; // Qué tan suave es el movimiento
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10); // Mantener la cámara atrás

    void LateUpdate()
    {
        if (target != null)
        {
            // Posición a la que queremos llegar
            Vector3 desiredPosition = target.position + offset;

            // Suavizamos el movimiento de la posición actual a la deseada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Aplicamos la posición
            transform.position = smoothedPosition;
        }
    }
}