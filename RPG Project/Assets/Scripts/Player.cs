﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public bool isPlayerTurn;
    int damage;
    int chanceOfCritical;
    int variedDamage;
    int variedPercent;

    Enemy enemyStats;

    public Player()
    {
        setCharacterName("Ethan");
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
        enemyStats = GameObject.Find("CubeOfDestruction").GetComponent<Enemy>();

    
		
	}//end of start()
	
	// Update is called once per frame
	void Update () {
		
	}//end of update()

    public void levelUp()
    {
        int hpToIncrease;
        int mpToIncrease;

        if (getLevel() < 99)
        {
            setLevel(getLevel() + 1);
            //HP will increase by random amount 
            hpToIncrease = Random.Range(12, 18);
            setMaxHP(getMaxHP() + hpToIncrease);
            if (getMaxHP() > 999)
            {
                setMaxHP(999);
            }
            //MP will increase by a random amount
            mpToIncrease = Random.Range(7, 10);
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
        damage = ((getStrength() * 2) - enemyStats.getDefence());
        chanceOfCritical = Random.Range(1, 101);

        if (damage < 1)
        {
            //opponents defence could be so high that the damage ends up as negative number
            damage = 1;
        }

        if (chanceOfCritical <= getLuck())
        {
            damage = (damage + (damage / 2));
        }
        variedPercent = Random.Range(0, 21);
        variedDamage = ((damage * variedPercent) / 100);
        damage = damage + variedDamage;
        print(damage);

    }
}//end of class
