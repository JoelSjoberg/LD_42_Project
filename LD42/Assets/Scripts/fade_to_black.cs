using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade_to_black : MonoBehaviour{

    public Animation ani;

    private void Start()
    {
        ani = GetComponent<Animation>();
    }

    public void start_fade()
    {
        ani.Play();
    }

}
