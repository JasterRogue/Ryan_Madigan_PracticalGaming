using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGameText : MonoBehaviour {

    Button startButton;

	// Use this for initialization
	void Start ()
    {
        startButton = GameObject.FindObjectOfType<Button>();
        startButton.onClick.AddListener(beginGame);

		
	}

    void beginGame()
    {
        //SceneManager.LoadScene("Test Scene");
        Initiate.Fade("Test Scene", Color.black, 5);
        //Got initiate and fade classes from asset store

    }
	

}
