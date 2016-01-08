using UnityEngine;
using System.Collections;

public class GraphController : MonoBehaviour {
    public AstarPath Astar;
    public ChapterLoader loader;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (loader.doneLoading)
        {
            loader.doneLoading = false;
            Astar.Scan();
        }
	}
}
