using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour {

    public Text itemText;
    Chest myChest;
    private float timer;
    private bool timerActive;


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
		if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
                itemText.gameObject.SetActive(false);

        }
	}

    public void setAndShowText()
    {
        itemText.gameObject.SetActive(true);
        startCountdown();
        itemText.text = "Found a " + myChest.itemInChest;
    }

    private void startCountdown()
    {
        timerActive = true;
        timer = 3.0f;
    }

    public void itemTextHide()
    {
        itemText.gameObject.SetActive(false);
    }
}
