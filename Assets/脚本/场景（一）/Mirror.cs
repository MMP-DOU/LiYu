using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    Transform m;
    public GameObject Light;
    private bool flag = false;
    private Collider nowlight;
    //private float cnt = 10;
    // Start is called before the first frame update
    void Start()
    {
        m = this.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!nowlight && flag==true)
        {
            Debug.Log("离开光线");
            DestroyLight();
        }
    }
 
    private void OnTriggerStay(Collider light)
    {
        if (light.tag.Equals("Light")&&flag==false) {
            Debug.Log("接触光线");
            SummonLight();
            nowlight = light;
        }
    }

    private void OnTriggerExit(Collider light)
    {
        if (light.tag.Equals("Light") && flag == true)
        {
            Debug.Log("离开光线");
            DestroyLight();
        }
    }
    private void SummonLight()//创建反射光线
    {
        flag = true;
        GameObject lig = Object.Instantiate(Light, this.transform);
        Vector3 pos = m.transform.position;
        Vector3 rot = this.transform.localEulerAngles;
        lig.transform.position = pos;
        lig.transform.localEulerAngles = new Vector3(0, 90, 0);
        lig.transform.localScale = new Vector3(1,1,2);
    }

    private void DestroyLight()
    {
        Object.Destroy(this.transform.GetChild(1).gameObject);
        flag = false;
    }
}
