using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {

    public bool isDoorOpen = false;
    Player myPlayer;

    // Use this for initialization
    void Start ()
    {
        myPlayer = GameObject.FindObjectOfType<Player>();
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if(Input.GetKey(KeyCode.E))
        {
            canOpenDoor();
        }
		
	}

    public void canOpenDoor()
    {
        if(!myPlayer)
        {
            myPlayer = GameObject.FindObjectOfType<Player>();
        }

        float distance = Vector3.Distance(transform.position, myPlayer.transform.position);

        if (distance <= 40)
        {
           if(!isDoorOpen)
             {
               print("Door open");
             }

        }
        

    }
}
