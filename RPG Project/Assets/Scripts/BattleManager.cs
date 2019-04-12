using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{

    enum BattlePhase {NonCombat,  InformOfTurn, WaitForTurnToEnd, ChooseNextCombatant}

    BattlePhase currently = BattlePhase.NonCombat;
    /*The purpose of this script is to deal 
     with battle encounters for the game.
     It will generate the requirements to 
     start a battle and keep track of majority of battle 
     related stuff*/

    PlayerControl myPlayerControl;//access to player control script
    Player player;//access to player script
    BattleText myBattleText;
    Enemy enemy;//access to enemy script
    int stepsToBattle;
    int currentExp = 0;
    int expNeededToLevelUp = 60;
    public bool inBattle;
    public bool playerAttacking;

    List<Character> combatants;
    int whosTurn = 0;
    private int indexOfPlayer;

    // Start is called before the first frame update
    void Start()
    {
        stepsToBattle = UnityEngine.Random.Range(150, 301);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Menu Scene");

        combatants = new List<Character>();


    }//end of start()

    internal void ImHere(Player newplayer)
    {
        player = newplayer;
       
        myPlayerControl = newplayer.gameObject.GetComponentInChildren<PlayerControl>();
        myPlayerControl.StepsToNextBattleIs(stepsToBattle, inBattle);

        combatants.Add(player);

        indexOfPlayer = combatants.IndexOf(player);

        whosTurn = indexOfPlayer;       
        
    }

    internal void MagicButtonPressed()
    {
        if (whosTurn == indexOfPlayer)
        {
            (combatants[indexOfPlayer] as Player).MagicCast();
            player.magicAttack();
            playerAttacking = true;
        }

        if (!enemy)
        {
            enemy = FindObjectOfType<Enemy>();
        }//if enemy doesnt exist find it

        

        if (!myBattleText)
        {
            myBattleText = FindObjectOfType<BattleText>();
        }


        if (myBattleText)
        {
            myBattleText.setUpStats();
        }
    }

    internal void ImHere(Enemy newplayer)
    {
        combatants.Add(newplayer);

        newplayer.updateStats();

    }

    internal void StepCountReached()
    {
        SceneManager.LoadScene("Battle Scene");

        inBattle = true;

        combatants = new List<Character>();
  
        currently = BattlePhase.InformOfTurn;
    }

    // Update is called once per frame
    void Update()
    {
        //if (enemy && whosTurn != indexOfPlayer && !enemy.isEnemyTurn)
        //{
        //    enemy.isEnemyTurn = true;
        //    enemy.enemyTurn();
        //    print("enemy turn called");
        //}

        switch (currently)
        {

            case BattlePhase.InformOfTurn:

                combatants[whosTurn].StartCombatTurn();
                currently = BattlePhase.WaitForTurnToEnd;
                break;

            case BattlePhase.WaitForTurnToEnd:


                break;

            case BattlePhase.ChooseNextCombatant:

                whosTurn = (whosTurn + 1) % combatants.Count;

                currently = BattlePhase.InformOfTurn;

                break;
        }

        if(enemy && inBattle)
        {
             if(enemy.getHP()<1)
               {
                        SceneManager.LoadScene("Test Scene");
                        inBattle = false;
                        stepsToBattle = UnityEngine.Random.Range(150, 301);
                        myPlayerControl.StepsToNextBattleIs(stepsToBattle, inBattle);
                        currentExp += 30;
                        checkForLevelUp();
                        myPlayerControl.stepCount = 0;
               }
        }//end of if enemey exists
       
        if(player)
        {
            if(!player.isPlayerAlive && inBattle)
                    {
                        //Game over sequence;
                        inBattle = false;
                        Initiate.Fade("Game Over", Color.black, 5);
                    }
        }//end of if player exists

    }//end of update()

    public void checkForLevelUp()
    {
        if (currentExp >= expNeededToLevelUp && player.getLevel()<99)
        {
            print("Level Up");
            int lastExpNeeded = expNeededToLevelUp;
            expNeededToLevelUp = lastExpNeeded + lastExpNeeded / 4;
            player.levelUp();
        }//end of if exp needed
    }

    internal void IveFinishedMyTurn()
    {
        currently = BattlePhase.ChooseNextCombatant;
    }

    public void AttackButtonPressed()
    {
        if (whosTurn == indexOfPlayer)//issue here not getting past this statement
        {
            (combatants[indexOfPlayer] as Player).MeleeAttack();
            player.playerAttack();
            print("Player attack called");
            playerAttacking = true;
        }

        if(!enemy)
        {
            enemy = FindObjectOfType<Enemy>();
        }//if enemy doesnt exist find it

        if (!myBattleText)
        {
            myBattleText = FindObjectOfType<BattleText>();
        }

        if (myBattleText)
        {
            myBattleText.setUpStats();
        }
        
    }
}//end of class
