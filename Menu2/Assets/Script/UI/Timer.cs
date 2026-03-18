using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtHUDTimer;
    [SerializeField] private TextMeshProUGUI txtFinalTimer;
    private float timeElapsed;
    private bool isTimerRunning = false;

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
            UpdateText(txtHUDTimer);
        }
    }

    public void StartTimer() { timeElapsed = 0; isTimerRunning = true; }
    public void StopTimer() { isTimerRunning = false; UpdateText(txtFinalTimer); }

    private void UpdateText(TextMeshProUGUI textElement)
    {
        if (textElement == null) return;
        int min = Mathf.FloorToInt(timeElapsed / 60);
        int sec = Mathf.FloorToInt(timeElapsed % 60);
        textElement.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
