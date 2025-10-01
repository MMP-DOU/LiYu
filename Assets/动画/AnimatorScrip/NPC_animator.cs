using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_animator : MonoBehaviour
{
    [Header("挂载动画组件的物体")]
    public GameObject anim;
    private Animator animator;
    [Header("显示按钮")]
    public Button button;
    [Header("引导主控")]
    public GameObject mainc;
    private bool btFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        animator = anim.GetComponent<Animator>();
        button.gameObject.SetActive(false);
        animator = anim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider pl)
    {
        if (pl.tag.Equals("Player")&&btFlag)
        {
            button.gameObject.SetActive(true);
            btFlag = false;
        }
    }

    private void OnTriggerExit(Collider pl)
    {
        if (pl.tag.Equals("Player")&&!btFlag)
        {
            button.gameObject.SetActive(false);
            btFlag = true ;
        }
    }

    public void setAnimBool()
    {
        animator.SetBool("Bool",true);
    }
}
