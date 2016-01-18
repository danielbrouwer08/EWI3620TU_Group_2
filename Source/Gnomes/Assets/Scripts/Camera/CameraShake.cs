using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    private float elapsed = 0;
    private float complete;
    private float damper,x,y,z;
    public float severness = 0.3f;
    public float duration = 2;
    private Vector3 originalCam;

    public IEnumerator Shake(){ 
        while (elapsed < duration){
            elapsed += Time.deltaTime;
            complete = elapsed / duration;
            damper = 1.0f - Mathf.Clamp(4.0f * complete - 3.0f, 0.0f, 1.0f);
            x = Random.value * 2.0f - 1.0f;
            y = Random.value * 2.0f - 1.0f;
            z = Random.value * 2.0f - 1.0f;
            x *= severness * damper;
            y *= severness * damper;
            z *= severness * damper;

            Camera.main.transform.localPosition = new Vector3(x, y, z);

            yield return null;
        }
        elapsed = 0;   
    }
}
