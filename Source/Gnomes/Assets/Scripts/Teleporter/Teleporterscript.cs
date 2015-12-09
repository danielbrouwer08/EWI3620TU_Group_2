using UnityEngine;
using System.Collections;

public class Teleporterscript : MonoBehaviour {

    public GameObject player;
    public float playerNum;
    public float teleportNum;
    public float teleportNumNew;
    private GameObject[] teleporters;
    private Vector3 position;

	// Use this for initialization
	void Start () {
        teleporters = GameObject.FindGameObjectsWithTag("Teleporter");
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
        if (other.GetComponent<PlayerController>().playerNum == 2)
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
