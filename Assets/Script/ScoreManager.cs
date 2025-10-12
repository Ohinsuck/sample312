using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public UIManager uiManager;
    public GaugeController gaugeController;

    [HideInInspector] public int currentScore = 0;
    [HideInInspector] public int currentCombo = 0;

    public void IncreaseScore(int amount)
    {
        currentScore += amount;
        uiManager?.UpdateScoreText(currentScore);
        gaugeController?.IncreaseGauge(amount);
    }

    public void IncreaseCombo()
    {
        currentCombo++;
        uiManager?.UpdateComboText(currentCombo);
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        uiManager?.UpdateComboText(currentCombo);
        gaugeController?.DecreaseGauge(10); // 예시
    }
}
