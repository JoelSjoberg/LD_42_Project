using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    private float[] rotations = { 0, 90, 180, 270}; // more angles
    //private float[] rotations = { 45, 135, 225, 315 }; //fewer angles
    private int rotation_index = 0;
    public float step = 1.0f;
    // Use this for initialization
    void Start()
    {
    }

    float t = 0.0f;

    // Update is called once per frame
    void Update()
    {

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, rotations[rotation_index]), Time.deltaTime * step);

        if (Input.GetKeyDown(KeyCode.A))
        {
            increment_rotation_index(1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            increment_rotation_index(-1);
        }

        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Rotate(0, 0, -90);
        }
        */
    }

    private void increment_rotation_index(int x)
    {
        rotation_index += x;
        if (rotation_index > rotations.Length - 1) rotation_index = 0;
        else if (rotation_index < 0) rotation_index = rotations.Length - 1;
    }
}
