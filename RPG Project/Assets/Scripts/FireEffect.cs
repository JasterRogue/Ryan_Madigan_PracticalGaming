using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour {

    public ParticleSystem fireEffect;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void playFireEffect(Vector3 pos)
    {
        transform.position = pos;

        fireEffect.Emit(80);
    }
}
