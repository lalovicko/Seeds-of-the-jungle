using UnityEngine;

public class ShadowNest : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    [SerializeField] private GameObject enemigoPrefab;
    [SerializeField] private float spawnRate = 30f;

    [Header("Efectos")]
    [SerializeField] private GameObject efectoExplosion;

    void Start()
    {
        // Empieza a invocar la función de spawn repetidamente
        InvokeRepeating("SpawnEnemy", spawnRate, spawnRate);
    }

    void SpawnEnemy()
    {
        if (enemigoPrefab != null)
        {
            Instantiate(enemigoPrefab, transform.position, Quaternion.identity);
        }
    }

    // Esta función será llamada por el jugador cuando tenga el escudo activo
    public void DestruirNido()
    {
        if (efectoExplosion != null)
        {
            Instantiate(efectoExplosion, transform.position, Quaternion.identity);
        }

        Debug.Log("Nido destruido por el Escudo de Espinas");
        Destroy(gameObject);
    }
}