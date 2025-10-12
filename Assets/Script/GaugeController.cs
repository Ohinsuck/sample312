using UnityEngine;

public class GaugeController : MonoBehaviour
{
    public UIManager uiManager;
    [SerializeField] private float currentGauge = 50.0f;
    public float failThreshold = 0.0f;

    private const float MAX_GAUGE = 100.0f;
    private const float MIN_GAUGE = 0.0f;

    public void IncreaseGauge(float amount)
    {
        currentGauge = Mathf.Min(currentGauge + amount, MAX_GAUGE);
        UpdateGaugeUI();
    }

    public void DecreaseGauge(float amount)
    {
        currentGauge = Mathf.Max(currentGauge - amount, MIN_GAUGE);
        UpdateGaugeUI();
        if (currentGauge <= failThreshold) GameOver();
    }

    private void UpdateGaugeUI()
    {
        // TODO: 게이지 UI 표시가 필요하면 UIManager와 연동
    }

    private void GameOver()
    {
        Debug.Log("Game Over! Gauge hit 0.");
        Time.timeScale = 0;
        // uiManager?.ShowGameOverPanel();
    }

    public float GetCurrentGauge() => currentGauge;
}
