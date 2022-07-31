using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipsPerSecond : MonoBehaviour
{
    public float clipsPerSecond = 0;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            clipsPerSecond = Mathf.Round(((clipsPerSecond + Stats.clipsPerSecond * timer) / 2) * 100.0f) / 100.0f;
            Stats.clipsPerSecond = 0;
            timer = 0;
        }

        if (clipsPerSecond >= 5)
        {
            Reference.Achievement.Unlock(12);

            if (clipsPerSecond >= 10)
            {
                Reference.Achievement.Unlock(13);
            }
        }
    }
}
