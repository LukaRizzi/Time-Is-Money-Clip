using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform Player;
    private void Update()
    {
        transform.LookAt(Player.position);
    }
}
