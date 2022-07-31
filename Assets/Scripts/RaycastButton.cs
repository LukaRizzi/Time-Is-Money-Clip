using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastButton : MonoBehaviour
{
    public LayerMask WhatIsButton;

    private void Update()
    {
        RaycastHit hit = new();

        if (Physics.Raycast(transform.position,transform.forward, out hit, 5f) && Input.GetMouseButtonDown(0))
        {
            if (((WhatIsButton.value & (1 << hit.transform.gameObject.layer)) > 0))
            {
                hit.transform.gameObject.GetComponent<HUDButton>().UseButton();
            }
        }
    }
}