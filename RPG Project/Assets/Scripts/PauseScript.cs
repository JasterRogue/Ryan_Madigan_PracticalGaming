using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour {

	public Text currentHPValue;
	public Text maxHPValue;
	public Text currentMPValue;
	public Text maxMPValue;
	public Text strengthValue;
	public Text defenceValue;
	public Text intelligenceValue;
	public Text specialDefenceValue;
	public Text agilityValue;
	public Text luckValue;
	public Text levelValue;
	public GameObject pauseMenu;

	public bool paused = false;

	Player myPlayer;

	// Use this for initialization
	void Start ()
	{
		myPlayer = FindObjectOfType<Player> ();
	

		//pauseMenu.SetActive (false);
	}

	// Update is called once per frame
	void Update ()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
			{
				paused = !paused;
				pauseMenu.SetActive (paused);
				setValues ();
				print ("Escape pressed");
			}
	}

	public void setValues()
	{
		currentHPValue.text = myPlayer.getHP ().ToString () + "/" ;
		currentMPValue.text = myPlayer.getMP ().ToString () + "/" ;
		maxHPValue.text = myPlayer.getMaxHP ().ToString ();
		maxMPValue.text = myPlayer.getMaxMP ().ToString ();
		strengthValue.text = myPlayer.getStrength ().ToString ();
		defenceValue.text = myPlayer.getDefence ().ToString ();
		intelligenceValue.text = myPlayer.getDefence ().ToString ();
		specialDefenceValue.text = myPlayer.getSpecialDefence ().ToString ();
		agilityValue.text = myPlayer.getAgility ().ToString ();
		luckValue.text = myPlayer.getLuck ().ToString ();
		levelValue.text = myPlayer.getLevel ().ToString ();
	}
}
