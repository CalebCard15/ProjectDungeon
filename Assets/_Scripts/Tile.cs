using UnityEngine;
using System.Collections;

public class Tile {

	public int count;					//number of times the tile has been selected to be used

	public int x;						//x coordinate of the tile
	public int y;						//y coordinate of the tile

	public bool openNorth;				//north tile is open
	public bool openSouth;				//south tile is open
	public bool openWest;				//west tile is open
	public bool openEast;				//east tile is open

	public TileType type;				//The type of tile that it is after constructing the map

	public bool endTile;				//The tile that the dungeon exit is located in

	public InteractableObject owner;	//The actual object that owns the tile currently

	public bool isEmpty;				//If the tile has no occupant

	public Tile()
	{
		
	}


	public Tile(int i, int j)
	{
		x = i;
		y = j;
	}


}
