using UnityEngine;
using TMPro;

public class PlayerInfoCtrl : MonoBehaviour
{
    public TMP_Text speedText;
    public float speed = 1.0f;

    private void Update()
    {
        if (speedText != null)
            speedText.text = $"Speed: {speed:0.00}";
    }
}
