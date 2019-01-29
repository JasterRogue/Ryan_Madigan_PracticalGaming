using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    private string characterName;
    private int level;
    private int HP;
    private int MP;
    private int strength;
    private int defence;
    private int intelligence;
    private int specialDefence;
    private int agility;
    private int luck;

    //Constructors
    public Character()
    {
        characterName = "";
        level = 0;
        HP = 0;
        MP = 0;
        strength = 0;
        defence = 0;
        intelligence = 0;
        specialDefence = 0;
        agility = 0;
        luck = 0;
    }

    //Setter methods
    public void setCharacterName(string characterName)
    {
        this.characterName = characterName;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    public void setHP(int hp)
    {
        this.HP = hp;
    }

    public void setMP(int mp)
    {
        this.MP = mp;
    }

    public void setStrength(int strength)
    {
        this.strength = strength;
    }

    public void setDefence(int defence)
    {
        this.defence = defence;
    }

    public void setIntelligence(int intelligence)
    {
        this.intelligence = intelligence;
    }

    public void setSpecialDefence(int specialDefence)
    {
        this.specialDefence = specialDefence;
    }

    public void setAgility(int agility)
    {
        this.agility = agility;
    }

    public void setLuck(int luck)
    {
        this.luck = luck;
    }

    //getter methods

    public string getCharacterName()
    {
        return characterName;
    }

    public int getLevel()
    {
        return level;
    }

    public int getHP()
    {
        return HP;
    }

    public int getMP()
    {
        return MP;
    }

    public int getStrength()
    {
        return strength;
    }

    public int getDefence()
    {
        return defence;
    }

    public int getIntelligence()
    {
        return intelligence;
    }

    public int getSpecialDefence()
    {
        return specialDefence;
    }

    public int getAgility()
    {
        return agility;
    }

    public int getLuck()
    {
        return luck;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
