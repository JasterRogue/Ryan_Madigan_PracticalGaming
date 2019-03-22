using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour {

    public Transform target;
    public Transform obstruction;
    float zoomSpeed = 2f;
    PlayerControl myPlayerControl;

	// Use this for initialization

	void Start ()
    {

        obstruction = target;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ViewObstructed();
		
	}

    void ViewObstructed()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, target.position - transform.position, out hit, 4.5f))
        {
            if(hit.collider.gameObject.tag != "Player")
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if(Vector3.Distance(obstruction.position, transform.position) >=3f && Vector3.Distance(transform.position, target.position) >= 1.5f)
                {
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
                }

                else
                {
                    obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                    if (Vector3.Distance(transform.position, target.position) < 4.5f)
                        transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
                }
            }
        }
    }//end of viewObstructed()
}
