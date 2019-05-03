using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonerAnimation : MonoBehaviour {

    // Use this for initialization
    Animator myAnimator;
	void Start ()
    {

        myAnimator = GetComponent<Animator>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        StartCoroutine(animation());

    }

    IEnumerator animation()
    {
        yield return new WaitForSeconds(5f);

        myAnimator.SetBool("foldArms", true);

        yield return new WaitForSeconds(2f);

        myAnimator.SetBool("foldArms", false);
    }
}
