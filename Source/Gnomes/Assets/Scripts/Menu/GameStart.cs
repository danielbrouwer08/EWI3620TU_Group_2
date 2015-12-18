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
        Vector3 P1Pos = new Vector3(23.5f, 3.5f, 25.0f);
        Vector3 P2Pos = new Vector3(26.5f, 3.5f, 25.0f);
        float P1Health = 100;
        float P2Health = 100;
        int P1Score = 0;
        int P2Score = 0;
        gamemanager.currentslot = 0;
        gamemanager.addNewSave(new Savegame(P1Pos, P1Health, P1Score, P2Pos, P2Health, P2Score,"Chapter1"));
        Application.LoadLevel("Chapter1");
    }
}
