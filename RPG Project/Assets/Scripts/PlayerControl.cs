using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    internal enum PlayerModes { Wandering, Battle_Idle, MoveToAttack, Melee_Attack, Move_Back, Range_Attack, Escape  };
    internal PlayerModes IAmCurrently = PlayerModes.Wandering;

    private float speed= 10.0f;
    private float turningSpeed = 100.0f;
    private Animator animate;
    public int stepCount=0; // Used to count number of steps player has taken for triggering random encounters
    private int stepCountdown;
    Enemy enemy;
    Vector3 playerPositionBeforeAttack;
    Player myPlayer;
    float waitTimer=0.0f;
    AnimatorClipInfo[] info;
	PauseScript myPauseScript;
    private Scene scene;
    TextMeshScript myTextMesh;
    int escapeChance;

    // Use this for initialization
    void Start () {
   
        animate = GetComponentInChildren<Animator>();
        enemy = FindObjectOfType<Enemy>();
        myPlayer = FindObjectOfType<Player>();
		
        scene = SceneManager.GetActiveScene();

        if(scene.name.Equals("Test Scene"))
        {
            myPauseScript = FindObjectOfType<PauseScript>();
        }

        else
        {
            myTextMesh = FindObjectOfType<TextMeshScript>();
        }
    
	}

    // Update is called once per frame
    void Update() {

        switch (IAmCurrently)
        {

            case PlayerModes.Wandering:

            Camera.main.transform.position = transform.position + new Vector3(0, 5, -5);

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

            case PlayerModes.Battle_Idle:

                animate.SetBool("battleIdle", true);
                playerPositionBeforeAttack = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                waitTimer = 0f;
                

                break;

            case PlayerModes.MoveToAttack:
                pointForward();
                moveInBattle();
                animate.SetBool("IsRunning", true);
                if (hasReachedTarget())
                {
                    IAmCurrently = PlayerModes.Melee_Attack;
                    animate.SetBool("leftPunch", true);
                    animate.SetBool("battleIdle", false);
                    //Global.textMesh.
                    myPlayer.DamageCall();
                    //  myTextMesh.showText(myPlayer.damage, new Vector3(enemy.transform.position.x, enemy.transform.position.y + 10, enemy.transform.position.z));
                    myTextMesh.createText(new Vector3(enemy.transform.position.x, enemy.transform.position.y + 2, enemy.transform.position.z), myPlayer.damage);

                }

                break;
            case PlayerModes.Melee_Attack:

                 info = animate.GetCurrentAnimatorClipInfo(0);

                if (info[0].clip.name == "hk_rh_right_A") //animation finished return to position
                {
            
                    waitTimer += Time.deltaTime;

                    if (hasReturnedToIdlePosition() && waitTimer >= 0.5f)
                    {
                        transform.position = new Vector3(0, 0, -4.57f);
                        IAmCurrently = PlayerModes.Battle_Idle;
                        animate.SetBool("IsRunning", false);
                        pointForward();
                        myPlayer.TurnFinished();
                        
                    }
                }

                break;

            case PlayerModes.Escape:

                if(myPlayer.getAgility() > enemy.getAgility())
                {
                    //can escape
                    pointBack();
                    animate.SetBool("IsRunning", true);
                    //animate.SetBool("battleIdle", false);
                  //  Global.manager.playerEscaped();
                    StartCoroutine(Global.manager.playerEscaped());
                    print("Better agility escape");
                }

                else
                {
                    //else down to chance
                    escapeChance = UnityEngine.Random.Range(0, 101);

                    if(escapeChance <= 25)
                    {
                        //escape success
                        pointBack();
                        animate.SetBool("IsRunning", true);
                       // animate.SetBool("battleIdle", false);
                        StartCoroutine(Global.manager.playerEscaped());
                        print("Lucky escape");
                        
                    }

                    else
                    {
                        //escape failed
                        myPlayer.TurnFinished();
                        print("escape failed");
                    }
                }

                break;

        }//end of switch(iAmCurrently)

    }//end of update()

    public bool hasReturnedToIdlePosition()
    {
        float distance = distanceToOriginalPosition();

        if(distance <=30)
        {
            return true;
        }

        return false;
    }

    private bool hasReachedTarget()
    {
        float distance = getDistanceToEnemy();

        if(distance <= 2)
        {
            return true;
        }

        return false;
    }

    internal void StepsToNextBattleIs(int stepsToBattle, bool inBattle)
    {

        if (inBattle)
        {
            IAmCurrently = PlayerModes.Battle_Idle;
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
        transform.Rotate(Vector3.down, turningSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Rotates the character right
    /// </summary>
    private void turnRight(float turningSpeed)
    {
        transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
    }

    public void initiateMeleeAttack()
    {
        IAmCurrently = PlayerModes.MoveToAttack;
        animate.SetBool("battleIdle", false);
        animate.SetBool("IsRunning", true);

    }//end of move to target

    public void initiateMagicAttack()
    {
        IAmCurrently = PlayerModes.Range_Attack;
        animate.SetBool("battleIdle", false);
       // animate.SetBool();
    }

    public void initiateEscapeAttempt()
    {
        IAmCurrently = PlayerModes.Escape;
    }

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

    private float getDistanceToEnemy()
    {
        Vector3 playerToEnemy;
        float distance;

        playerToEnemy = new Vector3(enemy.transform.position.x - transform.position.x, enemy.transform.position.y - transform.position.y, enemy.transform.position.z - transform.position.z);
        distance = Mathf.Sqrt((playerToEnemy.x * playerToEnemy.x) + (playerToEnemy.y * playerToEnemy.y) + (playerToEnemy.z * playerToEnemy.z));

        return distance;

    }

    private float distanceToOriginalPosition()
    {

        Vector3 playerToOriginalPos;
        float distance;

        playerToOriginalPos = new Vector3(playerPositionBeforeAttack.x - transform.position.x, playerPositionBeforeAttack.y - transform.position.y, playerPositionBeforeAttack.z - transform.position.z);
        distance = Mathf.Sqrt((playerToOriginalPos.x * playerToOriginalPos.x) + (playerToOriginalPos.y * playerToOriginalPos.y) + (playerToOriginalPos.z * playerToOriginalPos.z));

        return distance;

    }
    public void playerKnockdown()
    {
        animate.SetBool("isKnockedDown", true);

       // info = animate.GetCurrentAnimatorClipInfo(0);

       // print("knockdown called");

        if (animate.GetCurrentAnimatorStateInfo(0).IsName("knockdown_A"))
        {
            animate.SetBool("isKnockedDown", false);

            print("knock is false");
        }
    }
}
