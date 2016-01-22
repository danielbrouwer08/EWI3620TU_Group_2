using UnityEngine;
using System.Collections;

public class LoadSave : MonoBehaviour
{
    private GameManger gamemanager;

    void Awake()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
    }

    public void LoadSaveSlot(int sl)
    {
        string chapter = gamemanager.saves[sl].chapter;
        PlayerPrefs.SetInt("saveslot", sl);
        Debug.Log(gamemanager.saves[sl].toString());
        Application.LoadLevel(chapter);
    }
}
