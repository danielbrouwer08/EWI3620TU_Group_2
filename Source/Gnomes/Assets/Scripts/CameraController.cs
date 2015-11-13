using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    private Vector3 offset1;
    private Vector3 offset2;
    private Vector3 combinedoffset;
    private Vector3 combinedposition;
    private Vector3 playerdistances;
    private Vector3 cameradistance;
    private float cameratrigger;
    public int sensitivity;
	
	void Start () {
        offset1 = transform.position - Player1.transform.position;
        offset2 = transform.position - Player2.transform.position;
    }

    void LateUpdate () {
        UpdateCamera();
        UpdateDistance();
	}

    void CameraProperties(){
        combinedoffset = (offset1 + offset2) / 2;
        combinedposition = (Player1.transform.position + Player2.transform.position) / 2;
        playerdistances = Player1.transform.position - Player2.transform.position;
    }

    void UpdateDistance(){
        CameraProperties();
        cameratrigger = playerdistances.magnitude * sensitivity;
        if (Camera.main.fieldOfView < 100 && Camera.main.fieldOfView > 50 && cameratrigger > 50 && cameratrigger < 100)
        {
            Camera.main.fieldOfView = cameratrigger;
        }
    }

    void UpdateCamera(){
        CameraProperties();
        transform.position = combinedposition + combinedoffset;
    }
}
