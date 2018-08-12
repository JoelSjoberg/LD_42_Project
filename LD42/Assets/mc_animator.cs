using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mc_animator : MonoBehaviour {

    Animation ani;
    Animator a;
    public AnimationClip[] clips;
    public int curr_clip;
	// Use this for initialization
	void Start () {
        ani = GetComponent<Animation>();
        a = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void set_clip(int index)
    {
        if (index == 1)
        {
            a.SetTrigger("jump");
            a.SetBool("at_goal", false);
        }
        if (index == 2)
        {
            a.SetBool("at_goal", true);
        }
    }
}
