using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floater : MonoBehaviour {

    public Vector3 dest;
    public space start_space;
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, dest, 2f * Time.deltaTime);
        if((transform.position - dest).magnitude < 0.3f)
        {
            start_space.get_connections();
        }
	}
}
