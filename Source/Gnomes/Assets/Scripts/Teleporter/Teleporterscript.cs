using UnityEngine;
using System.Collections;

public class Teleporterscript : MonoBehaviour {

    private GameObject[] players;
    public float playerNum;
    public float teleportNum;
    public float teleportNumNew;
    private GameObject[] teleporters;
    private Vector3 position;

	// Use this for initialization
	void Start () {
        teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
 
        if (other.gameObject.name.Equals("kabouterdik"))
        {
            if(teleportNum % 2 == 0)
            {
                teleportNumNew = teleportNum - 1;
                position = positionNew(teleportNumNew);
            }
            else
            {
                teleportNumNew = teleportNum + 1;
                position = positionNew(teleportNumNew);
            }
            other.gameObject.transform.position = position;
        }
        else
        {
            return;
        }
    
	}

    Vector3 positionNew(float teleportNumNew)
    {
        foreach(GameObject teleporter in teleporters)
        {
            if (teleporter.GetComponent<Teleporterscript>().teleportNum == teleportNumNew)
            {
                Debug.Log(teleportNumNew);
                return position = teleporter.transform.position + new Vector3(0,0,2f);
            }
        }
        return new Vector3(0, 0, 0);
    }
}
