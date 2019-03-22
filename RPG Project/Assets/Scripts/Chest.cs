using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    PlayerControl myPlayerControl;
    GameObject player;
    Transform playerTransform;
    private Material mat;
    string[] items = new string []{"Attack Boost","Thera Leaf","Mana Leaf"};
    public string itemInChest;
    Inventory myInventory;
    TextDisplay myTextDisplay;

    internal void YouContain(Item newItem)
    {
        myInventory.addTo(newItem);
    }

    public float itemTextTime = 0.0f;
    healthItem myHealthItem;
    mpItem myMPItem;
    
    
   

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        GameObject playerControl = GameObject.Find("CharacterObject");
        myPlayerControl = playerControl.GetComponent<PlayerControl>();

        mat = gameObject.GetComponent<MeshRenderer>().material;

        generateItemToBeInChest();

        myTextDisplay = FindObjectOfType<TextDisplay>();

        //myInventory = GameObject.Find("Inventory").GetComponent<Inventory>();

    }//end of start()
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKey(KeyCode.E))
        {
            checkDistanceToPlayer();
        }
        
	}//end of update()

    public void checkDistanceToPlayer()
    {
        Vector3 playerToChest;
        float playerToChestDistance;

        playerToChest = new Vector3(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y, transform.position.z - playerTransform.position.z);
        playerToChestDistance = Mathf.Sqrt((playerToChest.x * playerToChest.x) + (playerToChest.y * playerToChest.y) + (playerToChest.z + playerToChest.z));
      
        if(playerToChestDistance <= 4)
        {  
            myTextDisplay.setAndShowText();
           
            GameObject.Destroy(gameObject);

            myHealthItem = new healthItem("Thera Leaf", 90);

            //myInventory.addTo(myHealthItem);

        }    
    }//end of checkDistance to player

    public void generateItemToBeInChest()
    {
        itemInChest = items[UnityEngine.Random.Range(0, 3)];
    }


    

    
}
