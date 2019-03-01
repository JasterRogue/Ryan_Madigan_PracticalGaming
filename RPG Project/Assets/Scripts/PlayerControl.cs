using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    enum PlayerModes { Wandering, Battle};
    PlayerModes IAmCurrently = PlayerModes.Wandering;

    private float speed= 10.0f;
    private float turningSpeed = 90.0f;
    private Animator animate;
    public int stepCount=0; // Used to count number of steps player has taken for triggering random encounters
    private int stepCountdown;

    // Use this for initialization
    void Start () {
   
        animate = GetComponentInChildren<Animator>();
        if (animate)
            print("Found");
        else
            print("NO Animator");
		
	}

    // Update is called once per frame
    void Update() {

        switch (IAmCurrently)
        {

            case PlayerModes.Wandering:
        Camera.main.transform.position = transform.position + new Vector3(0, 3, -10);

                if (Input.GetKey(KeyCode.W))
                {
                    animate.SetBool("IsRunning", true);
                    moveForward(speed);
                    stepCount++;
                }
                else
                    animate.SetBool("IsRunning", false);

                if (Input.GetKey(KeyCode.A))
                {
                    turnLeft(turningSpeed);
                }

                if (Input.GetKey(KeyCode.D))
                {
                    turnRight(turningSpeed);
                }

                if (stepCount > stepCountdown)
                    Global.manager.StepCountReached();
                

                break;

            case PlayerModes.Battle:

                animate.SetBool("battleIdle", true);

                if (Global.manager.playerAttacking)
                {
                    moveToTarget();
                    animate.SetBool("leftPunch", true);
                    animate.SetBool("battleIdle", false);

                    pointBack();

                    while(transform.position != new Vector3(0,0,10))
                    {
                        moveInBattle();
                        animate.SetBool("isRunning", true);

                    }

                    animate.SetBool("isRunning", false);
                    animate.SetBool("battleIdle", true);
                }

                else
                {
                    animate.SetBool("leftPunch", false);
                    animate.SetBool("battleIdle", true);

                }

                break;

    }//end of switch(iAmCurrently)
    }//end of update()



    internal void StepsToNextBattleIs(int stepsToBattle, bool inBattle)
    {

        if (inBattle)
        {
            IAmCurrently = PlayerModes.Battle;
        }
        else
        {
            IAmCurrently = PlayerModes.Wandering;
            stepCountdown = stepsToBattle;
        }

    }

    /// <summary>
    /// Changes direction and moves the character forward
    /// </summary>
    /// <param name="speed">Speed for movement</param>
    private void moveForward(float speed)
    {
        transform.position += speed * transform.forward * Time.deltaTime;
    }

    /// <summary>
    /// Activates character animation for opening chest
    /// </summary>
    public void openChest()
    {
        //animate.SetBool("isOpeningChest", true);

    }

    /// <summary>
    /// Activates character animation for opening door
    /// </summary>
    public void openDoor()
    {
        
    }

    /// <summary>
    /// Rotates the character left
    /// </summary>
    private void turnLeft(float turningSpeed)
    {
        transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the character right
    /// </summary>
    private void turnRight(float turningSpeed)
    {
        transform.Rotate(Vector3.down, turningSpeed * Time.deltaTime);
    }

    public void moveToTarget()
    {
        while(transform.position != new Vector3(0,0,10))
        {
            moveInBattle();
            animate.SetBool("battleIdle", false);
            animate.SetBool("isRunning", true);
        }

        animate.SetBool("isRunning", false);

    }//end of move to target

    public void moveInBattle()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public void pointForward()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    public void pointBack()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }
}
