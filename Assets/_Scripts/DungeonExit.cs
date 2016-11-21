using UnityEngine;
using System.Collections;

public class DungeonExit : InteractableObject {


	public override void Interact()
	{
		print("exit now");
		ResetTile();
		GameManager.instance.LoadNewLevel();
	}
}
