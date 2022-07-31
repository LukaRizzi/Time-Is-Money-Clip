using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltUpper : MonoBehaviour
{
    public LayerMask whatIsMovable;
    public List<Rigidbody> rbs;
    [SerializeField] private float moveForce = 25f;

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in rbs.ToArray())
        {
            if (rb == null)
            {
                rbs.Remove(rb);
                continue;
            }

            rb.AddForce(transform.forward * moveForce + Vector3.up * 3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsMovable.value & (1 << other.transform.gameObject.layer)) > 0 && other.gameObject.GetComponent<Rigidbody>())
        {
            rbs.Add(other.gameObject.GetComponent<Rigidbody>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((whatIsMovable.value & (1 << other.transform.gameObject.layer)) > 0 && other.gameObject.GetComponent<Rigidbody>())
        {
            rbs.Remove(other.gameObject.GetComponent<Rigidbody>());
        }
    }
}
