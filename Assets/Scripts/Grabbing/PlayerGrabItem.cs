using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabItem : MonoBehaviour
{
    public LayerMask propLayer;
    Transform target;
    public Transform propGrabPos;
    Transform grabbedProp = null;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));

        if (target)
            target.GetComponent<Prop>().watched = false;
        if (Physics.Raycast(ray, out hit, 3, propLayer))
        {
            target = hit.transform;
        }
        else
        {
            target = null;
        }

        if (target)
        {
            target.GetComponent<Prop>().watched = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (grabbedProp) {  DropItem(); }
                GrabItem(target);
                target = null;
            }
        }

        if (grabbedProp && Input.GetMouseButtonDown(1))
        {
            DropItem();
        }
    }

    void GrabItem(Transform _target)
    {
        if (target.name == "UnmadeClip(Clone)")
            Reference.Achievement.Unlock(1);

        grabbedProp = _target;
        _target.GetComponent<Prop>().active = true;
        _target.SetParent(propGrabPos, false);
        _target.localRotation = Quaternion.Euler(-90, 0, 0);
        _target.localPosition = Vector3.zero; //_target.localRotation = Quaternion.Euler(-90,0,0);
    }

    void DropItem()
    {
        Debug.Log("Dropped Item " + grabbedProp.name);
        grabbedProp.GetComponent<Prop>().active = false;
        grabbedProp.SetParent(null, false);
        grabbedProp.position = propGrabPos.position;
        grabbedProp.rotation = Quaternion.identity;
        grabbedProp = null;
    }

}
