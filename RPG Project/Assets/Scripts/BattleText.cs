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
        allButttons[0].onClick.AddListener(magicAttack);
        allButttons[2].onClick.AddListener(escape);
        allButttons[3].onClick.AddListener(useItem);
        setUpStats();

        for(int i = 0;i < allButttons.Length; i++)
        {
            print("Button " + i + " is " + allButttons[i]);
        }  
    }
	
	// Update is called once per frame
	void Update ()
    {
		setUpStats ();

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

    public void magicAttack()
    {
        Global.manager.MagicButtonPressed();
    }

    public void escape()
    {
        Global.manager.EscapeButtonPressed();
    }

    public void useItem()
    {
        //add code
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
