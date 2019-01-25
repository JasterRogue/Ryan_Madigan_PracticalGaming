using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float speed= 5.0f;
    private float turningSpeed = 5.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveForward(speed);
        turnRight(turningSpeed);
      // moveLeft(speed);
       // moveRight(speed*-1);
        //moveBack(speed);
        Camera.main.transform.position = transform.position + new Vector3(0, 3, -10);
	}

    /// <summary>
    /// Changes direction and moves the character forward
    /// </summary>
    /// <param name="speed">Speed for movement</param>
    private void moveForward(float speed)
    {
        //transform.position += speed * Vector3.forward * Time.deltaTime;
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
