using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendClips : MonoBehaviour
{
    [SerializeField] private GenerateClips gclips;
    public LayerMask WhatIsClip;
    private void OnTriggerEnter(Collider other)
    {
        if ((WhatIsClip.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            gclips.GenerateClipFree();
            Destroy(other.gameObject);
        }
    }
}
