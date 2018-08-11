using UnityEngine.Audio;
using System;
using UnityEngine;

public class Audio_manager : MonoBehaviour {

    public Sound[] sounds;
    public static Audio_manager instance;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(instance);

        foreach(Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    public void play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound with name: " + name + " was not found");
        }
        s.source.Play();
    }



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
