using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Prop : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    Outline _outline;
    Collider _coll;

    public MonoBehaviour PropScript;
    public bool Watched = false;
    public bool Active = false;
    public bool InvertScript = false;
    public bool DisableColl = true;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        if (!rb)
            rb = GetComponent<Rigidbody>();
        _coll = GetComponent<Collider>();
    }

    void Update()
    {
        _outline.enabled = Watched && !Active;
        if (rb)
            rb.isKinematic = Active;
        if (PropScript)
            PropScript.enabled = Active == !InvertScript;
        _coll.enabled = !Active || !DisableColl;
    }
}
