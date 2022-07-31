using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySeller : MonoBehaviour
{
    public GameObject ActualStation;
    public GameObject Canvas;
    public RaycastBuilding BuildingScript;
    public int Cost = 500;

    private bool _insideTrigger = false;

    [Header("Achievements")]
    public bool IsThisSeller = true;
    public bool IsThisBucket = false;

    private void Update()
    {
        if (_insideTrigger && Input.GetMouseButtonDown(0) && Stats.Money >= Cost && !BuildingScript.Building)
        {
            Invoke("Buy",.1f);
        }
    }

    private void Buy()
    {
        if (IsThisBucket)
        {
            if (Reference.Achievement.Unlocked[4])
                Reference.Achievement.Unlock(5);
            else
                Reference.Achievement.Unlock(4);
        }

        Stats.Money -= Cost;

        ActualStation.SetActive(true);
        ActualStation.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _insideTrigger = true; Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _insideTrigger = false; Canvas.SetActive(false);
        }
    }
}
