using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse_hover_face : MonoBehaviour {

    Audio_manager am;
	// Use this for initialization
	void Start () {
        am = FindObjectOfType<Audio_manager>();
	}
	

    IEnumerator fade()
    {
        Material material = GetComponent<MeshRenderer>().materials[0];
        Color color = material.color;
        
        while (color.a > 0)
        {
            color.a -= 1f * Time.deltaTime;
            material.color = new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForEndOfFrame();
        }
        // completely removes this after fading
        if (color.a < 1)
        {
            this.gameObject.SetActive(false);
        }
    }


    private void OnMouseOver()
    {
        
        StartCoroutine(fade());

    }
    private void OnMouseEnter()
    {
        am.play_random_sound();
        am.play_random_pitch("jump");
    }
}
