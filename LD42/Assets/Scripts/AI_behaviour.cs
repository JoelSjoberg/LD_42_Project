using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_behaviour : MonoBehaviour {

    public space current_space;
    space next_space = null;

    float ticker = 0.0f;
    public float wait_secs = 1;

    private bool dead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // The AI can move once per second, count the seconds
        ticker += Time.deltaTime;

        if(ticker >= wait_secs)
        {
            ticker = 0.0f;

            find_desired_move();
        }

	}

    void find_desired_move()
    {
        space[] avail = current_space.get_connections();
        List<space> valid = new List<space>();
        foreach(space s in avail)
        {
            if(s != null && !s.broken && !s.is_obstacle)
            {
                valid.Add(s);
            }
        }
        if(valid.Count > 0)
        {
            next_space = valid[(int)Random.Range(0, valid.Count)];
            move();
        }
        
        
    }

    private void move()
    {
        current_space.decay();
        current_space = next_space;

        next_space = null;
    }


    float fraction = 0.0f, increment = 0.5f;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.transform.GetComponent<Player_cube>().hit_player();
        }
    }
}
