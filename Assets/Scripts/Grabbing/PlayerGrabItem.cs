using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabItem : MonoBehaviour
{
    public LayerMask PropLayer;
    public Transform PropGrabPos;
    Transform _target;
    Transform _grabbedProp = null;

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));

        if (_target)
            _target.GetComponent<Prop>().Watched = false;
        if (Physics.Raycast(ray, out hit, 3, PropLayer))
        {
            _target = hit.transform;
        }
        else
        {
            _target = null;
        }

        if (_target)
        {
            _target.GetComponent<Prop>().Watched = true;
            if (Input.GetMouseButtonDown(0))
            {
                if (_grabbedProp) {  DropItem(); }
                GrabItem(_target);
                _target = null;
            }
        }

        if (_grabbedProp && Input.GetMouseButtonDown(1))
        {
            DropItem();
        }
    }

    void GrabItem(Transform _target)
    {
        if (_target.name == "UnmadeClip(Clone)")
            Reference.Achievement.Unlock(1);

        _grabbedProp = _target;
        _target.GetComponent<Prop>().Active = true;
        _target.SetParent(_grabbedProp, false);
        _target.localRotation = Quaternion.Euler(-90, 0, 0);
        _target.localPosition = Vector3.zero; //_target.localRotation = Quaternion.Euler(-90,0,0);
    }

    void DropItem()
    {
        Debug.Log("Dropped Item " + _grabbedProp.name);
        _grabbedProp.GetComponent<Prop>().Active = false;
        _grabbedProp.SetParent(null, false);
        _grabbedProp.position = PropGrabPos.position;
        _grabbedProp.rotation = Quaternion.identity;
        _grabbedProp = null;
    }

}
