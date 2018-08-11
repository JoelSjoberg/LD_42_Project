using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space : MonoBehaviour {

    // Input mapping = D: 0, S: 1, A: 2, W: 3 also indexing for connections array

    // variables for gameplay
    public space[] connections = new space[4];              // Can only be 4 long for directions left, right, up, down
    public bool broken = false;
    public int visits_before_break = 1;
    private int max_visits = 5;
    // graph search variables, if I have time for ai
    private int distance = -1;

    public bool is_goal = false;

	// Use this for initialization
	void Start () {

        get_connections();
	}
	
	// Update is called once per frame
	void Update () {

	}

    private void get_connections()
    {
        RaycastHit hit;

        // D-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, Mathf.Infinity))
        {
            {
                connections[0] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        // S-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit, Mathf.Infinity))
        {
            {
                connections[1] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        // A-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, Mathf.Infinity))
        {
            {
                connections[2] = hit.transform.gameObject.GetComponent<space>();
            }
        }

        // W-direction
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)) 
        {
            {
                connections[3] = hit.transform.gameObject.GetComponent<space>();
            }
        }
    }

    // return a valid place to move to, returns null if already visited or no connectio available
    public space get_connection(int index)
    {
        if (connections[index] != null)
        {
            if (!connections[index].broken) return connections[index];
            return null;

        }
        return null;
    }

    // check if it is possible to win from this space
    bool win_possible()
    {
        // TODO implement dijkstras algorithm for shortest path finding(alternatively, implement A*)

        return false;
    }


    // Decay after each visit, if visited enough, break it and fade out
    public void decay()
    {
        visits_before_break -= 1;

        float fade_amount = 1 - (1 / (visits_before_break + 1));
        StartCoroutine(fade(fade_amount));

        if (visits_before_break <= 0)
        {
            broken = true;
        } 
    }

    float fade_amount = 0.0f;
    IEnumerator fade(float alpha)
    {
        Material material = GetComponent<MeshRenderer>().materials[0];
        Color color = material.color;

        while(color.a > alpha)
        {
            fade_amount += (0.001f * Time.deltaTime);
            material.color = new Color(color.r, color.g, color.b, fade_amount);
            transform.GetComponent<MeshRenderer>().materials[0] = material;
            yield return new WaitForEndOfFrame();
        }
        fade_amount = 0;
    }
}
