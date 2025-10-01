using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ReciveLight2 : MonoBehaviour
{
    [Header("类型：1或2")]
    public short type = 1;
    private Collider nowlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nowlight==null)
        {
            fuckBool();
        }
    }

    private void OnTriggerStay(Collider other1)
    {
        if (other1.tag.Equals("Light"))
        {
            Debug.Log("机关启动"+this.name);
            nowlight = other1;
            changeBool();
        }
    }

    private void OnTriggerExit(Collider other1)
    {
        if (other1.tag.Equals("Light"))
        {
            Debug.Log("机关关闭");
            fuckBool();
        }
    }
    private void changeBool()
    {
        if (type == 1)
        {
            Debug.Log("type="+type);
            transform.parent.GetComponent<clum2>().one = true;
        }
        else if (type == 2)
        {
            Debug.Log("type=" + type);
            transform.parent.GetComponent<clum2>().two = true;
        }
    }
    private void fuckBool()
    {
        if (type == 1)
        {
            transform.parent.GetComponent<clum2>().one = false;
        }
        else if (type == 2)
        {
            transform.parent.GetComponent<clum2>().two = false;
        }
    }
}
