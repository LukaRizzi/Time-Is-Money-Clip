using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GenerateClips : MonoBehaviour
{
    [SerializeField] private GameObject clip;
    [SerializeField] private GameObject[] easterEggClip;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private AudioClip aError;
    [SerializeField] private AudioSource aSource;
    [SerializeField] private HUDTextController warning;

    [SerializeField] private GameObject buyAutomaticButton;
    [SerializeField] private GameObject upgradeShopStage2HUD;

    [SerializeField] private TextMeshProUGUI clipUpgradePriceHUD;
    [SerializeField] private int clipUpgradePrice = 250;

    [SerializeField] private TextMeshProUGUI automaticEnabledHUD;

    [SerializeField] private int wirePrice = 4;
    [SerializeField] private TextMeshProUGUI wirePriceText;
    [SerializeField] private TextMeshProUGUI wirePriceUpgradeText;
    [SerializeField] private int wirePriceUpdateCost = 50;

    public bool Automatic = false;
    public float SpawnCD = 5f;
    public float Timer = 0f;

    [Header("Achievements")]
    public bool IsThisWireGenerator = false;
    public bool IsThisClipGenerator = false;

    private void Start()
    {
        warning = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDTextController>();
    }

    private void Update()
    {
        if (Automatic)
        {
            Timer -= Time.deltaTime;

            if (Timer <= 0)
            {
                Timer += SpawnCD;
                AutoGenerateClip();
            }
        }
    }

    [SerializeField] private int autobuyUpgradePrice = 100;
    public void BuyClipsAutomatically()
    {
        if (Stats.Money >= autobuyUpgradePrice)
        {
            Stats.Money -= autobuyUpgradePrice;
            Automatic = true;
            buyAutomaticButton.SetActive(false);
            upgradeShopStage2HUD.SetActive(true);
        }
        else
        {
            aSource.clip = aError;
            aSource.Play();
            warning.SendWarning("Not enough money.", 1f);
        }
    }

    public void OptimizeMetalUse()
    {
        if (Stats.Money >= wirePriceUpdateCost)
        {
            Stats.Money -= wirePriceUpdateCost;
            wirePrice--;
            wirePriceUpdateCost *= 2;

            wirePriceText.text = "Wire Price: $" + wirePrice.ToString();
            wirePriceUpgradeText.text = "$" + wirePriceUpdateCost.ToString();

            aSource.clip = aClips[Random.Range(0, aClips.Length)];
            aSource.Play();

            if (wirePrice == 1)
            {
                wirePriceUpgradeText.transform.parent.gameObject.SetActive(false);
            }
        }
        else
        {
            aSource.clip = aError;
            aSource.Play();
            warning.SendWarning("Not enough money.", 1f);
        }
    }

    public void UpgradeWireSpeed()
    {
        if (Stats.Money >= clipUpgradePrice)
        {
            Stats.Money -= clipUpgradePrice;
            clipUpgradePrice = Mathf.RoundToInt(clipUpgradePrice * 3f);
            clipUpgradePriceHUD.text = "$" + clipUpgradePrice.ToString();
            SpawnCD *= .5f;
            Timer = Mathf.Min(Timer, SpawnCD);

            aSource.clip = aClips[Random.Range(0, aClips.Length)];
            aSource.Play();
        }
        else
        {
            aSource.clip = aError;
            aSource.Play();
            warning.SendWarning("Not enough money.", 1f);
        }
    }

    public void GenerateClip()
    {
        if (Stats.Money >= wirePrice)
        {
            if (IsThisWireGenerator)
                Reference.Achievement.Unlock(0);

            Stats.Money -= wirePrice;

            GameObject _clip = Random.Range(0, 100) > .01 ? clip : easterEggClip[Random.Range(0, easterEggClip.Length)];

            Instantiate(_clip, spawnPoint.position, spawnPoint.rotation);

            aSource.clip = aClips[Random.Range(0, aClips.Length)];
            aSource.Play();
        }
        else
        {
            aSource.clip = aError;
            aSource.Play();
            warning.SendWarning("Not enough money.", 1f);
        }
    }

    public void AutoGenerateClip()
    {
        if (Stats.Money >= wirePrice)
        {
            if (IsThisWireGenerator)
                Reference.Achievement.Unlock(0);

            Stats.Money -= wirePrice;

            GameObject _clip = Random.Range(0, 100) > .01 ? clip : easterEggClip[Random.Range(0, easterEggClip.Length)];

            Instantiate(_clip, spawnPoint.position, spawnPoint.rotation);

            aSource.clip = aClips[Random.Range(0, aClips.Length)];
            aSource.Play();
        }
    }

    public void GenerateClipFree()
    {
        if (IsThisClipGenerator)
            Reference.Achievement.Unlock(2);

        GameObject _clip = Random.Range(0, 100) > .01 ? clip : easterEggClip[Random.Range(0, easterEggClip.Length)];

        Instantiate(_clip, spawnPoint.position, spawnPoint.rotation);

        aSource.clip = aClips[Random.Range(0, aClips.Length)];
        aSource.Play();
    }

    public void EnableAutomatic()
    {
        Automatic = !Automatic;
        automaticEnabledHUD.text = Automatic ? "Automatic Enabled" : "Automatic Disabled";

        aSource.clip = aClips[Random.Range(0, aClips.Length)];
        aSource.Play();
    }
}