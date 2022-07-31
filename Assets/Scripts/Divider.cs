using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Divider : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Transform spawn1;
    [SerializeField] private Transform spawn2;

    private bool _isSpawn1 = true;

    private void OnTriggerEnter(Collider other)
    {
        if ((layerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            other.transform.position = _isSpawn1 ? spawn1.position : spawn2.position;
            _isSpawn1 = !_isSpawn1;
        }
    }
}
