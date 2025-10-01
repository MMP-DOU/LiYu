using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterJump : MonoBehaviour
{

    public float speed = 0f;
    private bool flag = false;
    private bool flag0 = false;
    private Animator anim;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!flag&&!flag0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("Jump", true);
                SetfalgT();
                Invoke("Setfalg0F", 0.6f);
            }
            else
            {
                anim.SetBool("Jump", true);
                Invoke("SetfalgT", 0.44f);
                Setfalg0T();
                Invoke("Setfalg0F", 1.45f);
                
            }
        }
    }
    //Update is called once per frame
    void FixedUpdate()
    {

        if (flag)
        {
            flag = false;
            rb.velocity = new Vector3(rb.velocity.x,13f, rb.velocity.z);
        }
    }
    void SetfalgT() { 
        flag = true;
    }
    void Setfalg0T()
    {
        flag0 = true;
    }
    void Setfalg0F()
    {
        flag0 = false;
        anim.SetBool("Jump", false);
    }
}
