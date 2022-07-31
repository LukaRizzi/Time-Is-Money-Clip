using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    [SerializeField] private float despawnTime = 180f;

    private void Start()
    {
        Destroy(gameObject, despawnTime);
    }
}
