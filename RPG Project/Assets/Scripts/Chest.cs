﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour {

    PlayerControl pc;
    GameObject player;
    Transform playerTransform;
    private Material mat;
    string[] items = new string []{"Attack Boost","Thera Leaf","Mana Leaf"};
    public string itemInChest;
    Inventory inventory;
    public Text itemText;
   

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        GameObject playerControl = GameObject.Find("CharacterObject");
        pc = playerControl.GetComponent<PlayerControl>();
        mat = gameObject.GetComponent<MeshRenderer>().material;
        generateItemToBeInChest();

        inventory = GetComponent<Inventory>();


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
            setAndShowText();
            print("Im fading");
            //inventory.inventoryItems.Add(itemInChest);
            GameObject.Destroy(gameObject);
        }    
    }//end of checkDistance to player

    public void generateItemToBeInChest()
    {
        itemInChest = items[Random.Range(0, 4)];
    }

    public void setAndShowText()
    {
        itemText.text = "Found a " + itemInChest;
    }
}