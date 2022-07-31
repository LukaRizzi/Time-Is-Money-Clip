using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip animClip;

    [SerializeField] private TextMeshProUGUI achievementName;
    [SerializeField] private TextMeshProUGUI achievementDescription;

    [SerializeField] private TextMeshProUGUI nextAchievementName;
    [SerializeField] private TextMeshProUGUI nextAchievementDescription;

    [SerializeField] private Achievement[] achievements;
    public bool[] Unlocked;

    [Header("AchievementData")]
    public Transform LastSeller;

    private void Awake()
    {
        Unlocked = new bool[achievements.Length];
        for (int i = 0; i < Unlocked.Length; i++)
        {
            Unlocked[i] = false;
        }
    }

    private void Start()
    {
        UpdateNextAchievement();
    }

    [SerializeField] private AudioSource aSource;
    public void Unlock(int _id) //Cambiar a ID
    {
        if (Unlocked[_id])
            return;

        anim.Play(animClip.name,-1,0f);
        aSource.Play();

        Unlocked[_id] = true;
        achievementName.text = achievements[_id].title;
        achievementDescription.text = achievements[_id].desc;

        UpdateNextAchievement();
    }

    public void UpdateNextAchievement()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (Unlocked[i])
                continue;

            nextAchievementName.text = achievements[i].title;
            nextAchievementDescription.text = achievements[i].desc;
            break;
        }
    }
}
