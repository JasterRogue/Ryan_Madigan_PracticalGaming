using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour {

    // Use this for initialization

    public static BattleManager manager;
    

	void Start () {

       
        manager = FindObjectOfType<BattleManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
