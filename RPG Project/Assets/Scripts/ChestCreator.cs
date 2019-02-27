using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCreator : MonoBehaviour {

    public Transform chest;

    List<Chest> allChests;

    // Use this for initialization
    void Start () {

        allChests = new List<Chest>();



        allChests.Add (createNewChest(new Vector3(5, 1, 5), new Weapon("sting", 5)));



        Instantiate(chest, new Vector3(1, 1, 3), Quaternion.identity);
        

}

    private Chest createNewChest(Vector3 positionOfChest, Item chestContents)
    {
        Transform newChestGO = (Transform)Instantiate(chest, positionOfChest, Quaternion.identity);
        Chest newChest = newChestGO.gameObject.GetComponent<Chest>();

        newChest.YouContain(chestContents);

        return newChest;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
