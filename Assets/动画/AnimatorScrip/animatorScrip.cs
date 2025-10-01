using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatorScrip : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D)) {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("slowrun",true);
        }
        else
        {
            anim.SetBool("slowrun", false);
        }
       
    }
}
