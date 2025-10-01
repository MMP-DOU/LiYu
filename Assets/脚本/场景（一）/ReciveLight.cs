using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class ReciveLight : MonoBehaviour
{
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Light"))
        {
            Debug.Log("»ú¹ØÆô¶¯");
            openDoor();
        }
    }

    private void openDoor()
    {
        Animator anim = door.GetComponent<Animator>();
        anim.SetTrigger("Trigger");
    }
}
