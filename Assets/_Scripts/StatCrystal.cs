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
	//such as a crystal that increases attack and defense
	public override void Interact (Player player)
	{
		if(power > 0)
		{
			player.power += power;
		}

		if(defense > 0)
		{
			player.defense += defense;
		}

		if(health > 0)
		{
			player.health += health;
		}

		if(attackSpeed > 0)
		{
			player.attackSpeed += attackSpeed;
		}

		//reset the tile when the player picks up the crystal
		ResetTile(player);
		//Remove the crystal when picked up
		Destroy(gameObject);
	}
}
