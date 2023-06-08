using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hero"))
        {
            other.transform.position += playerChangePos;
            camera.transform.position += cameraChangePos;
        }
    }
}
