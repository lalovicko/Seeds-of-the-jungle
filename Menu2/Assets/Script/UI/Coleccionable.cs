using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    [SerializeField] private int pointsValue = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.AddScore(pointsValue);
                Destroy(gameObject);
            }
        }
    }
}