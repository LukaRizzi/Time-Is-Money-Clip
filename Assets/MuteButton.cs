using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MuteButton : MonoBehaviour
{
    //Uso listener en vez de un sistema de masters acá xq era una jam de 4 días

    private bool _muted = false;
    private float _unmutedVol;

    [SerializeField] private AudioClip[] aClips;
    [SerializeField] private AudioSource aSource;

    private void Awake()
    {
        _unmutedVol = AudioListener.volume;
    }

    public void Mute()
    {
        _muted = !_muted;
        AudioListener.volume = _muted ? 0 : _unmutedVol;
        aSource.clip = aClips[Random.Range(0, aClips.Length)];
        aSource.Play();
    }
}
