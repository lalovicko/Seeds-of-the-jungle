using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class UIStatemanager : MonoBehaviour
{
    public enum UIState
    {
        MainMenu,
        Option,
        Pause,
        HUD,
        Dead
    }

    [Header("Paneles")]

    [SerializeField] private GameObject Panel_Mainmenu;
    [SerializeField] private GameObject Panel_Options;
    [SerializeField] private GameObject Panel_Pause;
    [SerializeField] private GameObject Panel_HUD;
    [SerializeField] private GameObject Panel_Dead; // Panel de Game Over

    [Header("Debug")]

    [SerializeField] private TextMeshProUGUI txtStateDebug;

    private UIState currentState;

    private void Start()
    {
        ChangeState(UIState.MainMenu);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Solo permitir pausar si estamos jugando
            if (currentState == UIState.HUD)
            {
                ChangeState(UIState.Pause);
            }
            else if (currentState == UIState.Pause)
            {
                ChangeState(UIState.HUD);
            }
        }
    }

    public void ChangeState(UIState nextState)
    {
        currentState = nextState;

        // Apagar todos los paneles
        if (Panel_Mainmenu != null) Panel_Mainmenu.SetActive(false);
        if (Panel_Options != null) Panel_Options.SetActive(false);
        if (Panel_Pause != null) Panel_Pause.SetActive(false);
        if (Panel_HUD != null) Panel_HUD.SetActive(false);
        if (Panel_Dead != null) Panel_Dead.SetActive(false);

        // Tiempo normal por defecto
        Time.timeScale = 1f;

        switch (currentState)
        {
            case UIState.MainMenu:
                if (Panel_Mainmenu != null) Panel_Mainmenu.SetActive(true);
                Time.timeScale = 0f;
                break;

            case UIState.Option:
                if (Panel_Options != null) Panel_Options.SetActive(true);
                Time.timeScale = 0f;
                break;

            case UIState.Pause:
                if (Panel_Pause != null) Panel_Pause.SetActive(true);
                Time.timeScale = 0f;
                break;

            case UIState.HUD:
                if (Panel_HUD != null) Panel_HUD.SetActive(true);
                Time.timeScale = 1f;
                break;

            case UIState.Dead:
                if (Panel_Dead != null) Panel_Dead.SetActive(true);
                Time.timeScale = 0f;
                break;
        }

        // Texto de debug
        if (txtStateDebug != null)
        {
            txtStateDebug.text = "Current State: " + currentState.ToString();
        }
    }

    // BOTONES UI

    public void OnclickButtonStart()
    {
        ChangeState(UIState.HUD);
    }

    public void OnclickButtonOptions()
    {
        ChangeState(UIState.Option);
    }

    public void OnclickButtonBackToMainMenu()
    {
        ChangeState(UIState.MainMenu);
    }

    public void OnclickButtonResume()
    {
        ChangeState(UIState.HUD);
    }

    public void OnclickButtonPause()
    {
        ChangeState(UIState.Pause);
    }

    public void OnclickButtonExit()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}
