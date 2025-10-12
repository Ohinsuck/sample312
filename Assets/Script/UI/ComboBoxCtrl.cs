using UnityEngine;
using TMPro;

public class ComboBoxCtrl : MonoBehaviour
{
    public TMP_Text comboText;

    public void ShowCombo(int combo)
    {
        if (comboText != null)
            comboText.text = combo.ToString();
        // TODO: 애니메이션/이펙트 추가 가능
    }
}
