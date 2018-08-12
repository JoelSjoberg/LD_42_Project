using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class space : MonoBehaviour {

    // Input mapping = D: 0, S: 1, A: 2, W: 3 also indexing for connections array

    // variables for gameplay
    public space[] connections = new space[4];              // Can only be 4 long for directions left, right, up, down
    public bool broken = false;
    public int visits_before_break = 1;
    private int max_visits = 5;

    // assign one goal space per level
    public bool is_goal = false, is_obstacle = false, is_key = false;

    public space obstacle_space; // For key space to unlock obstackle space
    public GameObject obstacle;
    public float ray_length = 20;

    public string next_scene;

	// Use this for initialization
	void Start ()
    {
        get_connections();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    public space[] get_connections()
    {
        RaycastHit hit;

        // D-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, ray_length))
        {
            {
                connections[0] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        // S-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, ray_length))
        {
            {
                connections[1] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        // A-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, ray_length))
        {
            {
                connections[2] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        // W-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, ray_length)) 
        {
            {
                connections[3] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        return connections;
    }

    // return a valid place to move to, returns null if already visited or no connectio available
    public space get_connection(int index)
    {
        if (connections[index] != null)
        {
            if (!connections[index].broken && !connections[index].is_obstacle) return connections[index];
            return null;
        }
        return null;
    }

    // Returns true if the player can still reach the goal, false otherwise
    // breadth first search
    public bool space_connected_to_goal()
    {
        Queue<space> queue = new Queue<space>();
        List<space> visited = new List<space>();
        bool key_found = false;
        bool goal_found = false;
        queue.Enqueue(this);

        while(queue.Count > 0)
        {
            space s = queue.Dequeue();

            if (s.is_goal)
            {
                goal_found = true;
            }
            if (s.is_key)
            {
                key_found = true;
            }

            if((Game_manager.key_exists && key_found && goal_found) || (!Game_manager.key_exists && goal_found))
            {
                return true;
            }

            visited.Add(s);
            for (int i = 0; i < s.connections.Length; i++)
            { 
                // check that it is not null, that it hasn't been visited and that it can be travelled accross
                if(s.connections[i] != null && !visited.Contains(s.connections[i]) && s.connections[i].broken == false)
                {
                    queue.Enqueue(s.connections[i]);
                }
            }
        }
        return false;
    }

    public void remove_obstacle()
    {

        this.is_obstacle = false;
        this.obstacle.GetComponent<obstacle_animation_trigger>().play_anim();
    }

    // Decay after each visit, if visited enough, break it and fade outue
    public void decay()
    {
        
        if(visits_before_break > 0) visits_before_break -= 1; // avoid edge case

        float fade_amount = 1 - (1 / (visits_before_break + 1));
        StartCoroutine(fade(fade_amount));

        if (visits_before_break <= 0)
        {
            broken = true;
        } 
    }

    // fade efter decay
    float fade_amount = 0.0f;

    IEnumerator fade(float alpha)
    {
        float goal_a = alpha * 255;
        Material material = GetComponent<MeshRenderer>().materials[0];
        Color color = material.color;

        while(color.a > goal_a)
        {
            color.a -= 1f * Time.deltaTime;
            material.color =  new Color(color.r, color.g, color.b, color.a);
            yield return new WaitForEndOfFrame();
        }
        // completely removes this after fading
        if(color.a < 1)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void load_next_scene()
    {

        SceneManager.LoadScene(next_scene);
    }
}
