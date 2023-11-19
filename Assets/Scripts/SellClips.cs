using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellClips : MonoBehaviour
{
    public LayerMask whatIsClip;
    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private AudioSource aSource;

    private int storedClips = 0;
    [SerializeField] private float sellClipsPerSecond = 3;
    private float timer;
    [SerializeField] private TextMeshProUGUI storedClipsText;

    [SerializeField] private TextMeshProUGUI storedClipsUpgradeText;
    [SerializeField] private TextMeshProUGUI storedClipCostText;
    [SerializeField] private float storedClipUpgradeCost = 100;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1 / sellClipsPerSecond)
        {
            if (storedClips > 0)
            {
                Sell();
            }
            timer -= 1 / sellClipsPerSecond;
        }
    }

    public void UpgradeStoredClipCapacity()
    {
        if (Stats.Money >= storedClipUpgradeCost)
        {
            Stats.Money -= Mathf.RoundToInt(storedClipUpgradeCost);

            sellClipsPerSecond++;

            storedClipsUpgradeText.text = "Sells up to " + sellClipsPerSecond.ToString() + " clips per second.";
            storedClipUpgradeCost *= 2f;
            storedClipCostText.text = "$" + storedClipUpgradeCost.ToString();

            aSource.clip = aClips[Random.Range(0, aClips.Length)];
            aSource.Play();
        }
    }

    private void Sell()
    {
        if (Reference.Achievement.lastSeller != null)
        {
            if (Reference.Achievement.lastSeller != transform)
            Reference.Achievement.Unlock(9);
        }
        Reference.Achievement.lastSeller = transform;

        Reference.Achievement.Unlock(3);
        storedClips--;
        storedClipsText.text = "Stored Clips: " + storedClips.ToString();
        Stats.ClipsSold++;
        Stats.Money += 15; Stats.TotalRevenue += 15; Stats.clipsPerSecond++;

        if (Stats.TotalRevenue >= 100)
        {
            Reference.Achievement.Unlock(10);

            if (Stats.TotalRevenue >= 1000)
            {
                Reference.Achievement.Unlock(11);

                if (Stats.TotalRevenue >= 100000)
                {
                    Reference.Achievement.Unlock(15);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((whatIsClip.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            if (other.transform.parent) //Quantum Bucket Achievement
            {
                if (other.transform.parent.name == "Bucket")
                    {Reference.Achievement.Unlock(6);}
            }

            Destroy(other.gameObject);

            storedClips++;
            storedClipsText.text = "Stored Clips: " + storedClips.ToString();

            aSource.clip = aClips[Random.Range(0, aClips.Length)];
            aSource.Play();
        }
    }
}
