using UnityEngine;
using System.Collections;

public class Lookatcamera : MonoBehaviour
{
    Transform cam;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        transform.LookAt(cam);
        transform.Rotate(0, 180, 0);
    }
}