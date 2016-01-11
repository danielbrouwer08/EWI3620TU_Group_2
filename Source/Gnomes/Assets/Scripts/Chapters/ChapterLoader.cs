using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChapterLoader : MonoBehaviour
{

	public GameObject terrainGenerator;
	public GameObject interpolator;
	public GameObject chapterStartCheckpoint;
	public GameObject ChapterEndCheckpoint;
	public GameObject mountain;
	public GameObject interpolatorMountain;
	public GameObject[] levels;
	private GameObject lastTerrainSpawner;
	private Vector3 spawnOffset = new Vector3 (0.0f, 0.0f, 0.0f);
	public bool doneLoading;

	// Use this for initialization
	void Start ()
	{

		instantiateCheckpointTerrain (chapterStartCheckpoint);

		for (int i=0; i<levels.Length; i++) {
		//for(int i=0;i<1;i++)
			interpolateTerrain (levels [i], true); //interpolate the beginning of the terrain
			instantiateTerrain (levels [i]);
			interpolateTerrain (levels [i], false); //interpolate the end of the terrain with the random terrain so landscape contains no gaps
			instantiateRandomTerrain ();
		}

		instantiateCheckpointTerrain (ChapterEndCheckpoint);

		//Wait till everything is spawned and then instantiate all mountains
		StartCoroutine (spawnMountainsWhenDone ());
	}

	IEnumerator spawnMountainsWhenDone ()
	{
		while (!lastTerrainSpawner.GetComponent<TerrainSpawner>().done) {
			yield return null;
			//nasty wait loop that waits till all randomterrain pieces are spawned
		}
		//instantiate all mountains
		instantiateMountains ();
		doneLoading = true;
	}

	void instantiateMountains ()
	{
		//List<GameObject> childs = returnTerrainObjects(terrain);
		//
		//if(childs.Count==0)//on start and end terrain the gameobject itself represents a terrain therefore no childs have to be found;
		//{
		//	childs.Add(terrain);
		//}

		//Find all terrain pieces in the level
		GameObject[] allTerrains = GameObject.FindGameObjectsWithTag ("Terrain");
		Debug.Log ("Amount of terrains: " + allTerrains.Length);
		//Debug.Log ("Childs aantal: " + childs.Count);
		//For all terrain pieces in a level add a wall / mountain to the side
		for (int k=0; k<allTerrains.Length; k++) {
			//Make a new GameObject for the mountain terrain
			GameObject CopyTerrain = new GameObject ();
			string terrainName = "Mountain";
			CopyTerrain.name = terrainName;
			CopyTerrain.transform.position = allTerrains [k].transform.position + new Vector3 (0.0f, 0.0f, allTerrains [k].GetComponent<Terrain> ().terrainData.size.z);
			CopyTerrain.tag = "Mountain";

			//Add the terraincomponent to the GameObject
			CopyTerrain.AddComponent<Terrain> ();
			//Copy the data from the prefab for the new terrain

			TerrainData BaseTerrainData = mountain.GetComponent<Terrain> ().terrainData;
			if (allTerrains [k].name == "Interpolator") {
				BaseTerrainData = interpolatorMountain.GetComponent<Terrain> ().terrainData;
			}

			TerrainData CopyTerrainData = (TerrainData)Object.Instantiate (BaseTerrainData);
			Terrain mountainTerrain = CopyTerrain.GetComponent<Terrain> ();
			mountainTerrain.terrainData = CopyTerrainData;
			TerrainData lastTerrainData = allTerrains [k].GetComponent<Terrain> ().terrainData;

			//Allocating space for a new heightmap for the terrain
			int heightmapWidth = mountainTerrain.terrainData.heightmapWidth;
			int heightmapHeight = mountainTerrain.terrainData.heightmapHeight;


			float[,] heightsOfLastTerrain = lastTerrainData.GetHeights (0, 0, lastTerrainData.heightmapWidth, lastTerrainData.heightmapHeight);
			float[,] heightmap = new float[heightmapWidth, heightmapHeight];
			float heightScaleFactor = lastTerrainData.size.y / mountainTerrain.terrainData.size.y;

			//Create same contour
			for (int i=0; i<heightmapWidth; i++) {
				for (int j=0; j<heightmapHeight; j++) {
					int i_coord = (int)i * lastTerrainData.heightmapResolution / mountainTerrain.terrainData.heightmapResolution;
					int j_coord = (int)j * lastTerrainData.heightmapResolution / mountainTerrain.terrainData.heightmapResolution;

					//float height = (heightsOfLastTerrain[i_coord,lastTerrainData.heightmapHeight-1] * heightScaleFactor);
					float height = (heightsOfLastTerrain [lastTerrainData.heightmapWidth - 1, j_coord] * heightScaleFactor);
					heightmap [i, j] = height;
				}
			}

			//Create a slope
			for (int j=0; j<heightmapHeight; j++) {
				for (int i=0; i<heightmapWidth; i++) {
					//slope with abs of sinusoid to create contours on the mountain 
					//heightmap [i, j] = heightmap [i, j] + (0.001f*i*i)*heightScaleFactor/lastTerrainData.size.y + Mathf.Abs(0.05f*Mathf.Sin(i*0.015f*Mathf.PI));
					heightmap [i, j] = heightmap [i, j] + (0.001f*i*i)*heightScaleFactor/lastTerrainData.size.y;
					//heightmap [i, j] = heightmap [i, j] + (50*Mathf.Sin((0.5f*Mathf.PI*i)/heightmapWidth))*heightScaleFactor/lastTerrainData.size.y;
				}
			}

		
			mountainTerrain.terrainData.SetHeights (0, 0, heightmap);
			CopyTerrain.AddComponent<TerrainCollider> ().terrainData = mountainTerrain.terrainData;
		
			//spawnOffset += new Vector3(0.0f,0.0f,mountainTerrain.terrainData.size.y);
		}
	}

	void interpolateTerrain (GameObject level, bool fromStart)
	{
		List<GameObject> childs = returnTerrainObjects (level);
		GameObject lastTerrain = lastTerrainPiece (childs);

		//Make a new GameObject for the interpolation terrain
		GameObject CopyTerrain = new GameObject ();
		string terrainName = "Interpolator";
		CopyTerrain.name = terrainName;
		CopyTerrain.tag = "Terrain";

		//Locate the GameObject to the right place
		CopyTerrain.transform.position = spawnOffset;
		
		//Add the terraincomponent to the GameObject
		CopyTerrain.AddComponent<Terrain> ();
		
		//Copy the data from the prefab for the new terrain
		TerrainData BaseTerrainData = interpolator.GetComponent<Terrain> ().terrainData;
		TerrainData CopyTerrainData = (TerrainData)Object.Instantiate (BaseTerrainData);
		Terrain interpolatorTerrain = CopyTerrain.GetComponent<Terrain> ();
		interpolatorTerrain.terrainData = CopyTerrainData;
		

		TerrainData lastTerrainData = lastTerrain.GetComponent<Terrain> ().terrainData;

		//USe same heightmap dimensions as previous terrain for interpolator terrainpiece
		//interpolatorTerrain.terrainData.heightmapResolution = lastTerrainData.heightmapResolution;


		//Allocating space for a new heightmap for the terrain
		int heightmapWidth = interpolatorTerrain.terrainData.heightmapWidth;
		int heightmapHeight = interpolatorTerrain.terrainData.heightmapHeight;



		//int heightmapWidth = lastTerrainData.heightmapWidth;
		//int heightmapHeight = lastTerrainData.heightmapHeight;


		//HIER VERDER!!
		//float[,] heights = lastTerrainData.GetHeights(heightmapWidth,heightmapHeight,lastTerrainData.heightmapResolution,lastTerrainData.heightmapResolution);
		float[,] heightsOfLastTerrain = lastTerrainData.GetHeights (0, 0, lastTerrainData.heightmapWidth, lastTerrainData.heightmapHeight);

		float[,] heightmap = new float[heightmapWidth, heightmapHeight];

		float heightScaleFactor = lastTerrainData.size.y / interpolatorTerrain.terrainData.size.y;
		//print ("hscf: " + heightScaleFactor);

		for (int i=0; i<heightmapWidth; i++) {
			for (int j=0; j<heightmapHeight; j++) {
				int i_coord = (int)i * lastTerrainData.heightmapResolution / interpolatorTerrain.terrainData.heightmapResolution;
				int j_coord = (int)j * lastTerrainData.heightmapResolution / interpolatorTerrain.terrainData.heightmapResolution;

				if (!fromStart) {
					//print ("heightvalue: " + i + " = " + heightsOfLastTerrain[heightmapWidth-1,j]);
					//heightmap[i,j] = lastTerrain.GetComponent<Terrain>().terrainData.GetHeights(heightmapWidth,heightmapHeight-j,1,1)[0,0];
					//heightmap[i,j] = lastTerrain.GetComponent<Terrain>().terrainData.GetHeight(heightmapWidth,heightmapHeight-j);

					float height = (heightsOfLastTerrain [i_coord, lastTerrainData.heightmapHeight - 1] * heightScaleFactor);
					heightmap [i, j] = height - height / heightmapHeight * j;
				} else {

					float height = heightsOfLastTerrain [i_coord, 0] * heightScaleFactor;
					heightmap [i, j] = height / heightmapHeight * j;
				}
			}
		}

		interpolatorTerrain.terrainData.SetHeights (0, 0, heightmap);

		CopyTerrain.AddComponent<TerrainCollider> ().terrainData = interpolatorTerrain.terrainData;

		spawnOffset += new Vector3 (interpolatorTerrain.terrainData.size.x, 0.0f, 0.0f);

	}

	GameObject lastTerrainPiece (List<GameObject> childs) //returns the terrainpiece furthest away
	{
		GameObject returnObject = childs [0];
		for (int i=0; i<childs.Count; i++) {
			if (returnObject.transform.position.x < childs [i].transform.position.x) {
				returnObject = childs [i];
			}
		}

		return returnObject;
	}

	void instantiateTerrain (GameObject level)
	{
		float y_offset_terrain = lastTerrainPiece (returnTerrainObjects (level)).transform.position.y;
		GameObject.Instantiate (level, spawnOffset - new Vector3 (0.0f, y_offset_terrain, 0.0f), Quaternion.Euler (0, 0, 0));
		//instantiateMountains(level);
		//print ("terrainlength: " + terrainLength(level));
		spawnOffset += new Vector3 (terrainLength (level), 0.0f, 0.0f);
	}

	List<GameObject> returnTerrainObjects (GameObject level)
	{
		List<GameObject> childs = new List<GameObject> ();
		;
		int childCount = level.transform.childCount;
		for (int i=0; i<childCount; i++) {
			GameObject temp = level.transform.GetChild (i).gameObject;
			if (temp.CompareTag ("Terrain")) {
				childs.Add (temp);
			}
		}

		return childs;
	}

	void instantiateRandomTerrain ()
	{
		lastTerrainSpawner = (GameObject)GameObject.Instantiate (terrainGenerator, spawnOffset, Quaternion.Euler (0, 0, 0));
		spawnOffset += new Vector3 (terrainGenerator.GetComponent<TerrainSpawner> ().amountOfTerrainsToSpawn * 50, 0.0f, 0.0f);
	}

	void instantiateCheckpointTerrain (GameObject terrain)
	{
		GameObject.Instantiate (terrain, spawnOffset, Quaternion.Euler (0, 0, 0));
		//instantiateMountains(terrain);
		spawnOffset += new Vector3 (50.0f, 0.0f, 0.0f);
	}

	float terrainLength (GameObject level) //returns the terrainlength of a gameobject containing one or multiple chained terrains
	{

		List<GameObject> childs = returnTerrainObjects (level);

		float length = 0;

		for (int i=0; i<childs.Count; i++) {
			length += childs [i].GetComponent<Terrain> ().terrainData.size.x;
		}

		return length;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
