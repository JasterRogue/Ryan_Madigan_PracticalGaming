using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshScript : MonoBehaviour {

    TextMesh myTextMesh;


	// Use this for initialization
	void Start ()
    {
        myTextMesh = FindObjectOfType<TextMesh>();


		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    public void showText(int value, Vector3 textPosition)
    {
        transform.position = textPosition;
        myTextMesh.text = value.ToString();

        
    }

    public void raiseTextAndFade()
    {
        int count = 0;

        while(count < 5)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);

            
        }
    }




}
