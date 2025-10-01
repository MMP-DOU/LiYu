using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootLight : MonoBehaviour
{
    [Header("类型：1或2")]
    public short type = 1;
    private bool IsLight = false;
    public GameObject Light;
    public float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (type == 1)
        {
            if (this.transform.parent.GetComponent<clum2>().one == true && IsLight == false)
            {
                SummonLight();
            }
            if (this.transform.parent.GetComponent<clum2>().one == false && IsLight == true)
            {
                DestroyLight();
            }
        }
        else if (type == 2) {
            if (this.transform.parent.GetComponent<clum2>().two == true && IsLight == false)
            {
                SummonLight();
            }
            if (this.transform.parent.GetComponent<clum2>().two == false && IsLight == true)
            {
                DestroyLight();
            }
        }

    }
    private void SummonLight()//创建反射光线
    {
        IsLight = true;
        GameObject lig = Object.Instantiate(Light, this.transform);
        Vector3 pos = this.transform.GetChild(0).position;
        Vector3 rot = this.transform.localEulerAngles;
        lig.transform.position = pos;
        lig.transform.localEulerAngles = new Vector3(x, y, z);
        lig.transform.localScale = new Vector3(1, 1, 1);
    }

    private void DestroyLight()
    {
        Object.Destroy(this.transform.GetChild(1).gameObject);
        IsLight = false;
    }
}
