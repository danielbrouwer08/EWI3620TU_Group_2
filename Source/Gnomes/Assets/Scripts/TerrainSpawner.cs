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

	// Use this for initialization
	void Start ()
	{
		int length = 3;
		generateTerrainSequence (length);
	}


	void generateTerrainSequence (int length)
	{
		Vector3 TerrainSpawnPosition = new Vector3 (0.0f, 0.0f, 0.0f);

		for(int i=0;i<length;i++)
		{
			TerrainSpawnPosition = new Vector3 (i*25.0f, 0.0f, 0.0f);
			createTerrain(TerrainSpawnPosition);
		}

	}

	void createTerrain (Vector3 TerrainSpawnPosition)
	{
		Terrain terrain;
		TerrainData terrainData = new TerrainData ();
	
		terrainData.size = new Vector3 (25.0f, 25.0f, 25.0f);
		terrainData.heightmapResolution = 100;
		terrainData.baseMapResolution = 100;
	
		//Instantiate terrain 
		//terrain = (Terrain.CreateTerrainGameObject (terrainData)).GetComponent<Terrain> ();
		terrainObject = (GameObject)GameObject.Instantiate (BaseTerrain, TerrainSpawnPosition, Quaternion.Euler (0, 0, 0));
		terrainObject.name = "GrassTerrain";
		terrain = terrainObject.GetComponent<Terrain> ();
	
		print (terrain.terrainData.heightmapWidth);
		float[,] heightmap = new float[terrain.terrainData.heightmapWidth, terrain.terrainData.heightmapHeight];
	
		for (int i=0; i<terrain.terrainData.heightmapWidth; i=i+deltaWidth) {
			for (int j=0; j<terrain.terrainData.heightmapHeight; j=j+deltaHeight) {
				//Change height on terrain on random places with a random height
				if (j <= terrain.terrainData.heightmapWidth && i <= terrain.terrainData.heightmapHeight && Random.Range (0.0f, 1.0f) > (1 - addHeightChance))
				//heightmap [i, j] = Random.Range (0.0f, 0.1f);
					heightmap [i, j] = 0.0f;
			}
		}
	
		//Add heightdifferences to the terrain
		terrain.terrainData.SetHeights (0, 0, heightmap);

	}

	// Update is called once per frame
	void Update ()
	{
			
	}
}
