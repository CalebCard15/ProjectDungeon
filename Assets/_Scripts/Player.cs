using UnityEngine;
using System;
using System.Collections;

public class Player : PersistentSingleton<Player> {


	public Animator anim;

	public int maxHealth = 20;
	public int health;
	public int power = 2;
	public int defense = 0; 
	public float attackSpeed = 1f;
	public bool canMove;
	public AudioSource attackAudio;
	public Tile currentTile;

	private SpriteRenderer spriteRenderer;
	private float walkTimeDelay = .175f;
	private float timeSinceWalk = 0f;



	// Use this for initialization
	void Start () {
		canMove = true;
		health = maxHealth;
		currentTile = DungeonGenerator.dungeon[0,0];
		anim = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		attackAudio = GetComponent<AudioSource>();
	
	}

	// Update is called once per frame
	void Update () {
		if(canMove)
		{
			timeSinceWalk += Time.deltaTime;

			Vector3 movePosition = new Vector3(currentTile.y*DungeonGenerator.tileSize, currentTile.x * DungeonGenerator.tileSize, 0);

			transform.position = Vector3.Lerp(transform.position, movePosition, .75f);

			if(timeSinceWalk >= walkTimeDelay)
			{


				int horizontal;
				int vertical;

				horizontal = (int)Input.GetAxisRaw("Horizontal");
				vertical = (int)Input.GetAxisRaw("Vertical");
				if(horizontal != 0 || vertical != 0)
				{
					if(horizontal != 0)
					{
						spriteRenderer.flipX = horizontal > 0 ? false : true;


						vertical = 0;
					}

					Move(vertical, horizontal);


				}



				timeSinceWalk = 0f;
			}
		}

	
	}


	void Move(int vert, int horiz)
	{
		if(vert == 0)
		{
			if(CanMove(currentTile.x, currentTile.y + horiz))
			{
				if(DungeonGenerator.dungeon[currentTile.x, currentTile.y + horiz].isEmpty)
				{
					anim.SetTrigger("StartWalk");
					currentTile = DungeonGenerator.dungeon[currentTile.x, currentTile.y + horiz];
				}
				else
				{
					DungeonGenerator.dungeon[currentTile.x, currentTile.y + horiz].owner.Interact();
				}
					
			}
		}
		else if(horiz == 0)
		{
			if(CanMove(currentTile.x + vert, currentTile.y))
			{
				if(DungeonGenerator.dungeon[currentTile.x + vert, currentTile.y].isEmpty)
				{
					anim.SetTrigger("StartWalk");
					currentTile = DungeonGenerator.dungeon[currentTile.x + vert, currentTile.y];
				}
					
				else
				{
					DungeonGenerator.dungeon[currentTile.x + vert, currentTile.y].owner.Interact();
				}
					
			}
		}

	}

	bool CanMove(int x, int y)
	{
		if(x < 0 || x > DungeonGenerator.dungeon.GetLength(0) || y < 0 || y > DungeonGenerator.dungeon.GetLength(1))
		{
			return false;
		}
		else if(DungeonGenerator.dungeon[x,y].type == TileType.FourClosed)
		{
			return false;
		}

		return true;
	}

	public bool isFlipped()
	{
		return spriteRenderer.flipX;
	}

	public void TakeDamage(int damage)
	{
		if(damage - defense <= 0)
		{
			//take no damage
			return;
		}
		health -= damage - defense;
		UIManager.instance.UpdateUI();
		if(health <= 0)
		{
			GameManager.instance.PlayerDie();
		}
	}


}
	
