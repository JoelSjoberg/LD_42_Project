using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_cube : MonoBehaviour {

    public space current_space;
    private space space_to_move_to = null;

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

    private void move()
    {
        current_space.decay();
        current_space = space_to_move_to;

        // Bugged
        print(current_space.space_connected_to_goal());


        space_to_move_to = null;
        StartCoroutine(move_player(current_space.transform.position));
    }

    float fraction = 0.0f, increment = 0.2f;    // increase increment to make transition faster
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
