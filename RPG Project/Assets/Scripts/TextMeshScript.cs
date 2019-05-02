using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshScript : MonoBehaviour {

     public Transform textMesh;
     Transform txtMeshTransform;
     Color textAlpha;
     TextMesh txtMesh;
    float txtTimer;


    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    public void createText(Vector3 textPos, int txtValue)
    {
        Transform txtMeshTransform = (Transform)Instantiate(textMesh);
        txtMesh = txtMeshTransform.GetComponent<TextMesh>();
        txtMesh.text = txtValue.ToString();
        txtMesh.transform.position = textPos;
        
    }

    public void escapeFailed(Vector3 textPos)
    {
        Transform txtMeshTransform = (Transform)Instantiate(textMesh);
        txtMesh = txtMeshTransform.GetComponent<TextMesh>();
        txtMesh.text = "Escape Attempt Failed";
        txtMesh.transform.position = textPos;

    }

}
