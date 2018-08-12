using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_cube : MonoBehaviour {

    public space current_space;
    private space space_to_move_to = null;

    // Lerping between spaces
    float fraction = 0.0f, increment = 0.2f;    // increase increment to make transition faster

    public Follower cam;

    // Last minute solution for fading, figure out how to better handle this in the future!
    public fade_to_black fd;

    public mc_animator anim;

    // Use this for initialization
    void Start ()
    {

	}

	// Update is called once per frame
	void Update () {

        // handle input

            if(Input.GetKeyDown(KeyCode.W))
            {
                space_to_move_to = current_space.get_connection(3);
            }

            if(Input.GetKeyDown(KeyCode.D))
            {
                space_to_move_to = current_space.get_connection(0);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                space_to_move_to = current_space.get_connection(1);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                space_to_move_to = current_space.get_connection(2);
            }

        if(space_to_move_to != null)
        {
            move();
        }
    }

    private void FixedUpdate()
    {
        // Movement between spaces
        Vector3 pos = current_space.transform.position;
        Vector3 to = new Vector3(pos.x, pos.y + 0.5f, pos.z);

        if ((transform.position - pos).magnitude > 0.5)
        {
            fraction += increment;
            transform.position = Vector3.Lerp(transform.position, to, fraction);
        }
        else fraction = 0.0f;


        if (current_space.is_goal)
        {
            float_away();
        }
    }

    private void move()
    {
        current_space.decay();
        current_space = space_to_move_to;
        anim.set_clip(1);
        print(current_space.space_connected_to_goal());

        if(!current_space.space_connected_to_goal())
        {
            // Should reload scene now!
        }

        space_to_move_to = null;

        // Unlock obstackle space if you found the key
        if (current_space.is_key)
        {
            current_space.obstacle_space.remove_obstacle();
        }
    }

    // float into victory screen
    void float_away()
    {
        cam.following = false;
        anim.set_clip(2);
        transform.position += Vector3.forward * 10 * Time.deltaTime;
        current_space.transform.position += Vector3.forward * 10 * Time.deltaTime;
        StartCoroutine(fade());
        StartCoroutine(load_next(0.90f));
    }

    // called when AI hits player
    public void hit_player()
    {
        cam.start_shake(0.1f);
        StartCoroutine(restart(0.9f));
        StartCoroutine(fade());
        Debug.Log("Do it over again");
    }


    IEnumerator fade()
    {
        fd.start_fade();
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator load_next(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        current_space.load_next_scene();
    }
    IEnumerator restart(float sec) 
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(Application.loadedLevel);
    }
}
