using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Achievement", menuName = "Achievement", order = 1)]
public class Achievement : ScriptableObject
{
    public string title = "";
    public string desc = "";
}