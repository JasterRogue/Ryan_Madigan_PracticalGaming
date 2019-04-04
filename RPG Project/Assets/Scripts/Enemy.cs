using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character {

    int damage;
    int chanceOfCritical;
    int variedDamage;
    int variedPercent;
    UnityEngine.Random randomCritical = new UnityEngine.Random();
    Player playerStats;
    PlayerControl myPlayerControl;
    public bool isEnemyAlive;
    Animator myAnimator;
	Vector3 originalPosition;

    public Enemy()
    {
        setCharacterName("Bandit");
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

        myPlayerControl = GameObject.FindObjectOfType<PlayerControl>();

        Global.manager.ImHere(this);

        myAnimator = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Global.manager.inBattle)
        {
            isEnemyAlive = true;
        }

        if(getHP()<1)
        {
            GameObject.Destroy(gameObject);
            EnemyHasDied();
        }

		if(myAnimator.GetBool("isRunning") && !hasReachedPlayer())
		{
			runToPlayer ();
		}

		if (hasReachedPlayer ())
		{
			myAnimator.SetBool ("isAttacking", true);
		}



   

    }

    public void setup()
    {
        setHP(getMaxHP());
        setMP(getMaxMP());
    }

    public void enemyTurn()
    {
		//list of attack
		//1 melee, 2 range, 3 heal

        if (getHP() > 1)
        {
            playerStats.isPlayerTurn = false;

           	//decide on move

			if (getHP () < getMaxHP () / 4 && getMP () >= 8) {
				//use heal
				setHP (getHP () + 30);
				setMP (getMP () - 8); 
			} 

			else if (getMP () >= 10)
			{
				setMP (getMP () - 10);
				//do magic attack
			}

			else
			{
				originalPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
				//do melee attack
				calculateMeleeDamage();

				//run to player
				myAnimator.SetBool("isRunning", true);
				//do attack
				applyDamage(damage);

				myPlayerControl.playerKnockdown();

				//go back to position
				transform.position = originalPosition;
			}
		
        }

        else
        {
            GameObject.Destroy(gameObject);
            EnemyHasDied();
            myAnimator.SetBool("isDead", true);
        }

	}//end of enemyTurn()

    private void EnemyHasDied()
    {
        isEnemyAlive = false;
    }

    internal override void waitForAttackChoice()
    {
      
    }

    public void enemyDamageTaken(int damage)
    {
        setHP(getHP() - damage);
    }

    public void applyDamage(int damage)
    {
        playerStats.setHP(playerStats.getHP() - damage);
    }

    void runToPlayer()
    {
		myAnimator.SetBool ("isRunning", true);
		transform.Translate(Vector3.forward * Time.deltaTime);

    }

	public void calculateMeleeDamage()
	{
		//calculates  the damage output
		damage = ((getStrength() * 2) - playerStats.getDefence());
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

	public bool hasReachedPlayer()
	{
		if (Vector3.Distance (transform.position, myPlayerControl.transform.position) <= 5) {
			return true;
		}
		else
		{
			return false;
		}
	}
}
