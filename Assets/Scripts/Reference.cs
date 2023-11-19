using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public static AchievementManager Achievement;
    public AchievementManager achievement;

    private void Awake()
    {
        Achievement = achievement;
    }
}