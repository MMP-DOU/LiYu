using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneCollect : MonoBehaviour
{
    [Header("磁石数量")]
    public short num = 0;
    [Header("要隐藏的按钮")]
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void numplus()
    {
        num++;
        button.SetActive(false);
    }
}
