using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Prop : MonoBehaviour
{
    Outline outline;
    [SerializeField] Rigidbody rb;
    Collider coll;

    public MonoBehaviour propScript;
    public bool watched = false;
    public bool active = false;
    public bool invertScript = false;
    public bool disableColl = true;

    private void Start()
    {
        outline = GetComponent<Outline>();
        if (!rb)
            rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    void Update()
    {
        outline.enabled = watched && !active;
        if (rb)
            rb.isKinematic = active;
        if (propScript)
            propScript.enabled = active == !invertScript;
        coll.enabled = !active || !disableColl;
    }
}
