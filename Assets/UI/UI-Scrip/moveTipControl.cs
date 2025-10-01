using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class moveTipControl : MonoBehaviour
{
    [Header("���Ƽ���")]
    public GameObject target;
    [Header("Ŀ������")]
    public int Id;
    private Guide[] scrips;
    private Guide guide;
    // Start is called before the first frame update
    void Start()
    {
        scrips = target.GetComponents<Guide>();
        //guide��Ŀ��ű�
        guide = Array.Find(scrips, s => s.ID == Id);
        if (guide == null)
        {
            Debug.LogError($"δ�ҵ�����Ϊ{Id}�����");
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
