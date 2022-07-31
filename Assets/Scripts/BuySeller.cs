using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySeller : MonoBehaviour
{
    public GameObject actualStation;
    public GameObject canvas;
    public RaycastBuilding buildingScript;
    private bool insideTrigger = false;
    public int cost = 500;

    [Header("Achievements")]
    public bool isThisSeller = true;
    public bool isThisBucket = false;

    private void Update()
    {
        if (insideTrigger && Input.GetMouseButtonDown(0) && Stats.Money >= cost && !buildingScript.building)
        {
            Invoke("Buy",.1f);
        }
    }

    private void Buy()
    {
        if (isThisBucket)
        {
            if (Reference.Achievement.unlocked[4])
                Reference.Achievement.Unlock(5);
            else
                Reference.Achievement.Unlock(4);
        }

        Stats.Money -= cost;

        actualStation.SetActive(true);
        actualStation.transform.parent = null;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTrigger = true; canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            insideTrigger = false; canvas.SetActive(false);
        }
    }
}
