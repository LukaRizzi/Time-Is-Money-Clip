using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reference : MonoBehaviour
{
    public static AchievementManager Achievement;
    [SerializeField] private AchievementManager achievement;

    private void Awake()
    {
        Achievement = achievement;
    }
}