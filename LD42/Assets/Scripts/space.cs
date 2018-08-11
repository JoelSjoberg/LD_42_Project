using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space : MonoBehaviour {

    // Input mapping = D: 0, S: 1, A: 2, W: 3 also indexing for connections array

    // variables for gameplay
    public space[] connections = new space[4];              // Can only be 4 long for directions left, right, up, down
    public bool visited_by_player = false;

    // graph search variables, if I have time for ai
    private bool visited_search = false;
    private int bfi = -1;

	// Use this for initialization
	void Start () {
        //connections = get_valid_connections();
        BoxCollider collider = GetComponent<BoxCollider>();
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
            if (!connections[index].visited_by_player) return connections[index];
            return null;

        }
        return null;
    }
}
