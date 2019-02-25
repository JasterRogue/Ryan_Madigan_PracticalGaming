using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    /*This script will be used for the inventory,
     *the inventory will store all possible items the player can carry */

    List<Item> Items;

	// Use this for initialization
	void Start ()
    {
        Items = new List<Item>();

    }//end of start()
	
	// Update is called once per frame
	void Update ()
    {
		
	}//end of update()

    public void addTo(Item newItem)
    {
        Items.Add(newItem);
    }

    internal Item getItem(int v)
    {
        return Items[v];
    }
}
