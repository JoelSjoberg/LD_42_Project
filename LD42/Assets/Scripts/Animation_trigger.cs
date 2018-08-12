using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_trigger : MonoBehaviour {

    public AnimationClip clip;
    Animation ani;

	// Use this for initialization
	void Start ()
    {
        ani = GetComponent<Animation>();
        Game_manager.set_key_exists(true);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ani.clip = clip;
            ani.Play();
            Debug.Log("Player");
            Game_manager.set_key_exists(false);
        }

    }

}
