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
	public Vector3 cameraOffset;
	private GameManger gameManager;
	public bool LoadLastCheckpoint;
	
	void Start () {
		gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();
		Vector3 camerapos;

		if(LoadLastCheckpoint)
		{
			Debug.Log (gameManager.returnCurrent().toString());
			camerapos = (gameManager.returnCurrent().P1Pos + gameManager.returnCurrent().P2Pos)/2.0f + cameraOffset;
		}else
		{
			camerapos = transform.position;
		}
        offset1 = camerapos - Player1.transform.position;
        offset2 = camerapos - Player2.transform.position;
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

    void RoundTrigger(){
        if (cameratrigger <= 50)
        {
            cameratrigger = 50;
        }
        if (cameratrigger >= 70){
            cameratrigger = 70;
        }
    }

    void UpdateDistance(){
        CameraProperties();
        cameratrigger = playerdistances.magnitude * sensitivity;
        RoundTrigger();
        if (Camera.main.fieldOfView <= 70 && Camera.main.fieldOfView >= 50){
            Camera.main.fieldOfView = cameratrigger;
        }
    }

    void UpdateCamera(){
        CameraProperties();
        transform.position = combinedposition + combinedoffset;
    }


}
