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

	public override void Interact()
	{
		spriteRenderer.flipX = Player.instance.isFlipped();
		
		Player.instance.anim.SetTrigger("Attack");
		anim.SetTrigger("Attack");

		if(Player.instance.attackSpeed >= attackSpeed)
		{
			TakeDamage(Player.instance.power);
			Player.instance.TakeDamage(power);

			print("Player Health: " + Player.instance.health + "\t Enemy Health: " + health);
		}
		else
		{
			Player.instance.TakeDamage(power);
			TakeDamage(Player.instance.power);

			print("Player Health: " + Player.instance.health + "\t Enemy Health: " + health);
		}
	}

	void TakeDamage(int damage)
	{
		health -= damage - defense;
		if(health <= 0)
		{
			ResetTile();
			Destroy(gameObject);
		}
	}
}
