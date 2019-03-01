using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleText : MonoBehaviour {

    public Text nameText;
    public Text currentHPText;
    public Text maxHPText;
    public Text currentMPText;
    public Text maxMPText;

    Player myPlayer;
    int hpUnder30Percent;
    int mpUnder30Percent;
    Button[] allButttons;
    
	// Use this for initialization
	void Start ()
    {
        myPlayer = GameObject.Find("char_ethan").GetComponent<Player>();
        allButttons = FindObjectsOfType<Button>();
        allButttons[1].onClick.AddListener(playerAttack);
        setUpStats();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //if players hp is less than 30% change colour to yellow
        if (myPlayer.getHP() < hpUnder30Percent)
        {
            currentHPText.color = new Color(233, 255, 0);
        }

        else
        {
            currentHPText.color = new Color(255.0f, 255.0f, 255.0f);
        }

        //if players mp is less than 30% change colour to yellow
        if (myPlayer.getMP() < mpUnder30Percent)
        {
            currentMPText.color = new Color(233, 255, 0);
        }

        else
        {
            currentMPText.color = new Color(255.0f, 255.0f, 255.0f);
        }

    }

    public void playerAttack()
    {
        Global.manager.AttackButtonPressed();
    }

    public void setUpStats()
    {
        hpUnder30Percent = ((myPlayer.getMaxHP() * 30) / 100);
        mpUnder30Percent = ((myPlayer.getMaxMP() * 30) / 100);

        
        nameText.text = myPlayer.getCharacterName();
        currentHPText.text = myPlayer.getHP().ToString();
        maxHPText.text = " / " + myPlayer.getMaxHP().ToString();
        currentMPText.text = myPlayer.getMP().ToString();
        maxMPText.text = " / " + myPlayer.getMaxMP().ToString();

        nameText.color = new Color(255.0f, 255.0f, 255.0f);
        currentHPText.color = new Color(255.0f, 255.0f, 255.0f);
        maxHPText.color = new Color(255.0f, 255.0f, 255.0f);
        currentMPText.color = new Color(255.0f, 255.0f, 255.0f);
        maxMPText.color = new Color(255.0f, 255.0f, 255.0f);
    }
}
