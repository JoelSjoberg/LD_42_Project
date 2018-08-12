using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_menu : MonoBehaviour {


    Audio_manager am;
	// Use this for initialization
	void Start () {
        am = FindObjectOfType<Audio_manager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void start_game()
    {
        am.play("click");
        SceneManager.LoadScene("Level1");
    }
}
