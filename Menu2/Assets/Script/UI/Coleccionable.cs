using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [Header("Configuración del Coleccionable")]
    [SerializeField] private int pointsValue = 50; // Valor de la semilla sagrada

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Importante: Tu planta debe tener el Tag "Player" en el Inspector
        if (other.CompareTag("Player"))
        {
            // Intentamos obtener el script de movimiento de la planta
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                // Llamamos a la función de sumar puntaje que añadimos antes
                player.AddScore(pointsValue);

                // La semilla desaparece del mundo
                Destroy(gameObject);
            }
        }
    }
}