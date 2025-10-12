using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;

    private void Start()
    {
        if (scoreText != null) scoreText.text = "0";
        if (comboText != null)
        {
            comboText.text = "COMBO 0";
            comboText.gameObject.SetActive(false);
        }
    }

    public void UpdateScoreText(int score)
    {
        if (scoreText != null)
            scoreText.text = score.ToString("N0");
    }

    public void UpdateComboText(int combo)
    {
        if (comboText == null) return;

        if (combo > 1)
        {
            comboText.text = $"COMBO {combo}";
            comboText.gameObject.SetActive(true);
        }
        else
        {
            comboText.gameObject.SetActive(false);
        }
    }
}
