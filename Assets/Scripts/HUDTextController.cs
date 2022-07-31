using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDTextController : MonoBehaviour
{
    public TextMeshProUGUI warningText;
    public TextMeshProUGUI moneyText;

    public void SendWarning(string _text)
    {
        warningText.text = _text;
        Invoke("CleanWarning", 5f);
    }
    public void SendWarning(string _text, float _time)
    {
        warningText.text = _text;
        Invoke("CleanWarning", _time);
    }

    public void CleanWarning()
    {
        warningText.text = "";
    }

    private void Update()
    {
        moneyText.text = "$" + Stats.Money.ToString();
    }
}
