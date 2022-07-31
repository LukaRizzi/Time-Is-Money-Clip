using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldClips : MonoBehaviour
{
    public LayerMask whatIsClip;

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsClip.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((whatIsClip.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.transform.parent = null;
        }
    }
}
