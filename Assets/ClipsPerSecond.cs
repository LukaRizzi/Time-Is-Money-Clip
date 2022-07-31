using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipsPerSecond : MonoBehaviour
{
    public float Clips = 0;
    private float _timer = 0;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > 1)
        {
            Clips = Mathf.Round(((Clips + Stats.ClipsPerSecond * _timer) / 2) * 100.0f) / 100.0f;
            Stats.ClipsPerSecond = 0;
            _timer = 0;
        }

        if (Clips >= 5)
        {
            Reference.Achievement.Unlock(12);

            if (Clips >= 10)
            {
                Reference.Achievement.Unlock(13);
            }
        }
    }
}
