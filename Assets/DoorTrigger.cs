using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject Hero; 
    public float triggerDistance = 2f;

    private void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
    }

}