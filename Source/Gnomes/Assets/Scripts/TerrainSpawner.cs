using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainSpawner : MonoBehaviour
{
				
	private GameObject terrainObject;
	private int deltaWidth;
	private int deltaHeight;

	//Terrain properties
	public float addHeightChance;	//Chance that a patch is raised
	public float risingFactor; 		//intensity of heightincrease
	public int amountOfTerrainsToSpawn; //The amount of terrains to spawn
	public GameObject BaseTerrain;

	//Water properties
	public GameObject water;
	public float waterHeight;

	//Mushroomproperties
	public GameObject Mushroom;		//Mushroom prefab
	public float mushroomYOffset;	//Y offset for the mushroom spawning
	public int amountOfMushrooms;	//amount of mushrooms to try and spawn

	//Properties of the patches (patchlength * amount of patches should be equal to the heigtmapdetail (65 in this case))
	private int lengthOfPatch = 13;
	private int widthOfPatch = 13;
	private int amountOfPatches2D = 5;

	// Use this for initialization
	void Start ()
	{
		generateTerrainSequence (amountOfTerrainsToSpawn);
	}

	void generateTerrainSequence (int length)
	{
		//Position of first Terrain piece placement
		Vector3 TerrainSpawnPosition = new Vector3 (0.0f, 0.0f, 0.0f);

		//Generate and place all terrains pieces in a row
		for (int i=0; i<length; i++) {
			TerrainSpawnPosition = new Vector3 (i * 25.0f, 0.0f, 0.0f);
			createTerrain (TerrainSpawnPosition, i);
		}

	}

	void createTerrain (Vector3 TerrainSpawnPosition, int index)
	{
		//Make a new GameObject for the terrain
		GameObject CopyTerrain = new GameObject ();
		string terrainName = "TerrainN0" + index.ToString ();
		CopyTerrain.name = terrainName;

		//Locate the GameObject to the right place
		CopyTerrain.transform.position = TerrainSpawnPosition;

		//Add the terraincomponent to the GameObject
		CopyTerrain.AddComponent<Terrain> ();

		//Copy the data from the prefab for the new terrain
		TerrainData BaseTerrainData = BaseTerrain.GetComponent<Terrain> ().terrainData;
		TerrainData CopyTerrainData = (TerrainData)Object.Instantiate (BaseTerrainData);
		Terrain terrain = CopyTerrain.GetComponent<Terrain> ();
		terrain.terrainData = CopyTerrainData;

		//Allocating space for a new heightmap for the terrain
		int heightmapWidth = terrain.terrainData.heightmapWidth;
		int heightmapHeight = terrain.terrainData.heightmapHeight;

		//Make heightmap all zero for flat terrain
		//float[,] heightmap = returnEmptyHeightmap(heightmapWidth,heightmapHeight);

		//Calculate a random heightmap
		//float[,] heightmap = calculateNewRandomHeightmap (heightmapWidth, heightmapHeight);

		//Calculate new 'random' Heightmap
		float[,] heightmap = calculateNewHeightmap (heightmapWidth, heightmapHeight);

		//Add heightmap to the terrainData.
		terrain.terrainData.SetHeights (0, 0, heightmap);

		//Add mushrooms to the terrainData
		addMushrooms(terrain,TerrainSpawnPosition,amountOfMushrooms);

		//Add Water to the terrain
		GameObject.Instantiate(water,TerrainSpawnPosition + new Vector3(12,waterHeight,12),Quaternion.Euler(0, 0, 0));

		//Add a terrainCollider to the GameObject
		CopyTerrain.AddComponent<TerrainCollider> ().terrainData = terrain.terrainData;
	}

	void addMushrooms(Terrain terrain, Vector3 TerrainSpawnPosition, int amount)
	{
		TerrainData terrainData = terrain.terrainData;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;

		float deltaLength = terrainLength/amountOfPatches2D;
		float deltaWidth = terrainWidth/amountOfPatches2D;

		int patchN0;
		//Patch is identified with x and y value
		List<int> patchN0ListX = new List<int>();
		List<int> patchN0ListZ = new List<int>();

		//Generate random list of patchnumbers for x-axis where mushrooms have to be placed. List has a max length of 10 and min length of 1.
		for(int i=0;i<amount;i++)
		{
			patchN0 = Random.Range(0,amountOfPatches2D);
			if(!patchN0ListX.Contains(patchN0))
			{
				//Only add a patch to the list if the list not already contains it.
				patchN0ListX.Add(patchN0);
			}
		}

		//Generate random list of patchnumbers for z-axis where mushrooms have to be placed. List has the same length as patches data for X
		for(int i=0;i<patchN0ListX.Count;i++)
		{
			patchN0 = Random.Range(0,amountOfPatches2D);
			patchN0ListZ.Add(patchN0);
		}

		//place mushrooms on the patches
		for(int i=0;i<patchN0ListX.Count;i++)
		{
			float[] XZCoord = patchLocation(patchN0ListX[i], patchN0ListZ[i], terrain, TerrainSpawnPosition);

			float xCoord = XZCoord[0];
			float zCoord = XZCoord[1];


			float yCoord = terrain.SampleHeight(new Vector3(xCoord,0.0f,zCoord));

			//Spawn position for the mushroom:
			Vector3 mushroomSpawnPosition = new Vector3(xCoord,yCoord+mushroomYOffset,zCoord);

			//Spawn the mushroom
			GameObject.Instantiate(Mushroom,mushroomSpawnPosition,Quaternion.Euler(0, 0, 0));
		}




	}

	float[,] calculateNewHeightmap (int heightmapWidth, int heightmapHeight)
	{
		float[,] newHeightmap = new float[heightmapWidth, heightmapHeight];

		//Devide the heightmap into 5*5 different patches forming a grid. The heightvalues of the end will not be adjusted so all terrainpieces will match 
		for (int h=0; h<(amountOfPatches2D); h++) {
			for (int g=1; g<(amountOfPatches2D)-1; g++) {
				float RandomHeigthValue = Random.Range (0.1f, 1.0f)*risingFactor;
				//if true uphigh the patch with RandomHeightValuet
				if (Random.Range (0.0f, 1.0f) > (1 - addHeightChance)) {
					for (int i=widthOfPatch*h; i<widthOfPatch*(h+1); i++) {
						for (int j=lengthOfPatch*g; j<lengthOfPatch*(g+1); j++) {
							newHeightmap [i, j] = RandomHeigthValue;
						}
					}
				}
			}
		}

		return newHeightmap;
	}

	float[,] calculateNewRandomHeightmap (int heightmapWidth, int heightmapHeight)
	{
		float[,] newHeightmap = new float[heightmapWidth, heightmapHeight];
		for (int i=0; i<heightmapWidth; i=i+deltaWidth) {
			for (int j=0; j<heightmapHeight; j=j+deltaHeight) {
				//Change height on terrain on random places with a random height
				if (j <= heightmapWidth && i <= heightmapHeight && Random.Range (0.0f, 1.0f) > (1 - addHeightChance))
					newHeightmap [i, j] = Random.Range (0.0f, 0.1f);
				//heightmap [i, j] = 0.0f;
			}
		}
		return newHeightmap;
	}

	float[,] returnEmptyHeightmap (int heightmapWidth, int heightmapHeight)
	{
		float[,] newHeightmap = new float[heightmapWidth, heightmapHeight];
		for (int i=0; i<heightmapWidth; i=i+deltaWidth) {
			for (int j=0; j<heightmapHeight; j=j+deltaHeight) {
				//Change height on terrain on random places with a random height
				if (j <= heightmapWidth && i <= heightmapHeight && Random.Range (0.0f, 1.0f) > (1 - addHeightChance))
					newHeightmap [i, j] = 0.0f;
			}
		}
		return newHeightmap;
	}

	float[] patchLocation(int i, int j, Terrain terrain, Vector3 TerrainSpawnPosition) //returns realworld x,z location of patch i,j
	{
		TerrainData terrainData = terrain.terrainData;
		float heightmapHeigt = terrainData.heightmapHeight;
		float heightmapWidth = terrainData.heightmapWidth;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;
		
		float deltaLength = terrainLength/amountOfPatches2D;
		float deltaWidth = terrainWidth/amountOfPatches2D;
		
		float[] returnCoord = new float[2];

		//First term is the relative position of the patch, middle term is used to update the spawnpoint of a mushroom relative to the terrain position. The last term is used to spawn the mushroom in the middle of the patch
		returnCoord[0] = deltaLength*i + TerrainSpawnPosition.x + (lengthOfPatch/2)/heightmapHeigt*terrainLength;
		returnCoord[1] = deltaWidth*j + TerrainSpawnPosition.z + (widthOfPatch/2)/heightmapWidth*terrainWidth;
		
		return returnCoord;
	}

	void removeGrassOfPatch(Terrain terrain, int i, int j) //remove grass of patch i,j
	{
		//hier verder
	}

	void removeGrass(Terrain terrain, int x, int z)
	{
		int[,] map = terrain.terrainData.GetDetailLayer(0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);	

		//remove grass at x,z position
		map[x,z] = 0;

		terrain.terrainData.SetDetailLayer(0, 0, 0, map);
	}



	
}
