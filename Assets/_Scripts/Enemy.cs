using UnityEngine;
using System.Collections;

public class Enemy : InteractableObject {

	public int health = 10;
	public int power = 5;
	public int defense = 0;

	public Animator anim;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Interact()
	{
		spriteRenderer.flipX = Player.instance.isFlipped();
		
		Player.instance.anim.SetTrigger("Attack");
		anim.SetTrigger("Attack");

		TakeDamage(Player.instance.power);
		Player.instance.TakeDamage(power);

		print("Player Health: " + Player.instance.health + "\t Enemy Health: " + health);
	}

	public void TakeDamage(int damage)
	{
		if(damage - defense <= 0)
		{
			//take no damage
			return;
		}
		health -= damage - defense;
		if(health <= 0)
		{
			ResetTile();
			Destroy(gameObject);
		}
	}
}











/* Scrap code

*/