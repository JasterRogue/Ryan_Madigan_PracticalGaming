using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character {

    enum EnemyState { Idle, ChooseAction, MeleeAttack, MagicAttack, Heal, runTo}
    EnemyState isCurrently = EnemyState.Idle;


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
    AnimatorClipInfo[] info;

    public Enemy()
    {
        setCharacterName("Bandit");
        setLevel(1);
        setMaxHP(80);
        setMaxMP(50);
        setHP(getMaxHP());
        setMP(getMaxMP());
        setStrength(8);
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
    void Update()



    {


        switch (isCurrently)
        {
            case EnemyState.Idle:




                break;

            case EnemyState.ChooseAction:

                if (getHP() < getMaxHP() / 4 && getMP() >= 8)
                {
                    //use heal

                    myAnimator.SetBool("isHealing", true);

                    info = myAnimator.GetCurrentAnimatorClipInfo(0);



                    setHP(getHP() + 30);
                    setMP(getMP() - 8);
                    print("Magic heal");
                    isCurrently = EnemyState.Heal;
                }

                else
                {
                    if (getMP() >= 10)
                    {
                        setMP(getMP() - 10);
                        print("Magic Attack");
                        calculateMagicDamage();

                        myAnimator.SetBool("isCastingMagic", true);
                        //do magic attack
                        applyDamage(damage);

                        info = myAnimator.GetCurrentAnimatorClipInfo(0);

                        isCurrently = EnemyState.MagicAttack;

                    }
                    else
                    {
                        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                        //do melee attack
                        calculateMeleeDamage();



                        isCurrently = EnemyState.runTo;


                    }
                }


                break;

            case EnemyState.Heal:


                if (info[0].clip.name == "idle_A")
                {
                    myAnimator.SetBool("isHealing", false);
                    isCurrently = EnemyState.Idle;
                    TurnFinished();
                }






                break;

            case EnemyState.MagicAttack:


                if (info[0].clip.name == "idle_A")
                {
                    myAnimator.SetBool("isCastingMagic", false);
                    isCurrently = EnemyState.Idle;
                    TurnFinished();
                }

                break;

            case EnemyState.runTo:


                runToPlayer();

                if (hasReachedPlayer())
                {
                    isCurrently = EnemyState.MeleeAttack;

                }




                break;


            case EnemyState.MeleeAttack:

                if (info[0].clip.name == "idle_A")
                {
                    myAnimator.SetBool("isAttacking", false);
                    isCurrently = EnemyState.Idle;
                    applyDamage(damage);
                    transform.position = originalPosition;
                    TurnFinished();
                }

                break;






        }
    }
  //      if(Global.manager.inBattle)
  //      {
  //          isEnemyAlive = true;
  //      }

  //      if(getHP()<1)
  //      {
  //          GameObject.Destroy(gameObject);
  //          EnemyHasDied();
  //      }



		//if (hasReachedPlayer ())
		//{
		//	myAnimator.SetBool ("isAttacking", true);

  //          if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("hk_side_left_A"))
  //          {
  //              myPlayerControl.playerKnockdown();
  //          }
		//}    
  //  }

    public void updateStats()
    {
        if(playerStats.getLevel() >= 1 && playerStats.getLevel() <= 4)
        {
            setMaxHP(80);
            setMaxMP(50);
            setHP(getMaxHP());
            setMP(getMaxMP());
            setStrength(8);
            setDefence(6);
            setIntelligence(4);
            setSpecialDefence(12);
            setAgility(8);
            setLuck(4);
        }

        if (playerStats.getLevel() >= 5 && playerStats.getLevel() <= 12)
        {
            setMaxHP(170);
            setMaxMP(90);
            setHP(getMaxHP());
            setMP(getMaxMP());
            setStrength(18);
            setDefence(15);
            setIntelligence(12);
            setSpecialDefence(12);
            setAgility(18);
            setLuck(9);
        }

        if (playerStats.getLevel() >= 13 && playerStats.getLevel() <= 30)
        {
            setMaxHP(280);
            setMaxMP(155);
            setHP(getMaxHP());
            setMP(getMaxMP());
            setStrength(32);
            setDefence(27);
            setIntelligence(20);
            setSpecialDefence(21);
            setAgility(35);
            setLuck(16);
        }
    }//updates enemy stats based on player level

    public void setup()
    {
        setHP(getMaxHP());
        setMP(getMaxMP());
    }

    public void enemyTurn()
    {

        isCurrently = EnemyState.ChooseAction;

		//list of attack
		//1 melee, 2 range, 3 heal
        /*
        if (getHP() > 1)
        {
            playerStats.isPlayerTurn = false;

           	//decide on move


			else if 

			
            TurnFinished();
		
        }

        else
        {
            GameObject.Destroy(gameObject);
            EnemyHasDied();
            myAnimator.SetBool("isDead", true);
        }
        */
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

    public void calculateMagicDamage()
    {
        damage = ((getIntelligence() * 3) - playerStats.getSpecialDefence());

        if (damage < 1)
        {
            //opponents defence could be so high that the damage ends up as negative number
            damage = 1;
        }

        variedPercent = UnityEngine.Random.Range(0, 21);
        variedDamage = ((damage * variedPercent) / 100);
        damage = damage + variedDamage;
    }

	public bool hasReachedPlayer()
	{
		if (Vector3.Distance (transform.position, myPlayerControl.transform.position) <= 4) {
			return true;
		}
		else
		{
			return false;
		}
	}
}
