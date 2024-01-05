using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ambiencemanager : MonoBehaviour
{
    public SunRotate sunRotate;
    public AudioClip day;
    public AudioClip night;
    public AudioSource audioSource;

    public enum WhatTime
    {
        day,
        night
    }
    public WhatTime currenttime;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // 37 = południe
    // 58 = wieczór
    // 68 = noc
    // 110 = świt

    // Update is called once per frame
    void Update()
    {
        if (sunRotate.timePassed >= 68 && sunRotate.timePassed <= 110 && currenttime == WhatTime.day)
        {
            noc();
        }
        else if (sunRotate.timePassed >= 110 && currenttime == WhatTime.night || sunRotate.timePassed >= 0 && sunRotate.timePassed <= 67 && currenttime == WhatTime.night)
        {
            rano();
        }
    }

    void rano()
    {
        audioSource.Stop();
        audioSource.clip = day;
        audioSource.Play();
        currenttime = WhatTime.day;
    }

    void noc()
    {
        audioSource.Stop();
        audioSource.clip = night;
        audioSource.Play();
        currenttime = WhatTime.night;
    }
}
