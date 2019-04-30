using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshScript : MonoBehaviour {

     TextMesh myTextMesh;
     Color textAlpha;


	// Use this for initialization
	void Start ()
    {
        myTextMesh = FindObjectOfType<TextMesh>();

        textAlpha = myTextMesh.color;

        textAlpha.a = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    public  void showText(int value, Vector3 textPosition)
    {
        textAlpha.a = 255.0f;

        transform.position = textPosition;
        myTextMesh.text = value.ToString();

        raiseTextAndFade();
    }

    public  void raiseTextAndFade()
    {
        int count = 0;

        while(count < 100)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y+1,transform.position.z);

            textAlpha.a--;

            count++;
            
        }
    }




}
