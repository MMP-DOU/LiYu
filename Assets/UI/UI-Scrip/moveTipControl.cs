using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class moveTipControl : MonoBehaviour
{
    [Header("控制集合")]
    public GameObject target;
    [Header("目标索引")]
    public int Id;
    private Guide[] scrips;
    private Guide guide;
    // Start is called before the first frame update
    void Start()
    {
        scrips = target.GetComponents<Guide>();
        //guide是目标脚本
        guide = Array.Find(scrips, s => s.ID == Id);
        if (guide == null)
        {
            Debug.LogError($"未找到索引为{Id}的组件");
        }

        guide.SetConditionT();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Invoke("SetC", 8f);
        }
    }

    void SetC()
    {
        guide.SetConditionF();
    }
}
