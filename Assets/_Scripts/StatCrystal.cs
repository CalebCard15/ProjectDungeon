using UnityEngine;
using System.Collections;

public class StatCrystal : InteractableObject {

	public int power;
	public int defense;
	public int health;
	public float attackSpeed;

	// Use this for initialization
	void Start () {
		
	}

	//Using this methodology the crystal class can be any mixture of stat increases, 
	//such as a crystal that increases attack and defense, etc...
	//
	//This also lets the designer customize what the crystals are doing directly
	//in the editor.

	public override void Interact ()
	{
		if(power > 0)
		{
			Player.instance.power += power;
		}

		if(defense > 0)
		{
			Player.instance.defense += defense;
		}

		if(health > 0)
		{
			Player.instance.maxHealth += health;
			Player.instance.health += health;
		}

		if(attackSpeed > 0)
		{
			Player.instance.attackSpeed += attackSpeed;
		}
		UIManager.instance.UpdateUI();

		//reset the tile when the player picks up the crystal
		ResetTile();
		//Remove the crystal when picked up
		Destroy(gameObject);
	}
}
