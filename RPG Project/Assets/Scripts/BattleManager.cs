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

    PlayerControl pc;//access to player control script
    Player player;//access to player script
    BattleText myBattleText;
    Enemy enemy;//access to enemy script
    int stepsToBattle;
    int currentExp = 0;
    int expNeededToLevelUp = 60;

    // Start is called before the first frame update
    void Start()
    {
        myBattleText = GameObject.Find("BattleGUI").GetComponent<BattleText>();
        pc = GameObject.Find("char_ethan").GetComponent<PlayerControl>();
        player = FindObjectOfType<Player>();

        enemy = GameObject.Find("CubeOfDestruction").GetComponent<Enemy>();

        stepsToBattle = Random.Range(150, 301);
        print(stepsToBattle);

    }//end of start()

    // Update is called once per frame
    void Update()
    {     
        /*if(pc.stepCount>= stepsToBattle)
        {
            //Code to start a random battle goes here
            print("Battle has occured");
            //SceneManager.LoadScene("Battle Scene");
            stepsToBattle = Random.Range(150,301);
            currentExp += 30;
            checkForLevelUp();
            pc.stepCount = 0;
        }//end of if stepCount > steps to battle  */        
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



  
}
