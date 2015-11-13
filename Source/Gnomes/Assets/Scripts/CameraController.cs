using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    private Vector3 offset1;
    private Vector3 offset2;
    private Vector3 combinedoffset;
    private Vector3 playerdistances;
    public int mindistance;
	
	void Start () {
        offset1 = transform.position - Player1.transform.position;
        offset2 = transform.position - Player2.transform.position;
    }

    void LateUpdate () {
        UpdateCamera();
	}

    void CameraOffset(){
        combinedoffset = (offset1 + offset2) / 2;
        playerdistances = Player1.transform.position - Player2.transform.position;
    }

    void UpdateCamera(){
        CameraOffset();
        if (combinedoffset.magnitude > mindistance){
            transform.position = (Player1.transform.position + Player2.transform.position) / 2 + combinedoffset;
        }
    }
}
