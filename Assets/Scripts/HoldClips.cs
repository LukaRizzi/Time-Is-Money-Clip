using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldClips : MonoBehaviour
{
    public LayerMask WhatIsClip;

    private void OnTriggerEnter(Collider other)
    {
        if ((WhatIsClip.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((WhatIsClip.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.transform.parent = null;
        }
    }
}
