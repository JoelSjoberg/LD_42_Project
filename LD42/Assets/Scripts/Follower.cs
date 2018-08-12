using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {


    public Transform subject_to_follow;
    public bool following = true;
    public float speed = 0.5f;
    public float zoom_speed = 1f;
    private Vector3 delta;
    private Camera cam;
    public float orto_size = 20;
    private float origin_orto_size;
    private bool changed_ortho = false;

	// Use this for initialization
	void Start () {
        delta = transform.position - subject_to_follow.position;
        cam = GetComponent<Camera>();
        origin_orto_size = cam.orthographicSize;

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dest = new Vector3(subject_to_follow.position.x + delta.x, transform.position.y, subject_to_follow.position.z + delta.z);
		if(following)
        {
            transform.position = Vector3.Lerp(transform.position, dest, speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.M))
        {
            changed_ortho = !changed_ortho;
        }

        if (changed_ortho) cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, orto_size, zoom_speed * Time.deltaTime);
        else cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, origin_orto_size, zoom_speed * Time.deltaTime);
    }

    public void start_shake(float sec)
    {
        StartCoroutine(shake(sec));
    }

    IEnumerator shake(float sec)
    {
        float t = 0.0f;

        while(t < sec)
        {
            t += Time.deltaTime;
            transform.position += Random.insideUnitSphere;
            yield return new WaitForSeconds(0.05f);

        }
    }
}
