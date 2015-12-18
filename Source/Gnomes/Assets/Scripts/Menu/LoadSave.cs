using UnityEngine;
using System.Collections;

public class LoadSave : MonoBehaviour
{
    private GameManger gamemanager;

    void Awake()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
    }

    public void LoadLevel(int sl)
    {
        string chapter = gamemanager.saves[sl].chapter;
        PlayerPrefs.SetInt("saveslot", sl);
        Application.LoadLevel(chapter);
    }

}
