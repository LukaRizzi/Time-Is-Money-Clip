using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltCorner : MonoBehaviour
{
    public LayerMask WhatIsMovable;
    public List<Rigidbody> Rbs;
    public bool Inverted = false;
    [SerializeField] private float moveForce = 15f;

    private void FixedUpdate()
    {
        Vector3 _i = transform.right;
        if (Inverted)
            _i *= -1;

        foreach (Rigidbody rb in Rbs.ToArray())
        {
            if (!rb)
                continue;
            rb.AddForce((transform.forward - _i) * .5f  * moveForce + Vector3.up * 2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((WhatIsMovable.value & (1 << other.transform.gameObject.layer)) > 0 && other.gameObject.GetComponent<Rigidbody>())
        {
            Rbs.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((WhatIsMovable.value & (1 << other.transform.gameObject.layer)) > 0 && other.gameObject.GetComponent<Rigidbody>())
        {
            Rbs.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
