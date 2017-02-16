using UnityEngine;
using System.Collections;

public class Enemy : InteractableObject {

	private const int BASE_XP_VALUE = 7;

	public int maxHealth = 10;
	public int health = 10;
	public int power = 1;
	public int defense = 0;
	public int level;
	public ulong xp;

	public Animator anim;
	public AudioSource attackAudio;

	private bool isDead;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {

		//Sets the level of the enemy
		if(GameManager.instance.currentLevel == 1)
		{
			level = 1;
		}
		else
		{
			level = GameManager.instance.currentLevel/2;
		}


		xp = GameManager.instance.lookupTable.xpTable[level];	//Sets the xp of the enemy based on the level using the xp lookup table

		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		attackAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Interact()
	{
		spriteRenderer.flipX = Player.instance.isFlipped();

		//Player Attack animation and sound
		Player.instance.anim.SetTrigger("Attack");
		Player.instance.attackAudio.Play();
		TakeDamage(Player.instance.power);

		//Enemy Attack animation and sound
		anim.SetTrigger("Attack");
		attackAudio.Play();
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
		UIManager.instance.UpdateEnemyUI(this);
		if(health <= 0)
		{
			ResetTile();
			Player.instance.gainXP(xp);
			Destroy(gameObject);
		}
	}
}











/* Scrap code

*/