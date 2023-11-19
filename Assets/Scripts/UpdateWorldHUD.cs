using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateWorldHUD : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private ClipsPerSecond cps;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        text.text = "Total clips sold: " + Stats.ClipsSold.ToString() + "\nTotal money made: $" + (Stats.ClipsSold*15).ToString() + "\nClips sold per second: " + cps.clipsPerSecond.ToString();
    }
}
