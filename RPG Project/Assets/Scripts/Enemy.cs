﻿using System;
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
    public bool isEnemyTurn;
    private bool magicAnimationStarted;
	private bool healAnimationStarted;
	private bool meleeAnimationStarted;
	TextMeshScript my3DText;
	public GameObject damageText;
    TextMeshScript myTextMesh;
    FireEffect myFireEffect;
	private float hpPercent;
	private float mpPercent;
	BattleText myBattleText;

    public Enemy()
    {
        setCharacterName("Bandit");
        setLevel(1);
        setMaxHP(80);
        setMaxMP(30);
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

        myTextMesh = FindObjectOfType<TextMeshScript>();

        myFireEffect = FindObjectOfType<FireEffect>();

		myBattleText = FindObjectOfType<BattleText> ();


    }

    // Update is called once per frame
    void Update()
    {
        if (getHP()>1)
        {
            info = myAnimator.GetCurrentAnimatorClipInfo(0);

			my3DText = FindObjectOfType<TextMeshScript> ();

            switch (isCurrently)
            {
                case EnemyState.Idle:

                    break;

				case EnemyState.ChooseAction:
				

                    if (getHP() < getMaxHP() / 4 && getMP() >= 8)
                    {
                        //use heal

                        myAnimator.SetBool("isHealing", true);
						setHP((getIntelligence() * 2) + 15);
                        setMP(getMP() - 8);
                        //print("Heal");
						healAnimationStarted = false;
						myAnimator.SetBool ("isHealing", true);
                        isCurrently = EnemyState.Heal;
						healAnimationStarted = false;
						hpPercent = (float)getHP () / (float)getMaxHP ();
						mpPercent = (float)getMP () / (float)getMaxMP ();
						myBattleText.updateMP (mpPercent);
						myBattleText.updateHP (hpPercent);

                    }

                    else
                    {
                        if (getMP() >= 10)
                        {
                            setMP(getMP() - 10);
							mpPercent = (float)getMP () / (float)getMaxMP ();
							myBattleText.updateMP (mpPercent);
                            print("Magic Attack");
                            calculateMagicDamage();
							applyDamage(damage);
							myAnimator.SetBool ("isCastingMagic", true);
                            //do magic attac
                            //  myTextMesh.showText(damage, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z));
                            myTextMesh.createText(new Vector3(playerStats.transform.position.x, playerStats.transform.position.y + 2, playerStats.transform.position.z), damage);

                            isCurrently = EnemyState.MagicAttack;
                            magicAnimationStarted = false;
                            myFireEffect.playFireEffect(transform.position);
                        }
                        else
                        {
                            originalPosition = new Vector3(-0.82f, 0.045f, 5.576f);
                            //do melee attack
                            calculateMeleeDamage();

                            isCurrently = EnemyState.runTo;
                        }
                    }

                    break;

                case EnemyState.Heal:

				if ((!healAnimationStarted) && (info[0].clip.name == "Standing_2H_Cast_Spell_01")) healAnimationStarted = true;

				if (healAnimationStarted && (info[0].clip.name == "idle_A"))
                    {
                        myAnimator.SetBool("isHealing", false);
                        isCurrently = EnemyState.Idle;
                        TurnFinished();
                        isEnemyTurn = false;
                    }

                    break;

                case EnemyState.MagicAttack:

                   // myFireEffect.playFireEffect(transform.position);

                    if ((!magicAnimationStarted) && (info[0].clip.name == "Standing_2H_Magic_Attack_02")) magicAnimationStarted = true;


                    if (magicAnimationStarted && (info[0].clip.name == "idle_A"))
                    {
                        myAnimator.SetBool("isCastingMagic", false);
                        isCurrently = EnemyState.Idle;
                        TurnFinished();
                        isEnemyTurn = false;
                    }

                    break;

                case EnemyState.runTo:

                    runToPlayer();

                    if (hasReachedPlayer())
                    {
                        isCurrently = EnemyState.MeleeAttack;
						meleeAnimationStarted = false;
                        myAnimator.SetBool("isAttacking", true);
						myTextMesh.createText(new Vector3(playerStats.transform.position.x, playerStats.transform.position.y + 2, playerStats.transform.position.z), damage);

                    }

                    break;

                case EnemyState.MeleeAttack:

				if ((!meleeAnimationStarted) && (info[0].clip.name == "hk_side_left_A")) meleeAnimationStarted = true;

					if (meleeAnimationStarted && (info[0].clip.name == "idle_A"))
                    {
                        myAnimator.SetBool("isAttacking", false);
                        isCurrently = EnemyState.Idle;
						print("melee attack");
                        applyDamage(damage);
                        transform.position = originalPosition;
                        TurnFinished();
                        isEnemyTurn = false;
                    }

                    break;


            }//end of switch()
        }

        else
        {
            //enemy is dead
            //GameObject.Destroy(gameObject);
            EnemyHasDied();
            myAnimator.SetBool("isDead", true);
        }
    }

    public void updateStats()
    {
        if(playerStats.getLevel() >= 1 && playerStats.getLevel() <= 4)
        {
            setMaxHP(80);
            setMaxMP(30);
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
	}//end of enemyTurn()

    private void EnemyHasDied()
    {
        isEnemyAlive = false;
    }

    internal override void waitForAttackChoice()
    {
        isCurrently = EnemyState.ChooseAction;
    }

    public void enemyDamageTaken(int damage)
    {
        setHP(getHP() - damage);
		hpPercent = (float)getHP () / (float)getMaxHP ();
		myBattleText.updateHP (hpPercent);

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
