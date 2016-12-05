using UnityEngine;
using System.Collections;

public class BattleManager : PersistentSingleton<BattleManager> {

	public float playerAttackTimer;
	public float enemyAttackTimer;

	// Use this for initialization
	void Start () {
		playerAttackTimer = 0f;
		enemyAttackTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		playerAttackTimer += Time.deltaTime;
		enemyAttackTimer += Time.deltaTime;
	}

	public void Fight(Enemy enemy)
	{
		Player.instance.canMove = false;


		//Keep battling as long as they both are alive
		while(Player.instance.health > 0 && enemy.health > 0)
		{
			Debug.Log(Player.instance.health + "\t\t" + enemy.health);
			Player.instance.anim.SetTrigger("Attack");
			enemy.TakeDamage(Player.instance.power);
			if(enemy.health <= 0)
				break;
			enemy.anim.SetTrigger("Attack");
			Player.instance.TakeDamage(enemy.power);
		}

		Player.instance.canMove = true;



	}


}
