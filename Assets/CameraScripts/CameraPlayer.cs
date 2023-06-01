using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    private GameObject Hero;


    void Start()
    {
        Hero = GameObject.Find("Hero");
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Hero.transform.position.x, Hero.transform.position.y, -10f), Time.deltaTime * 2f);
    }
}
