using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item {

	// Use this for initialization
    public int damage = 3;
    string weaponName;

    public Weapon()
    {
        damage = 0;
        weaponName = "";
    }

    public Weapon(int damage, string name)
    {
        damage = this.damage;
        name = this.weaponName;
    }


	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void updateWeapon()
    {

    }
}
