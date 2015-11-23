using UnityEngine;
using System.Collections;

public class Demolish : MonoBehaviour {

    void OnCollisionStay(Collision other)
    {
        if(Input.GetButton("Fire3") && other.gameObject.CompareTag("Breakable"))
        {
            Debug.Log("verwijder");
            other.gameObject.SetActive(false);
        }
    }
}
