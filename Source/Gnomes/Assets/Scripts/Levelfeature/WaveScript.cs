using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveScript : MonoBehaviour {
    private GameManger gameManager;
    public ChapterLoader loader;
    public GameObject[] players;
    public GameObject[] levels;
    public float speed;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
        players = GameObject.FindGameObjectsWithTag("Player");
        StartCoroutine(loadlevels());
    }

    IEnumerator loadlevels()
    {
        while (!loader.doneLoading)
        {
            yield return null;
        }
        levels = GameObject.FindGameObjectsWithTag("Level");
        Debug.Log("Ik heb de levels toegevoegd");
    }

    // Update is called once per frame
    void FixedUpdate () {
        float xpos = transform.position.x;
        speed = 6;
        for(int i = 0; i < levels.Length; i++)
        {
            if((xpos > levels[i].transform.position.x - 5) & (xpos < levels[i].transform.position.x + 55))
            {
                speed = levels[i].GetComponent<LevelScript>().speedvar;
            }
        }
        int count = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].transform.position.x - xpos > 30)
            {
                count += 1;
            }
            if (transform.position.x > players[i].transform.position.x + 1)
            {
                transform.position = new Vector3(players[i].GetComponent<PlayerController>().getLastSavedPos().x - 10, 0, 25f);
            }
        }
        if (count == 2)
        {
            speed = 6;
        }
        transform.Translate(speed*Time.deltaTime*(new Vector3(1,0,0)));
    }
}
