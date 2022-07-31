using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltUpper : MonoBehaviour
{
    public LayerMask WhatIsMovable;
    public List<Rigidbody> Rbs;
    [SerializeField] private float moveForce = 25f;

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in Rbs.ToArray())
        {
            if (rb == null)
            {
                Rbs.Remove(rb);
                continue;
            }

            rb.AddForce(transform.forward * moveForce + Vector3.up * 3f);
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
