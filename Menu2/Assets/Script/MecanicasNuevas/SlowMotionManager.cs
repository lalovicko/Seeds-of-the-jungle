using UnityEngine;
using System.Collections; // Necesario para las Corrutinas
using UnityEngine.InputSystem;

public class SlowMotionManager : MonoBehaviour
{
    [Header("Configuración de Tiempo")]
    [SerializeField] private float slowTimeScale = 0.5f;
    [SerializeField] private float duration = 5f;

    private bool isSlowed = false;

    void Update()
    {
        // Verificamos si presionas la tecla Shift (Nuevo Input System)
        if (Keyboard.current != null && Keyboard.current.shiftKey.wasPressedThisFrame && !isSlowed)
        {
            StartCoroutine(ActivateSlowMo());
        }
    }

    private IEnumerator ActivateSlowMo()
    {
        isSlowed = true;

        // Activamos el efecto
        Time.timeScale = slowTimeScale;
        // Ajustamos el tiempo de física para que no se vea a tirones
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // Esperamos el tiempo de duración (en tiempo real, no ralentizado)
        yield return new WaitForSecondsRealtime(duration);

        // Restauramos el tiempo normal
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        isSlowed = false;
        Debug.Log("Tiempo restaurado");
    }
}