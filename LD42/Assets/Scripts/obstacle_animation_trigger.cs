using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle_animation_trigger : MonoBehaviour {

    public AnimationClip clip;
    Animation ani;

    // Use this for initialization
    void Start()
    {
        ani = GetComponent<Animation>();
        Game_manager.set_key_exists(true);
    }

    public void play_anim()
    {
        ani.clip = clip;
        ani.Play();
    }
}
