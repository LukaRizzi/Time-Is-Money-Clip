using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayer : MonoBehaviour
{
    public bool debug = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.U))
        {
            Reference.Achievement.Unlock(17);
            debug = true;
        }

        if (debug && Input.GetKeyDown(KeyCode.C))
        {
            Stats.Money += 100;
        }
    }
}
