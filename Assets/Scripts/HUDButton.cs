using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDButton : MonoBehaviour
{
    public string funcName;
    public MonoBehaviour script;

    public void UseButton()
    {
        script.Invoke(funcName, 0f);
    }
}