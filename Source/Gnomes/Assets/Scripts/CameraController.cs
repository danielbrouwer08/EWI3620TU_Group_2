using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public GameObject Player1;
    public GameObject Player2;
    private Vector3 offset1;
    private Vector3 offset2;
    private Vector3 combinedoffset;
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
    }

    void UpdateCamera(){
        CameraOffset();
        if (combinedoffset.magnitude > 1){
            transform.position = (Player1.transform.position + Player2.transform.position) / 2 + combinedoffset;
        }
        
    }
}
