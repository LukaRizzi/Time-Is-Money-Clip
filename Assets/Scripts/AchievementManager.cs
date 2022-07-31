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
    public bool[] unlocked;

    [Header("AchievementData")]
    public Transform lastSeller;

    private void Awake()
    {
        unlocked = new bool[achievements.Length];
        for (int i = 0; i < unlocked.Length; i++)
        {
            unlocked[i] = false;
        }
    }

    private void Start()
    {
        UpdateNextAchievement();
    }

    [SerializeField] private AudioSource aSource;
    public void Unlock(int _id) //Cambiar a ID
    {
        if (unlocked[_id])
            return;

        anim.Play(animClip.name,-1,0f);
        aSource.Play();

        unlocked[_id] = true;
        achievementName.text = achievements[_id].title;
        achievementDescription.text = achievements[_id].desc;

        UpdateNextAchievement();
    }

    public void UpdateNextAchievement()
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            if (unlocked[i])
                continue;

            nextAchievementName.text = achievements[i].title;
            nextAchievementDescription.text = achievements[i].desc;
            break;
        }
    }
}
