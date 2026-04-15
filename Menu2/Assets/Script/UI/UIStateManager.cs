using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIStatemanager : MonoBehaviour
{
    public enum UIState { MainMenu, Option, Pause, HUD, Dead, Win }

    [Header("Paneles")]
    [SerializeField] private GameObject Panel_Mainmenu;
    [SerializeField] private GameObject Panel_Options;
    [SerializeField] private GameObject Panel_Pause;
    [SerializeField] private GameObject Panel_HUD;
    [SerializeField] private GameObject Panel_Dead;
    [SerializeField] private GameObject Panel_Win; 

    [Header("Debug")]
    [SerializeField] private TextMeshProUGUI txtStateDebug;

    private UIState currentState;

    private void Start() => ChangeState(UIState.MainMenu);

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (currentState == UIState.HUD) ChangeState(UIState.Pause);
            else if (currentState == UIState.Pause) ChangeState(UIState.HUD);
        }
    }

    public void ChangeState(UIState nextState)
    {
        currentState = nextState;

        // Apagar TODOS los paneles primero
        Panel_Mainmenu?.SetActive(false);
        Panel_Options?.SetActive(false);
        Panel_Pause?.SetActive(false);
        Panel_HUD?.SetActive(false);
        Panel_Dead?.SetActive(false);
        Panel_Win?.SetActive(false);

        switch (currentState)
        {
            case UIState.MainMenu:
            case UIState.Option:
            case UIState.Pause:
            case UIState.Dead:
            case UIState.Win: 
                Time.timeScale = 0f;
                break;
            case UIState.HUD:
                Time.timeScale = 1f;
                break;
        }

        // Activar el panel correspondiente
        if (currentState == UIState.MainMenu) Panel_Mainmenu?.SetActive(true);
        if (currentState == UIState.Option) Panel_Options?.SetActive(true);
        if (currentState == UIState.Pause) Panel_Pause?.SetActive(true);
        if (currentState == UIState.HUD) Panel_HUD?.SetActive(true);
        if (currentState == UIState.Dead) Panel_Dead?.SetActive(true);
        if (currentState == UIState.Win) Panel_Win?.SetActive(true); // <--- NUEVO

        if (txtStateDebug != null) txtStateDebug.text = "State: " + currentState.ToString();
    }

    // --- TUS BOTONES SIGUEN IGUAL ABAJO ---
    public void OnclickButtonStart() => ChangeState(UIState.HUD);
    public void OnclickButtonResume() => ChangeState(UIState.HUD);
    public void OnclickButtonBackToMainMenu() => ChangeState(UIState.MainMenu);
    public void OnclickButtonExit() { /* tu lógica de salir */ }
}