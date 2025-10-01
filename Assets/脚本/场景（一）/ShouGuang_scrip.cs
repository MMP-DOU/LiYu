//using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouGuang_scrip : MonoBehaviour
{
    public GameObject Light;
    [Header("光线起始的角度")]
    public float x, y, z;
    bool flag = true;
    bool triger = false;
    // Start is called before the first frame update
    void Start()
    {

        triger = this.transform.parent.GetComponent< colume_scrip > ().startlight;
        
    }
    // Update is called once per frame
    void Update()
    {
        if (triger&&flag)
        {
            SummonLight();
        }
        
    }
    private void SummonLight()//创建反射光线
    {
        flag = false;
        GameObject lig = Object.Instantiate(Light, this.transform);
        Vector3 pos = this.transform.GetChild(0).position;
        Vector3 rot = this.transform.localEulerAngles;
        lig.transform.position = pos;
        lig.transform.localEulerAngles = new Vector3(x, y, z);
        lig.transform.localScale = new Vector3(1, 1, 1);
    }

    private void DestroyLight()
    {
        Object.Destroy(this.transform.GetChild(0).gameObject);
        flag = true;
    }
}
