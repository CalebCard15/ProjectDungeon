using UnityEngine;
using System.Collections;

public class BattleManager : PersistentSingleton<BattleManager> {

	public float fightTimer;


	// Use this for initialization
	void Start () {
		fightTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
