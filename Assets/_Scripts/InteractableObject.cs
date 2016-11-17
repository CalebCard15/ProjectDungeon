using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour {

	public Tile currentTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void Interact()
	{
		//Let the childeren decide how they interact :)
	}

	//This resets the tile so the player can walk on it
	//Called when the player picks up the object or destroys the enemy on the tile
	protected void ResetTile()
	{
		Player.instance.currentTile = currentTile;
		currentTile.isEmpty = true;
		currentTile.owner = null;
	}
}
