using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_cube : MonoBehaviour {

    public space current_space;
    private space space_to_move_to = null;

    // Lerping between spaces
    float fraction = 0.0f, increment = 0.2f;    // increase increment to make transition faster

    public bool is_AI = false;

    // Use this for initialization
    void Start () {

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
    }

    private void move()
    {
        current_space.decay();
        current_space = space_to_move_to;

        print(current_space.space_connected_to_goal());

        space_to_move_to = null;
        //StartCoroutine(move_player(current_space.transform.position));

        // Unlock obstackle space if you found the key
        if (current_space.is_key)
        {
            current_space.obstacle_space.remove_obstacle();

        }
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
