using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public static SoundObject instance;

    private void Awake()
    {
        if (!instance) instance = this;
    }

    [SerializeField] private AudioSource hitClip;
    public void HitSound()
    {
        hitClip.Play();
    }
}
