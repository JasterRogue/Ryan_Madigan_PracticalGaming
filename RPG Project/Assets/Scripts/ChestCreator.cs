using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCreator : MonoBehaviour {

    public Transform chest;

    // Use this for initialization
    void Start () {

        Instantiate(chest, new Vector3(5, 1, 5), Quaternion.identity);
        Instantiate(chest, new Vector3(1, 1, 3), Quaternion.identity);
        

}
	
	// Update is called once per frame
	void Update () {
		
	}
}
