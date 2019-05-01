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
		if(txtTimer < 3f && txtMesh)
        {
            txtMesh.transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);

            txtTimer += Time.deltaTime;
        }

        else
        {
            GameObject.Destroy(this);
        }
	}

    public void createText(Vector3 textPos, int txtValue)
    {
        txtTimer = 0.0f;

        Transform txtMeshTransform = (Transform)Instantiate(textMesh);
        txtMesh = txtMeshTransform.GetComponent<TextMesh>();
        txtMesh.text = txtValue.ToString();
        txtMesh.transform.position = textPos;
        
    }

}
