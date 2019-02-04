using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character { 

    public Player()
    {
        setCharacterName("Ethan");
        setLevel(1);
        setMaxHP(54);
        setMaxMP(30);
        setHP(getMaxHP());
        setMP(getMaxMP());
        setStrength(6);
        setDefence(4);
        setIntelligence(3);
        setSpecialDefence(4);
        setAgility(5);
        setLuck(2);
    }//end of player()
    
   

	// Use this for initialization
	void Start () {

    
		
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
            hpToIncrease = Random.Range(11, 14);
            setMaxHP(getMaxHP() + hpToIncrease);
            if (getMaxHP() > 999)
            {
                setMaxHP(999);
            }
            //MP will increase by a random amount
            mpToIncrease = Random.Range(5, 9);
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
}//end of class
