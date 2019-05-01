using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeandRise : MonoBehaviour {
    float speed = 1;
    MeshRenderer meshRend;

	// Use this for initialization
	void Start ()
    {
       Destroy(gameObject, 2f);

        meshRend = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += speed * Vector3.up * Time.deltaTime;
	}
}
