﻿using System.Collections;
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
    public float itemTextTime = 0.0f;
    public Transform chest;
    
   

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        GameObject playerControl = GameObject.Find("CharacterObject");
        myPlayerControl = playerControl.GetComponent<PlayerControl>();

        mat = gameObject.GetComponent<MeshRenderer>().material;

        generateItemToBeInChest();

        myTextDisplay = GameObject.Find("TextBox").GetComponent<TextDisplay>();

        //myInventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        //Instantiate(chest, new Vector3(5,1,5),Quaternion.identity);



    }//end of start()
	
	// Update is called once per frame
	void Update ()
    {

        checkDistanceToPlayer();
	}//end of update()

    public void checkDistanceToPlayer()
    {
        Vector3 playerToChest;
        float playerToChestDistance;
        //playerToChest = Vector3.Distance(playerTransform.position, transform.position);
        playerToChest = new Vector3(transform.position.x - playerTransform.position.x, transform.position.y - playerTransform.position.y, transform.position.z - playerTransform.position.z);
        playerToChestDistance = Mathf.Sqrt((playerToChest.x * playerToChest.x) + (playerToChest.y * playerToChest.y) + (playerToChest.z + playerToChest.z));
       // print(playerToChestDistance);

        if(playerToChestDistance <= 2 && Input.GetKey(KeyCode.E))
        {
            //pc.openChest();

            while (mat.color.a > 0)
            {
                Color newColor = mat.color;
                newColor.a -= Time.deltaTime;
                mat.color = newColor;
                gameObject.GetComponent<MeshRenderer>().material = mat;
                
            }
            
            print("Im fading");
            myTextDisplay.setAndShowText();
           
            GameObject.Destroy(gameObject);
        }    
    }//end of checkDistance to player

    public void generateItemToBeInChest()
    {
        itemInChest = items[Random.Range(0, 3)];
    }


    

    
}
