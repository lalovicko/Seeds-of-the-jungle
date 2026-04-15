using UnityEngine;
using TMPro; // Para el texto de victoria

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int semillasTotales;
    private int semillasRecogidas = 1;
    private UIStatemanager uiManager;
    public int puntajeTotal = 0; // Para llevar la cuenta de los puntos

    public void SemillaRecogida(int puntos)
    {
        semillasRecogidas++;
        puntajeTotal += puntos; // <--- Sumamos los puntos aquí

        // Si tienes un método para actualizar el texto del HUD, llámalo aquí:
        // uiManager.ActualizarTextoPuntaje(puntajeTotal);

        if (semillasRecogidas >= semillasTotales)
        {
            uiManager.ChangeState(UIStatemanager.UIState.Win);
        }
    }
    void Awake()
    {
        instance = this;
    }

    void Start()
    {

        uiManager = Object.FindFirstObjectByType<UIStatemanager>();


        semillasTotales = GameObject.FindGameObjectsWithTag("Coleccionables").Length;

        Debug.Log("Semillas a recoger: " + semillasTotales);
    }

    public void SemillaRecogida()
    {
        semillasRecogidas++;

        // Solo se encarga de revisar la victoria
        if (semillasRecogidas >= semillasTotales)
        {
            uiManager.ChangeState(UIStatemanager.UIState.Win);
        }
    }
}