﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {


    enum PlayerModes { WaitForUserInput,WaitForTurn}

    PlayerModes currently = PlayerModes.WaitForTurn;
    public bool isPlayerTurn;
    public int damage;
    int chanceOfCritical;
    int variedDamage;
    int variedPercent;
    //BattleManager theManager;
    Enemy enemyStats;
    Weapon weaponDamage;
    PlayerControl animateChar;
    public bool isPlayerAlive;
	TextMeshScript my3DText;
	public string damageText;
	BattleText myBattleText;
	private float hpPercent;

    public Player()
    {
        setCharacterName("Yuri");
        setLevel(1);
        setMaxHP(54);
        setMaxMP(30);
        setHP(getMaxHP());
        setMP(getMaxMP());
        setStrength(12);
        setDefence(10);
        setIntelligence(7);
        setSpecialDefence(9);
        setAgility(11);
        setLuck(5);
        
    }//end of player()
    
   

	// Use this for initialization
	void Start ()
    {
      
        Global.manager.ImHere(this);
        animateChar = GetComponent<PlayerControl>();
        isPlayerAlive = true;

    }//end of start()
	
	// Update is called once per frame
	void Update ()
    {

        switch (currently)
        {

            case PlayerModes.WaitForUserInput:

                break;
        }

        if(Global.manager.inBattle == true)
        {
            enemyStats = GameObject.FindObjectOfType<Enemy>();

			my3DText = GameObject.FindObjectOfType<TextMeshScript> ();

			myBattleText = FindObjectOfType<BattleText> ();
           // weaponDamage = GameObject.FindObjectOfType<Weapon>();
        }

        if(getHP()<1)
        {
            playerHasDied();
        }
		
	}//end of update()

    public void levelUp()
    {
        int hpToIncrease;
        int mpToIncrease;

        if (getLevel() < 99)
        {
            setLevel(getLevel() + 1);
            //HP will increase by random amount 
            hpToIncrease = UnityEngine.Random.Range(12, 18);
            setMaxHP(getMaxHP() + hpToIncrease);
            if (getMaxHP() > 999)
            {
                setMaxHP(999);
            }
            //MP will increase by a random amount
            mpToIncrease = UnityEngine.Random.Range(7, 10);
            setMaxMP(getMaxMP() + mpToIncrease);

            if (getLevel() % 2 == 0)
            {
                //Level is an even number, increase these stats
                //Strength, SpecialDefence, Agility
                setStrength(getStrength() + 2);
                setSpecialDefence(getSpecialDefence() + 2);
                setAgility(getAgility() + 2);
            }//end of if level even

            else
            {
                //Level is an odd number, increase these stats
                //Defence, Intelligence, Luck
                setDefence(getDefence() + 2);
                setIntelligence(getIntelligence() + 2);
                setLuck(getLuck() + 1);
               
            }//end of else

            recover();

        }//end of if(level<99)
    }//end of levelUp()

    public void recover()
    {
        //recovers players hp and mp 
        setHP(getMaxHP());
        setMP(getMaxMP());

    }

    public void playerTurn()
    {
        isPlayerTurn = true;
    }

    public void playerAttack()
    {
        //calculates  the damage output
        damage = (((getStrength() * 2)) - enemyStats.getDefence());
        chanceOfCritical = UnityEngine.Random.Range(1, 101);

        if (damage < 1)
        {
            //opponents defence could be so high that the damage ends up as negative number
            damage = 1;
        }

        if (chanceOfCritical <= getLuck())
        {
            damage = (damage + (damage / 2));
        }
        variedPercent = UnityEngine.Random.Range(0, 21);
        variedDamage = ((damage * variedPercent) / 100);
        damage = damage + variedDamage;

    }

    public void magicAttack()
    {
        damage = (((getIntelligence() * 3)) - enemyStats.getSpecialDefence());

        if(damage < 1)
        {
            damage = 1;
        }

        variedPercent = UnityEngine.Random.Range(0, 21);
        variedDamage = ((damage * variedPercent) / 100);
        damage = damage + variedDamage;
    }

    public void calculateMagicDamage()
    {
        damage = ((getIntelligence() * 3) - enemyStats.getSpecialDefence());

        if (damage < 1)
        {
            //opponents defence could be so high that the damage ends up as negative number
            damage = 1;
        }

        variedPercent = UnityEngine.Random.Range(0, 21);
        variedDamage = ((damage * variedPercent) / 100);
        damage = damage + variedDamage;
    }

    internal void MeleeAttack()
    {
        animateChar.initiateMeleeAttack();
        //activates attack animation
    }

    internal void MagicCast()
    {
        //activates magic attack animation
        animateChar.initiateMagicAttack();
    }

    internal void EscapeAttempt()
    {
        animateChar.initiateEscapeAttempt();
    }

    internal override void waitForAttackChoice()
    {
        currently = PlayerModes.WaitForUserInput;
    }

    void playerHasDied()
    {
        isPlayerAlive = false;
    }

    public void applyDamage(int damage)
    {
        //applies damage calculated
        enemyStats.setHP(enemyStats.getHP() - damage);
		hpPercent = (float)enemyStats.getHP () / (float)enemyStats.getMaxHP ();
		myBattleText.updateHP (hpPercent);
    }

    public void DamageCall()
    {
        //this method calls the the method to apply damage that was calculated
        applyDamage(damage);
    }
}//end of class
