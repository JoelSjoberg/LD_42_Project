using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_cube : MonoBehaviour {

    public space current_space;
    private space space_to_move_to = null;

    // Lerping between spaces
    float fraction = 0.0f, increment = 0.2f;    // increase increment to make transition faster

    // animation tests
    Animation ani;
    public AnimationClip[] clips;
    public int curr_clip = 0; // this index indicates which animatin should be playing, 0 : idle, 1 : jump, 2 : celebrate 

    public Follower cam;

    // Use this for initialization
    void Start ()
    {
        ani = GetComponent<Animation>();
        ani.clip = clips[curr_clip];
	}

	// Update is called once per frame
	void Update () {

        ani.Play();

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

        print(current_space.space_connected_to_goal());

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

        transform.position += Vector3.forward * 10 * Time.deltaTime;
        current_space.transform.position += Vector3.forward * 10 * Time.deltaTime;
    }

    // Redundant
    IEnumerator move_player(Vector3 pos) 
    {
        Vector3 to = new Vector3(pos.x, pos.y + 0.5f, pos.z);// put player at appropriate height above current space
        while (fraction <= 1.0f)
        {
            fraction += increment;
            transform.position = Vector3.Lerp(transform.position, to, fraction);
            yield return new WaitForEndOfFrame();
        }
        fraction = 0.0f;
    }
}
