using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteButton : MonoBehaviour
{
    private bool muted = false;
    private float unmutedVol;

    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private AudioSource aSource;

    private void Awake()
    {
        unmutedVol = AudioListener.volume;
    }

    public void Mute()
    {
        muted = !muted;
        AudioListener.volume = muted ? 0 : unmutedVol;
        aSource.clip = aClips[Random.Range(0, aClips.Length)];
        aSource.Play();
    }
}
