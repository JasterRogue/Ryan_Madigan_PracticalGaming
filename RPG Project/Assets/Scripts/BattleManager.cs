using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    /*The purpose of this script is to deal 
     with battle encounters for the game.
     It will generate the requirements to 
     start a battle and keep track of majority of battle 
     related stuff*/

    PlayerControl pc;//access to player control script
    Player p;//access to player script
    int stepsToBattle;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerControl = GameObject.Find("char_ethan");
        pc = playerControl.GetComponent<PlayerControl>();
        p = FindObjectOfType<Player>();

        stepsToBattle = Random.Range(150, 301);
        print(stepsToBattle);

    }//end of start()

    // Update is called once per frame
    void Update()
    {
        p.levelUp();
        if(pc.stepCount>= stepsToBattle)
        {
            //Code to start a random battle goes here
            print("Battle has occured");
            stepsToBattle = Random.Range(150,301);
            pc.stepCount = 0;
            print("Steps reset");
            

        }//end of if stepCount > steps to battle
      
        
    }//end of update()
}
