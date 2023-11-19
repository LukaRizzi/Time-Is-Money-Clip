using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltCorner : MonoBehaviour
{
    public LayerMask whatIsMovable;
    public List<Rigidbody> rbs;
    public bool inverted = false;
    [SerializeField] private float moveForce = 15f;

    private void FixedUpdate()
    {
        Vector3 _i = transform.right;
        if (inverted)
            _i *= -1;

        foreach (Rigidbody rb in rbs.ToArray())
        {
            if (!rb)
                continue;
            rb.AddForce((transform.forward - _i) * .5f  * moveForce + Vector3.up * 2);
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
