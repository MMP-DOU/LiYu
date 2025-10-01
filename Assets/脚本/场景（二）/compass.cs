using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class compass : MonoBehaviour
{
    public GameObject Compass;
    public Photographer _photographer;
    private Transform _followingTarget;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Compass.transform.rotation = Quaternion.Euler(0, -_photographer.Yaw-225f, 0);
    }
}
