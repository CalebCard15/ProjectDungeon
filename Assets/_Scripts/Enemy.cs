using UnityEngine;
using System.Collections;

public class Enemy : InteractableObject {

	public int health = 10;
	public int power = 5;
	public int defense = 0;
	public float attackSpeed = .5f;

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

	public override void Interact (Player player)
	{
		spriteRenderer.flipX = player.isFlipped();
		
		player.anim.SetTrigger("Attack");
		anim.SetTrigger("Attack");

		if(player.attackSpeed >= attackSpeed)
		{
			TakeDamage(player.power, player);
			player.TakeDamage(power);

			print("Player Health: " + player.health + "\t Enemy Health: " + health);
		}
		else
		{
			player.TakeDamage(power);
			TakeDamage(player.power, player);

			print("Player Health: " + player.health + "\t Enemy Health: " + health);
		}
	}

	void TakeDamage(int damage, Player player)
	{
		health -= damage - defense;
		if(health <= 0)
		{
			ResetTile(player);
			Destroy(gameObject);
		}
	}
}
