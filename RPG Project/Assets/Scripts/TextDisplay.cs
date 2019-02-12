using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    public Text itemText;
    Chest myChest;


    // Use this for initialization
    void Start ()
    {
        myChest = GameObject.Find("TestChest").GetComponent<Chest>();
        itemText = GameObject.Find("TextBox").GetComponentInChildren<Text>();

        itemText.gameObject.SetActive(false);
       // itemText.GetComponent<Text>().enabled = false;

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void setAndShowText()
    {
        itemText.gameObject.SetActive(true); 
        itemText.text = "Found a " + myChest.itemInChest;
    }

    public void itemTextHide()
    {
        itemText.gameObject.SetActive(false);
    }
}
