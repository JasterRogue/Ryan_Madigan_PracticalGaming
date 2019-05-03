using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour {

    public GameObject fireEffectGO;
    ParticleSystem fireEffect;

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
        GameObject newFE = Instantiate(fireEffectGO);
        fireEffect = newFE.GetComponent<ParticleSystem>();
        newFE.transform.position = pos;
        
        fireEffect.Play();
    }
}
