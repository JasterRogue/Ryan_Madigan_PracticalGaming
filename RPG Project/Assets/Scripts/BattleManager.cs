﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {

       // myBattleText = GameObject.Find("BattleGUI").GetComponent<BattleText>();



       // enemy = GameObject.Find("CubeOfDestruction").GetComponent<Enemy>();

        stepsToBattle = UnityEngine.Random.Range(150, 301);
        print(stepsToBattle);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Test Scene");

    }//end of start()

    internal void ImHere(Player newplayer)
    {
        player = newplayer;
        if (!Global.manager.inBattle)
        {
            myPlayerControl = newplayer.gameObject.GetComponentInChildren<PlayerControl>();
            myPlayerControl.StepsToNextBattleIs(stepsToBattle);
        }
        
    }

    internal void StepCountReached()
    {
        print("Battle has occured");
        // myBattleText.setUpStats();
        SceneManager.LoadScene("Battle Scene");
        battleHasOccured();
    }

    // Update is called once per frame
    void Update()
    {     
  
        

        //if(enemy.getHP() < 1)
        //{
        //    SceneManager.LoadScene("Test Scene");
        //    stepsToBattle = Random.Range(150, 301);
        //    currentExp += 30;
        //    checkForLevelUp();
        //    myPlayerControl.stepCount = 0;
        //}
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

    public void playerHasAttacked()
    {
        player.playerAttack();

        enemy.enemyTurn();

        myBattleText.setUpStats();

        
    }

    void battleHasOccured()
    {
        inBattle = true;
    }



  
}
