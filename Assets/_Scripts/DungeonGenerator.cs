using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public enum TileType{FourOpen, OpenNorth, OpenSouth, OpenEast, OpenWest, ClosedNorth, ClosedSouth, ClosedWest, ClosedEast, OpenNE, OpenSE, OpenNW, OpenSW, OpenNS, OpenWE, FourClosed};

public class DungeonGenerator : MonoBehaviour {

	public GameObject exit;					//The prefab of the dungeon exit tile
	public GameObject[] enemies;			//Array of possible enemies to be place in a tile
	public GameObject[] crystals;			//Array of possible stat crystals to be placed in a tile

	public int rooms;						//number of rooms in the dungeon
	public int sizeX;						//Size of the dungeon in the vertical direction
	public int sizeY;						//Size of the dungeon in the horizontal direction

	public TileType[, , ,] lookupType;		//TileType lookup table to determine what type each tile is in the dungeon

	public static Tile[,] dungeon;			//The array for the dungeon made of the Tiles
	public List<Tile> emptyTiles;			//This is a list of all tiles that are open for placing objects

	public string seed = "";				//The seed for the random number generator
	public bool useRandomSeed;				//If the bool is true the seed will be randomly generated and the serialized seed will not be used

	public GameObject[] openTiles;			//Array of prefabs for all tiles that are completely open
	public GameObject[]	oneWallTiles;		//Array of prefabs for all tiles that have only one wall
	public GameObject[]	twoWallTiles;		//Array of prefabs for all tiles that have two walls
	public GameObject[] hShapedWallTiles;	//Array of prefabs for all tiles that have a H shape with two walls
	public GameObject[] threeWallTiles;		//Array of prefabs for all tiles that have three walls

	public int numberOfCrystals = 2;		//The number of crystals that should be spawned in the dungeon
	public int numberOfEnemies = 5;			//The number of crystals that should be spawned in the dungeon
		
	private int currentRoomNum = 0;			//The amount of rooms currently in the dungeon
	private System.Random randomGenerator;	//The random generator used for random situations

	public static readonly float tileSize = 4.17f;


	// Use this for initialization
	void Awake () { 
		emptyTiles = new List<Tile>();

		if(useRandomSeed)
		{
			seed = System.DateTime.Now.ToString();
		}

		randomGenerator = new System.Random(seed.GetHashCode());

		InitializeLookupTable();

		InitializeDungeon();

		SetDungeonPoints();

		SetDungeonLayout();

		InstantiateDungeon();
	
	}

	void Start()
	{
		SpawnObjects();
	}

	//Initializes the look up table for the types of tiles depending on the boolean values for open spaces in each cardinal direction
	//array is setup as follows
	//[NORTH,SOUTH,EAST,WEST]
	//1 if the spot is open and 0 if the spot is closed
	void InitializeLookupTable()
	{
		lookupType = new TileType[2,2,2,2];
		lookupType[0,0,0,0] = TileType.FourClosed;
		lookupType[1,1,1,1] = TileType.FourOpen;
		lookupType[1,1,1,0] = TileType.ClosedWest;
		lookupType[1,1,0,0] = TileType.OpenNS;
		lookupType[1,0,0,0] = TileType.OpenNorth;
		lookupType[0,1,0,0] = TileType.OpenSouth;
		lookupType[0,0,1,0] = TileType.OpenEast;
		lookupType[0,0,0,1] = TileType.OpenWest;
		lookupType[0,1,1,0] = TileType.OpenSE;
		lookupType[0,0,1,1] = TileType.OpenWE;
		lookupType[0,1,1,1] = TileType.ClosedNorth;
		lookupType[1,0,1,1] = TileType.ClosedSouth;
		lookupType[1,1,0,1] = TileType.ClosedEast;
		lookupType[1,0,1,0] = TileType.OpenNE;
		lookupType[0,1,0,1] = TileType.OpenSW;
		lookupType[1,0,0,1] = TileType.OpenNW;

	}

	//makes all of the dungeon tiles walls and makes a one tile layer all around be boundary type tiles
	void InitializeDungeon()
	{
		dungeon = new Tile[sizeX, sizeY];
		for(int i = 0; i < sizeX; i++)
		{
			for(int j = 0; j < sizeY; j++)
			{
				dungeon[i,j] = new Tile(i,j);
			}
		}
	}

	//Runs through the board and places a mark on all of the tiles that will be used in the dungeon
	void SetDungeonPoints()
	{
		int currX = 0;
		int currY = 0;

		//Set the inital origin point to be used
		dungeon[currX, currY].count++;



		while(currentRoomNum < rooms)
		{	
			//Get a number from 0 to 3, 0 north, 1 west, 2 south, 3 east
			int direction = randomGenerator.Next(0, 4);


			switch(direction)
			{
				//GO NORTH!
				case 0:
				{
					//if it stays inside of the boundaries
					if(currX+1 <= sizeX)
					{
						currX++;
						dungeon[currX, currY].count++;
						currentRoomNum++;
					}
					break;
				}
				//GO WEST!
				case 1:
				{
					//if it stays inside of the boundaries
					if(currY-1 > -1)
					{
						currY--;
						dungeon[currX, currY].count++;
						currentRoomNum++;
					}
					break;
				}
				//GO SOUTH!
				case 2:
				{

					if(currX-1 > -1)
					{
						currX--;
						dungeon[currX, currY].count++;
						currentRoomNum++;
					}
					break;
				}
				//GO EAST!
				case 3:
				{

					//if it stays inside of the boundaries
					if(currY+1 <= sizeY)
					{
						currY++;
						dungeon[currX, currY].count++;
						currentRoomNum++;
					}
					break;
				}
			}
		}
	}

	//Sets up the dungeon to tell every tile which side is open and which side is closed
	void SetDungeonLayout()
	{
		//This is the pointer to the end tile that will be the exit tile
		Tile lastTile = new Tile();


		for(int i = 0; i < sizeX; i++)
		{
			for(int j = 0; j < sizeY; j++)
			{
				//Checks for the south level boundary or if the block to the south has been counted
				if(i-1 < 0 || dungeon[i-1,j].count < 1)
					dungeon[i,j].openSouth = false;
				else
					dungeon[i,j].openSouth = true;
				
				//Checks for the north level boundary or if the block to the north has been counted
				if(i+1 >= sizeX || dungeon[i+1,j].count < 1)
					dungeon[i,j].openNorth = false;
				else
					dungeon[i,j].openNorth = true;
				
				//Checks for the west level boundary or if the block to the west has been counted
				if(j-1 < 0 || dungeon[i,j-1].count < 1)
					dungeon[i,j].openWest = false;
				else
					dungeon[i,j].openWest = true;

				//Checks for the east level boundary or if the block to the east has been counted
				if(j+1 >= sizeY || dungeon[i,j+1].count < 1)
					dungeon[i,j].openEast = false;
				else 
					dungeon[i,j].openEast = true;


				//Uses the look up tables to set the tile type of the current dungeon tile (lookup table created in the InitiateLookupTable method at the top)
				dungeon[i,j].type = lookupType[Convert.ToInt32(dungeon[i,j].openNorth), Convert.ToInt32(dungeon[i,j].openSouth), Convert.ToInt32(dungeon[i,j].openEast), Convert.ToInt32(dungeon[i,j].openWest)];

				//if the tile has not been picked to be a part of the dungeon then say it is closed
				if(dungeon[i,j].count == 0)
				{
					dungeon[i,j].type = TileType.FourClosed;
					dungeon[i,j].isEmpty = false;
				}
					

				//Keep setting the last tile and at the end the last tile will be in this variable
				if(dungeon[i,j].count > 0)
				{
					lastTile = dungeon[i,j];
				}


			}
		}

		//After the loop has looped, set the final endTile to be the one true endTile
		lastTile.endTile = true;
		SpawnExit(lastTile);
	}



	//Create the dungeon using its current tile layout and corresponding tile types
	//Randomly instantiate from a list of tiles which tile need to be placed
	void InstantiateDungeon()
	{
		for(int i = 0; i < sizeX; i++)
		{
			for(int j = 0; j < sizeY; j++)
			{
				GameObject inst = null;
				switch(dungeon[i,j].type)
				{
					//Case when the tile has four open walls
					case TileType.FourOpen:
					{
						int randTile = UnityEngine.Random.Range(0, openTiles.Length);
						inst = (GameObject)Instantiate(openTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), Quaternion.identity);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenNorth:
					{
						int randTile = UnityEngine.Random.Range(0, threeWallTiles.Length);
						inst = (GameObject)Instantiate(threeWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), threeWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 180);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenSouth:
					{
						int randTile = UnityEngine.Random.Range(0, threeWallTiles.Length);
						inst = (GameObject)Instantiate(threeWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), threeWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenEast:
					{
						int randTile = UnityEngine.Random.Range(0, threeWallTiles.Length);
						inst = (GameObject)Instantiate(threeWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), threeWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenWest:
					{
						int randTile = UnityEngine.Random.Range(0, threeWallTiles.Length);
						inst = (GameObject)Instantiate(threeWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), threeWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z - 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.ClosedNorth:
					{
						int randTile = UnityEngine.Random.Range(0, oneWallTiles.Length);
						inst = (GameObject)Instantiate(oneWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), oneWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z);
						inst.name = i + "," + j;
						break;
					}
					case TileType.ClosedSouth:
					{
						int randTile = UnityEngine.Random.Range(0, oneWallTiles.Length);
						inst = (GameObject)Instantiate(oneWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), oneWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 180);
						inst.name = i + "," + j;
						break;
					}
					case TileType.ClosedWest:
					{
						int randTile = UnityEngine.Random.Range(0, oneWallTiles.Length);
						inst = (GameObject)Instantiate(oneWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), oneWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.ClosedEast:
					{
						int randTile = UnityEngine.Random.Range(0, oneWallTiles.Length);
						inst = (GameObject)Instantiate(oneWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), oneWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z - 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenNE:
					{
						int randTile = UnityEngine.Random.Range(0, twoWallTiles.Length);
						inst = (GameObject)Instantiate(twoWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), twoWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenSE:
					{
						int randTile = UnityEngine.Random.Range(0, twoWallTiles.Length);
						inst = (GameObject)Instantiate(twoWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), twoWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenNW:
					{
						int randTile = UnityEngine.Random.Range(0, twoWallTiles.Length);
						inst = (GameObject)Instantiate(twoWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), twoWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 180);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenSW:
					{
						int randTile = UnityEngine.Random.Range(0, twoWallTiles.Length);
						inst = (GameObject)Instantiate(twoWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), twoWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z - 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenNS:
					{

						int randTile = UnityEngine.Random.Range(0, openTiles.Length);
						inst = (GameObject)Instantiate(hShapedWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), hShapedWallTiles[randTile].transform.rotation);
						inst.name = i + "," + j;
						break;
					}
					case TileType.OpenWE:
					{

						int randTile = UnityEngine.Random.Range(0, openTiles.Length);
						inst = (GameObject)Instantiate(hShapedWallTiles[randTile], new Vector3(j*tileSize, i*tileSize, 0), hShapedWallTiles[randTile].transform.rotation);
						inst.transform.eulerAngles = new Vector3(0, 0, inst.transform.rotation.eulerAngles.z + 90);
						inst.name = i + "," + j;
						break;
					}
					case TileType.FourClosed:
					{
						break;
					}


					
				}

				if(dungeon[i,j].type != TileType.FourClosed && !dungeon[i,j].endTile)
				{
					emptyTiles.Add(dungeon[i,j]);
					dungeon[i,j].isEmpty = true;
				}
					
			}

		}
	}

	public void SpawnObjects()
	{
		//Remove the origin where the player starts
		emptyTiles.Remove(dungeon[0,0]);
		Player.instance.currentTile = dungeon[0,0];
		
		//Spawn Crystals first
		for(int i = 0; i < numberOfCrystals; i++)
		{
			SpawnCrystal(emptyTiles[randomGenerator.Next(0, emptyTiles.Count)]);
		}

		for(int i = 0; i < numberOfEnemies; i++)
		{
			SpawnEnemy(emptyTiles[randomGenerator.Next(0, emptyTiles.Count)]);
		}


	}

	void SpawnEnemy(Tile tile)
	{
		int randomNumber = randomGenerator.Next(0, enemies.Length);
		GameObject inst = (GameObject)Instantiate(enemies[randomNumber], new Vector3(tile.y*tileSize, tile.x*tileSize, 0), enemies[randomNumber].transform.rotation);
		tile.owner = inst.GetComponent<InteractableObject>();
		tile.owner.currentTile = tile;
		tile.isEmpty = false;
		emptyTiles.Remove(tile);
	}

	void SpawnCrystal(Tile tile)
	{
		int randomNumber = randomGenerator.Next(0, crystals.Length);
		GameObject inst = (GameObject)Instantiate(crystals[randomNumber], new Vector3(tile.y*tileSize, tile.x*tileSize, 0), crystals[randomNumber].transform.rotation);
		tile.owner = inst.GetComponent<InteractableObject>();
		tile.owner.currentTile = tile;
		tile.isEmpty = false;
		emptyTiles.Remove(tile);
	}

	void SpawnExit(Tile tile)
	{
		GameObject inst = (GameObject)Instantiate(exit, new Vector3(tile.y*tileSize, tile.x*tileSize, 0), exit.transform.rotation);
		tile.owner = inst.GetComponent<InteractableObject>();
		tile.owner.currentTile = tile;
		tile.isEmpty = false;
	}




}
	