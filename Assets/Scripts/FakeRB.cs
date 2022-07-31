using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRB : MonoBehaviour
{
    [SerializeField] Transform rb;
    [SerializeField] Transform _transform;

    private void Update()
    {
        _transform.position = rb.position;
        rb.localPosition = Vector3.zero;
        rb.localRotation = Quaternion.Euler(-90,0,0);
    }
}
