﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character {

    int damage;
    int chanceOfCritical;
    int variedDamage;
    int variedPercent;
    Random randomCritical = new Random();
    Player playerStats;



    public Enemy()
    {
        setCharacterName("Cube of Destruction");
        setLevel(1);
        setMaxHP(80);
        setMaxMP(50);
        setHP(getMaxHP());
        setMP(getMaxMP());
        setStrength(80);
        setDefence(6);
        setIntelligence(4);
        setSpecialDefence(12);
        setAgility(8);
        setLuck(4);
        
    }

	// Use this for initialization
	void Start ()
    {
        playerStats = GameObject.Find("char_ethan").GetComponent<Player>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        
		
	}

    public void setup()
    {
        setHP(getMaxHP());
        setMP(getMaxMP());
    }

    public void enemyTurn()
    {

        if (getHP() > 1)
        {
            playerStats.isPlayerTurn = false;

            //calculates  the damage output
            damage = ((getStrength() * 2) - playerStats.getDefence());
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
            print("Enemy:  " + damage);
            playerStats.setHP(playerStats.getHP() - damage);

            playerStats.isPlayerTurn = true;
        }

        else
        {
            GameObject.Destroy(gameObject);
        }




    }
}
