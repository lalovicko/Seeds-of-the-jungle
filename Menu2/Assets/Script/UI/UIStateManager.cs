using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

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

    [Header("Paneles (Arrastra tus GameObjects aquí)")]
    [SerializeField] private GameObject Panel_Mainmenu;
    [SerializeField] private GameObject Panel_Options;
    [SerializeField] private GameObject Panel_Pause;
    [SerializeField] private GameObject Panel_HUD;
    [SerializeField] private GameObject Panel_Dead;

    [Header("Debug (Opcional)")]
    [SerializeField] private TextMeshProUGUI txtStateDebug;

    private UIState currentState;

    private void Start()
    {
        // Al iniciar el juego, mandamos al Menú Principal
        ChangeState(UIState.MainMenu);
    }

    public void ChangeState(UIState nextState)
    {
        currentState = nextState;

        // Apagar todos los paneles para que no se encimen
        if (Panel_Mainmenu != null) Panel_Mainmenu.SetActive(false);
        if (Panel_Options != null) Panel_Options.SetActive(false);
        if (Panel_Pause != null) Panel_Pause.SetActive(false);
        if (Panel_HUD != null) Panel_HUD.SetActive(false);
        if (Panel_Dead != null) Panel_Dead.SetActive(false);

        // Control del tiempo (Pausa el juego en menús, corre en el HUD)
        Time.timeScale = (currentState == UIState.HUD) ? 1f : 0f;

        switch (currentState)
        {
            case UIState.MainMenu:
                if (Panel_Mainmenu != null) Panel_Mainmenu.SetActive(true);
                break;

            case UIState.Option:
                if (Panel_Options != null) Panel_Options.SetActive(true);
                break;

            case UIState.Pause:
                if (Panel_Pause != null) Panel_Pause.SetActive(true);
                break;

            case UIState.HUD:
                if (Panel_HUD != null) Panel_HUD.SetActive(true);
                // Iniciamos el temporizador cuando el jugador empieza
                FindObjectOfType<TimerManager>()?.StartTimer();
                break;

            case UIState.Dead:
                if (Panel_Dead != null) Panel_Dead.SetActive(true);
                // Iniciamos la espera de 2 segundos para reiniciar
                StartCoroutine(WaitAndRestart());
                break;
        }

        if (txtStateDebug != null) txtStateDebug.text = "State: " + currentState.ToString();
    }

    private IEnumerator WaitAndRestart()
    {
        // Espera 2 segundos reales (porque Time.timeScale está en 0)
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // --- MÉTODOS PARA LOS BOTONES (On Click) ---

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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}