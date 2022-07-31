using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDButton : MonoBehaviour
{
    public string FuncName;
    public MonoBehaviour Script;

    public void UseButton()
    {
        Script.Invoke(FuncName, 0f);
    }
}