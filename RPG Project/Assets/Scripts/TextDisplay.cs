using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    public Text itemText;
    Chest tdc;
    Chest myChest;


    // Use this for initialization
    void Start ()
    {
       // GameObject textDisplayChest = GameObject.Find("Chest");
       // tdc = textDisplayChest.GetComponent<Chest>();
         myChest = gameObject.GetComponent<Chest>();

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void setAndShowText()
    {
        itemText.text = "Found a " + myChest.itemInChest;
    }
}
