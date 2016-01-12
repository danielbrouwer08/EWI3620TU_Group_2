using UnityEngine;
using System.Collections;

public class GraphController : MonoBehaviour {
    public AstarPath Astar;
    public ChapterLoader loader;
    private bool loaded = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (loader.doneLoading && loaded == false)
        {
            loaded = true;
            Astar.Scan();
        }
	}
}
