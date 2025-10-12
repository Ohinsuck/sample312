using UnityEngine;

public class JudgeEffectCtrl : MonoBehaviour
{
    [Header("Judge Effect GameObjects")]
    public GameObject perfectFX;
    public GameObject greatFX;
    public GameObject goodFX;
    public GameObject badFX;
    public GameObject missFX;

    public void HideAll()
    {
        if (perfectFX != null) perfectFX.SetActive(false);
        if (greatFX != null)   greatFX.SetActive(false);
        if (goodFX != null)    goodFX.SetActive(false);
        if (badFX != null)     badFX.SetActive(false);
        if (missFX != null)    missFX.SetActive(false);
    }

    public void ShowEffect(string result)
    {
        HideAll();
        switch (result)
        {
            case "Perfect": if (perfectFX != null) perfectFX.SetActive(true); break;
            case "Great":   if (greatFX != null)   greatFX.SetActive(true);   break;
            case "Good":    if (goodFX != null)    goodFX.SetActive(true);    break;
            case "Bad":     if (badFX != null)     badFX.SetActive(true);     break;
            case "Miss":    if (missFX != null)    missFX.SetActive(true);    break;
        }
    }
}
