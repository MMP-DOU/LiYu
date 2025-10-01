using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageMove0 : MonoBehaviour
{
    public float speed;
    private bool flag = true;
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 180)
        {
            flag = false;
        }
        else if (transform.position.y < -240)
        {
            flag = true;
        }

        if (flag)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        }
    }
}

