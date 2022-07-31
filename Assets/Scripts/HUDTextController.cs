using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDTextController : MonoBehaviour
{
    public TextMeshProUGUI WarningText;
    public TextMeshProUGUI MoneyText;

    public void SendWarning(string _text)
    {
        WarningText.text = _text;
        Invoke("CleanWarning", 5f);
    }
    public void SendWarning(string _text, float _time)
    {
        WarningText.text = _text;
        Invoke("CleanWarning", _time);
    }

    public void CleanWarning()
    {
        WarningText.text = "";
    }

    private void Update()
    {
        MoneyText.text = "$" + Stats.Money.ToString();
    }
}
