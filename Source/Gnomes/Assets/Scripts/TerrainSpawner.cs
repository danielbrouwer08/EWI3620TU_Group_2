using UnityEngine;
using System.Collections;

public class TerrainSpawner : MonoBehaviour
{
				
	private GameObject terrainObject;
	//private Terrain terrain;
	public int deltaWidth;
	public int deltaHeight;
	public float addHeightChance;
	public GameObject BaseTerrain;
	public GameObject Mushroom;

	// Use this for initialization
	void Start ()
	{
		int length = 4;
		generateTerrainSequence (length);
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
		print (terrainName + ": " + "heightmapWidt: " + heightmapWidth + " " + "heightmapHeight: " + heightmapHeight);

		//Make heightmap all zero for flat terrain
		//float[,] heightmap = returnEmptyHeightmap(heightmapWidth,heightmapHeight);

		//Calculate a random heightmap
		//float[,] heightmap = calculateNewRandomHeightmap (heightmapWidth, heightmapHeight);

		//Calculate new 'random' Heightmap
		float[,] heightmap = calculateNewHeightmap (heightmapWidth, heightmapHeight);

		//Add heightmap to the terrainData.
		terrain.terrainData.SetHeights (0, 0, heightmap);

		//Add mushrooms to the terrainData
		//addMushrooms(terrain.terrainData,TerrainSpawnPosition,5,5);

		//Add a terrainCollider to the GameObject
		CopyTerrain.AddComponent<TerrainCollider> ().terrainData = terrain.terrainData;
	}

	//void addMushrooms(TerrainData terrainData, Vector3 TerrainSpawnPosition, int amount, int amountOfPatches2D, int lengthOfPatch, int widthOfPatch)
	//{
	//	float heightmapHeigt = terrainData.heightmapHeight;
	//	float heightmapWidth = terrainData.heightmapWidth;
	//	float terrainLength = terrainData.size.x;
	//	float terrainWidth = terrainData.size.z;

	//	int patchN0 = Random.Range (0,amountOfPatches2D);



	float[,] calculateNewHeightmap (int heightmapWidth, int heightmapHeight)
	{
		float[,] newHeightmap = new float[heightmapWidth, heightmapHeight];

		int lengthOfPatch = 13;
		int widthOfPatch = 13;
		int amountOfPatches2D = 5;

		//Devide the heightmap into 5*5 different patches formin a grid
		for (int h=0; h<(amountOfPatches2D); h++) {
			for (int g=0; g<(amountOfPatches2D); g++) {
				float RandomHeigthValue = Random.Range (0.1f, 0.5f);
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


	// Update is called once per frame
	void Update ()
	{
			
	}
}
