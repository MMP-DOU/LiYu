using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStair : MonoBehaviour
{
    Animator animator;
    bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<clum2>().one && this.GetComponent<clum2>().two)
        {
            animator.SetBool("tra", true);
            if (flag == false)
            {
                Object.Destroy(this.transform.GetChild(6).gameObject);
                flag = true;
            }
        }
    }
}
