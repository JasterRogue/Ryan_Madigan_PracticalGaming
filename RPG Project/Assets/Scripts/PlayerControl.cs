﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed= 15.0f;
    private float turningSpeed = 90.0f;
    private Animator animate;
    // Use this for initialization
    void Start () {

        animate = GetComponentInChildren<Animator>();
        if (animate) print("Found"); else print("NO Animator");
		
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = transform.position + new Vector3(0, 3, -10);

        if(Input.GetKey(KeyCode.W))
        {
            animate.SetBool("IsRunning", true);
            moveForward(speed);
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
}