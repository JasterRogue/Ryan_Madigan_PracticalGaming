using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    PlayerControl pc;
    GameObject player;
    Transform playerTransform;
   

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        GameObject playerControl = GameObject.Find("CharacterObject");
        pc = playerControl.GetComponent<PlayerControl>();

    }
	
	// Update is called once per frame
	void Update () {

        checkDistanceToPlayer();
		
	}

    public void checkDistanceToPlayer()
    {
        Vector3 playerToChest;
        float playerToChestDistance;
        //playerToChest = Vector3.Distance(playerTransform.position, transform.position);
        playerToChest = new Vector3(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y, transform.position.z - playerTransform.position.z);
        playerToChestDistance = Mathf.Sqrt((playerToChest.x * playerToChest.x) + (playerToChest.y * playerToChest.y) + (playerToChest.z + playerToChest.z));
        print(playerToChestDistance);

        if(playerToChestDistance <= 2 && Input.GetKey(KeyCode.E))
        {
            pc.openChest();
        }    
    }
}
