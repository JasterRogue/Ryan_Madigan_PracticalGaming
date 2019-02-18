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
    BattleManager myBattleManager;


	// Use this for initialization
	void Start ()
    {
        myPlayer = GameObject.Find("char_ethan").GetComponent<Player>();
        myBattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        allButttons = FindObjectsOfType<Button>();
        allButttons[1].onClick.AddListener(playerAttack);
        setUpStats();
    }
	
	// Update is called once per frame
	void Update ()
    {

   

        if (myPlayer.getHP() < hpUnder30Percent)
        {
            currentHPText.color = new Color(233, 255, 0);
        }

        else
        {
            currentHPText.color = new Color(255.0f, 255.0f, 255.0f);
        }

        if (myPlayer.getMP() < mpUnder30Percent)
        {
            currentMPText.color = new Color(233, 255, 0);
        }

        else
        {
            currentMPText.color = new Color(255.0f, 255.0f, 255.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myPlayer.setHP(myPlayer.getHP() - 40);
            myPlayer.setMP(myPlayer.getMP() - 23);
            setUpStats();
        }

        //if(myPlayer.isPlayerTurn)
        //{
        //    GUI.enabled = true;
        //}

        //else
        //{
        //    GUI.enabled = false;

        //}


    }

    public void playerAttack()
    {
        myBattleManager.playerHasAttacked();
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
