using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class EspinasShield : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float duracionEscudo = 3f;
    [SerializeField] private float cooldownEscudo = 8f;
    [SerializeField] private GameObject visualEscudo;

    private bool estaActivo = false;
    private bool puedeUsar = true;

    void Start()
    {
        if (visualEscudo != null) visualEscudo.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && puedeUsar)
        {
            StartCoroutine(ActivarEscudo());
        }
    }

    private IEnumerator ActivarEscudo()
    {
        estaActivo = true;
        puedeUsar = false;
        if (visualEscudo != null) visualEscudo.SetActive(true);

        yield return new WaitForSeconds(duracionEscudo);

        estaActivo = false;
        if (visualEscudo != null) visualEscudo.SetActive(false);

        yield return new WaitForSeconds(cooldownEscudo);
        puedeUsar = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!estaActivo) return;

        // 1. Destruir Murciélagos
        // Asegúrate que el Murciélago tenga el Tag "Enemigo"
        if (other.CompareTag("Enemigo"))
        {
            Debug.Log("Murciélago eliminado por escudo");
            Destroy(other.gameObject);
        }

        // 2. Destruir Nidos
        ShadowNest nido = other.GetComponent<ShadowNest>();
        if (nido != null)
        {
            nido.DestruirNido();
        }
    }
}