using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [Header("Ajustes")]
    [SerializeField] private int scoreValue = 100; // Por si quieres que den puntos

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Le damos los puntos al PlayerMovement (el código que tú pusiste)
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.AddScore(scoreValue);
            }

            // 2. Le avisamos al GameManager para la victoria
            if (GameManager.instance != null)
            {
                GameManager.instance.SemillaRecogida();
            }

            Destroy(gameObject);
        }
    }
}
