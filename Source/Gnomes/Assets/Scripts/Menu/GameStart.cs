using UnityEngine;
using System.Collections;

public class GameStart : MonoBehaviour {

    private GameManger gamemanager;

    void Awake()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
    }

    public void ChapterLoad()
    {
		gamemanager.emptySaves ();
		Debug.Log("loading chapter 1");
        Application.LoadLevel("Chapter0");
    }
}
