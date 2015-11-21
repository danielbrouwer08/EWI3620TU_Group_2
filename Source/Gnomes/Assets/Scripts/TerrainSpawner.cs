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
	public float grassRemoveHeight; //Height at which grass needs to be removed
	private Vector3 TerrainSpawnPosition = new Vector3 (0.0f, 0.0f, 0.0f);//Position of first Terrain piece placement
	
	//Water properties
	public GameObject water;
	public float waterHeight;

	//Mushroomproperties
	public GameObject Mushroom;		//Mushroom prefab
	public float mushroomYOffset;	//Y offset for the mushroom spawning
	public int amountOfMushrooms;	//amount of mushrooms to try and spawn
	public float mushroomXRotationRange;
	public float mushroomYRotationRange;
	public float mushroomZRotationRange;
	public float scaleIntensity;

	//Vegetationproperties
	public GameObject vegetation;

	//Properties of the patches (patchlength * amount of patches should be equal to the heigtmapdetail (65 in this case))
	private int lengthOfPatch = 13;
	private int widthOfPatch = 13;
	private int amountOfPatches2D = 5;
	private float terrainLength = 50;

	// Use this for initialization
	void Start ()
	{
		//generate #amountOfTerrainsToSpawn terrains
		generateTerrainSequence (amountOfTerrainsToSpawn);
	}

	void generateTerrainSequence (int length)
	{


		//Generate and place all terrains pieces in a row
		for (int i=0; i<length; i++) {
			TerrainSpawnPosition = new Vector3 (i * terrainLength, 0.0f, 0.0f);
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
		float[,] heightmap = calculateNewHeightmap (terrain, heightmapWidth, heightmapHeight);

		//Add heightmap to the terrainData.
		terrain.terrainData.SetHeights (0, 0, heightmap);
	
		//Add mushrooms to the terrainData
		addMushrooms (terrain, amountOfMushrooms);

		//Add Water to the terrain
		GameObject.Instantiate (water, TerrainSpawnPosition + new Vector3 (0, waterHeight * risingFactor, 0), Quaternion.Euler (0, 0, 0));

		//Remove grass
		terrain.terrainData.SetDetailLayer (0, 0, 0, calculateNewDetailmap (terrain, heightmap)); //add new detailmap that has some patches of grass removed


		//Remove grass
		//int[,] newDetailmap = calculateNewDetailmap(terrain);

		//updateNewDetailmap(terrain);


		//Add a terrainCollider to the GameObject
		CopyTerrain.AddComponent<TerrainCollider> ().terrainData = terrain.terrainData;
	}

	void addMushrooms (Terrain terrain, int amount)
	{
		TerrainData terrainData = terrain.terrainData;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;

		float deltaLength = terrainLength / amountOfPatches2D;
		float deltaWidth = terrainWidth / amountOfPatches2D;

		int patchN0;
		//Patch is identified with x and y value
		List<int> patchN0ListX = new List<int> ();
		List<int> patchN0ListZ = new List<int> ();

		//Generate random list of patchnumbers for x-axis where mushrooms have to be placed. List has a max length of 10 and min length of 1.
		for (int i=0; i<amount; i++) {
			patchN0 = Random.Range (1, (amountOfPatches2D-1)); //range from 1 to amountOfPatches2D-1 so the outer edges dont get mushrooms.
			if (patchN0 % 2 != 0) {//only allow even numbers as patches (fixes bug that mushrooms spawn to close together)
				if (patchN0 == (amountOfPatches2D)) {
					patchN0--;
				} else {
					patchN0++;
				}
			}
			if (!patchN0ListX.Contains (patchN0)) {
				//Only add a patch to the list if the list not already contains it.
				patchN0ListX.Add (patchN0);
			}
		}

		//Generate random list of patchnumbers for z-axis where mushrooms have to be placed. List has the same length as patches data for X
		for (int i=0; i<patchN0ListX.Count; i++) {
			patchN0 = Random.Range (0, (amountOfPatches2D));

		

			if (patchN0 % 2 != 0) {//only allow even numbers as patches (fixes bug that mushrooms spawn to close together)
				if (patchN0 == (amountOfPatches2D)) {
					patchN0--;
				} else {
					patchN0++;
				}
			}
			patchN0ListZ.Add (patchN0);
		}

		//place mushrooms on the patches
		for (int i=0; i<patchN0ListX.Count; i++) {
			float[] XZCoord = patchLocation (patchN0ListX [i], patchN0ListZ [i], terrain, TerrainSpawnPosition);

			float xCoord = XZCoord [0];
			float zCoord = XZCoord [1];


			float yCoord = terrain.SampleHeight (new Vector3 (xCoord, 0.0f, zCoord));

			//Spawn position for the mushroom:
			Vector3 mushroomSpawnPosition = new Vector3 (xCoord, yCoord + mushroomYOffset, zCoord);

			//int[] TESTCoord = new int[2];
			//for(int h=1;h<25;h++)
			//{
			//TESTCoord[0] = h;
			//	TESTCoord[1] = h;
			//	removeGrass(terrain, TESTCoord);
			//}

			//Spawn the mushroom avoiding water
			if (terrain.SampleHeight (new Vector3 (xCoord, 0.0f, zCoord)) >= waterHeight) {
				float xRot = Random.Range (-mushroomXRotationRange, mushroomXRotationRange);
				float yRot = Random.Range (-mushroomYRotationRange, mushroomYRotationRange);
				float zRot = Random.Range (-mushroomZRotationRange, mushroomZRotationRange);


				//spawn mushroom with random rotation
				int random = Random.Range(0,2);
				GameObject toSpawn = Mushroom;
				if(random<1)
				{
					toSpawn = vegetation;
				}

				GameObject spawnedMushroom = (GameObject)GameObject.Instantiate (toSpawn, mushroomSpawnPosition, Quaternion.Euler (xRot, yRot, zRot));

				float scaleFactor = scaleIntensity*Random.Range (0.5f,2.0f); //random scaling of each mushroom

				spawnedMushroom.transform.localScale=new Vector3(scaleFactor,scaleFactor,scaleFactor);
			}
		}




	}

	float[,] calculateNewHeightmap (Terrain terrain, int heightmapWidth, int heightmapHeight)
	{
		float[,] newHeightmap = new float[heightmapWidth, heightmapHeight];

		//int[] heigtmapCoord = new int[2];
		//Detail map for grass removal:
		//int[,] DetailMap = terrain.terrainData.GetDetailLayer(0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);	

		//Devide the heightmap into amountOfPatches2D*amountOfPatches2D different patches forming a grid. The heightvalues of the end will not be adjusted so all terrainpieces will match 
		for (int h=0; h<(amountOfPatches2D); h++) {
			for (int g=0; g<(amountOfPatches2D); g++) { 
				//Generate a random heightvalue
				float RandomHeigthValue = Random.Range (0.5f, 1.0f) * risingFactor;


				if (g == 0 || g == (amountOfPatches2D) - 1) { //outer lines will get same heigth so terrainpieces will match in heigth
					for (int i=widthOfPatch*h; i<widthOfPatch*(h+1); i++) {
						for (int j=lengthOfPatch*g; j<lengthOfPatch*(g+1); j++) {
							newHeightmap [i, j] = 0.25f * risingFactor;
						}
					}
				} else if (Random.Range (0.0f, 1.0f) > (1 - addHeightChance)) { //if true uphigh the patch with RandomHeightValue
					for (int i=widthOfPatch*h; i<widthOfPatch*(h+1); i++) {
						for (int j=lengthOfPatch*g; j<lengthOfPatch*(g+1); j++) {
							//heigtmapCoord[0] = i; //store coordinate for grass removal
							//heigtmapCoord[1] = j; //store coordinate for grass removal
							newHeightmap [i, j] = RandomHeigthValue;
							//if((RandomHeigthValue>grassRemoveHeight*risingFactor) || (RandomHeigthValue<waterHeight*risingFactor)) //remove grass at terrain higher than the grassRemoveHeight and at terrain lower than water
							//{
							//	int[] detailmapCoord = heightmapCoordToDetailmapCoord(heigtmapCoord,terrain.terrainData); //convert heightmap coordinate to detailmapcoordinate
							//
							//	DetailMap[detailmapCoord[0],detailmapCoord[1]] = 0; //remove grass from detailmap
							//}

						}
					}
				}


			}
		}
		//terrain.terrainData.SetDetailLayer(0, 0, 0, DetailMap); //add new detailmap that has some patches of grass removed
		return newHeightmap;
	}

	int[,] calculateNewDetailmap (Terrain terrain, float[,] heightmap)
	{
		//Detail map for grass removal:
		int[,] DetailMap = terrain.terrainData.GetDetailLayer (0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);	

		int[] heigtmapCoord = new int[2];

		float[] worldCoord = new float[2];
		int[] detailmapCoord = new int[2];
		//int[] detailmapCoord = heightmapCoordToDetailmapCoord(heigtmapCoord,terrain.terrainData); //convert heightmap coordinate to detailmapcoordinate
	
		//loop througH the heightmap
		for (int i=0; i<heightmap.GetLength(0); i++) {
			for (int j=0; j<heightmap.GetLength(1); j++) {
				heigtmapCoord [0] = i; //store coordinate for grass removal
				heigtmapCoord [1] = j; //store coordinate for grass removal
				detailmapCoord = heightmapCoordToDetailmapCoord (heigtmapCoord, terrain.terrainData);

				if (heightmap [i, j] > grassRemoveHeight * risingFactor) {
					DetailMap [detailmapCoord [0], detailmapCoord [1]] = 0; //remove grass from detailmap
				}

				if (heightmap [i, j] < waterHeight * risingFactor) {
					DetailMap [detailmapCoord [0], detailmapCoord [1]] = 0; //remove grass from detailmap
				}

			}

		}

		
		return DetailMap;

		//loop throug whole map
//		for (int h=0; h<terrain.terrainData.detailWidth; h++) {
//			for (int g=0; g<terrain.terrainData.detailHeight; g++){
//				detailmapCoord[0] = h;
//				detailmapCoord[1] = g;
//				worldCoord = detailmapCoordToWorldCoord(detailmapCoord, terrain.terrainData); //get world coordinate from detail coordinate
//
//				//testlijst.Add(worldCoord[0]);
//				//testlijst.Add(worldCoord[1]);
//
//				mapHeight = Mathf.Abs(terrain.SampleHeight(new Vector3(worldCoord[0],0.0f,worldCoord[1]))); //mapheigt at detail location
//
//				//mapHeight = terrain.SampleHeight(new Vector3(detailmapCoord[0],0.0f,detailmapCoord[1]));
//				if(mapHeight>grassRemoveHeight*risingFactor)
//				{
//					DetailMap[detailmapCoord[0],detailmapCoord[1]] = 0; //remove grass from detailma
//				}
//			}
//		}



		//loop through all patches
//		for (int h=0; h<(amountOfPatches2D); h++) {
//			for (int g=1; g<(amountOfPatches2D)-1; g++){
//				for (int i=widthOfPatch*h; i<widthOfPatch*(h+1); i++) {
//					for (int j=lengthOfPatch*g; j<lengthOfPatch*(g+1); j++) {
//						if((terrain.terrainData.GetHeight(i,j)>grassRemoveHeight*risingFactor)) //remove grass at terrain higher than the grassRemoveHeight and at terrain lower than water
//						{
//							heigtmapCoord[0] = i; //store coordinate for grass removal
//							heigtmapCoord[1] = j; //store coordinate for grass removal
//
//							detailmapCoord = heightmapCoordToDetailmapCoord(heigtmapCoord,terrain.terrainData); //convert heightmap coordinate to detailmapcoordinate
//							DetailMap[detailmapCoord[0],detailmapCoord[1]] = 0; //remove grass from detailmap
//						}
//					}
//				}
//			}
//		}


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

	float[] patchLocation (int i, int j, Terrain terrain, Vector3 TerrainSpawnPosition) //returns realworld x,z location of patch i,j
	{
		TerrainData terrainData = terrain.terrainData;
		float heightmapHeigt = terrainData.heightmapHeight;
		float heightmapWidth = terrainData.heightmapWidth;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;
		
		float deltaLength = terrainLength / amountOfPatches2D;
		float deltaWidth = terrainWidth / amountOfPatches2D;
		
		float[] returnCoord = new float[2];

		//First term is the relative position of the patch, middle term is used to update the spawnpoint of a mushroom relative to the terrain position. The last term is used to spawn the mushroom in the middle of the patch
		returnCoord [0] = deltaLength * i + TerrainSpawnPosition.x + (lengthOfPatch / 2) / heightmapHeigt * terrainLength;
		returnCoord [1] = deltaWidth * j + TerrainSpawnPosition.z + (widthOfPatch / 2) / heightmapWidth * terrainWidth;
		
		return returnCoord;
	}

	float[] detailmapCoordToWorldCoord (int[] coord, TerrainData terrainData)
	{
		float[] returnCoord = new float[2];
		int detailHeight = terrainData.detailHeight;
		int detailWidth = terrainData.detailWidth;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;

		returnCoord [0] = ((((float)coord [0]) / detailHeight) * terrainLength) + TerrainSpawnPosition.x;
		returnCoord [1] = ((((float)coord [1]) / detailWidth) * terrainWidth) + TerrainSpawnPosition.z;

		return returnCoord;
	}


	//Returns the wordlcoordinate corrosponding to the detailmapcoordinate
	int[] worldCoordToDetailmapCoord (float[] coord, TerrainData terrainData)
	{
		int[] returnCoord = new int[2];
		int detailHeight = terrainData.detailHeight;
		int detailWidth = terrainData.detailWidth;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;

		returnCoord [0] = Mathf.FloorToInt ((coord [0]) / terrainLength * detailHeight);
		returnCoord [1] = Mathf.FloorToInt ((coord [1]) / terrainWidth * detailWidth);

		return returnCoord;
	}

	//Returns the heigtmapcoordinate corrosponding to the detailmapcoordinate
	int[] heightmapCoordToDetailmapCoord (int[] coord, TerrainData terrainData)
	{
		float[] tempCoord = heightmapCoordToWorldCoord (coord, terrainData);
		int[] returnCoord = worldCoordToDetailmapCoord (tempCoord, terrainData);

		return returnCoord;
	}

	//Returns the worldCoordinate corrosponding to the heightmapcoordinate
	float[] heightmapCoordToWorldCoord (int[] coord, TerrainData terrainData)
	{
		float[] returnCoord = new float[2];
		float heightmapHeigt = terrainData.heightmapHeight;
		float heightmapWidth = terrainData.heightmapWidth;
		float terrainLength = terrainData.size.x;
		float terrainWidth = terrainData.size.z;

		returnCoord [0] = coord [0] / heightmapHeigt * terrainLength;
		returnCoord [1] = coord [1] / heightmapWidth * terrainWidth;

		return returnCoord;

	}


}
