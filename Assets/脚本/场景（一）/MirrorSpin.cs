using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public Camera cam;
    private bool flag = false;
    public string namee;
    private int fl = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flag && Input.GetKeyDown(KeyCode.E))
        {
            Animator ani = this.GetComponent<Animator>();
            if (fl == 0)
            {
                ani.SetInteger(namee, 1);
                fl++;
            }
            else if (fl == 1)
            {
                ani.SetInteger(namee, 2);
                fl++;
            }
            else if (fl == 2)
            {
                ani.SetInteger(namee, 3);
                fl++;
            }
            else if (fl == 3)
            {
                ani.SetInteger(namee, 0);
                fl=0;
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("玩家进入触发范围");
            showUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("玩家离开触发范围");
            hindUI();
        }
    }

    private void showUI()
    {
        cam.farClipPlane = 114f;
        flag = true;
   
    }

    private void hindUI()
    {
        cam.farClipPlane = 98f;
        flag = false;
    }
}
