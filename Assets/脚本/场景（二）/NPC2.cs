using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC2 : MonoBehaviour
{
    [Header("显示按钮")]
    public Button button;
    [Header("完成事件数")]
    public short num = 0;
    private bool btFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider pl)
    {
        if (pl.tag.Equals("Player") && btFlag)
        {
            button.gameObject.SetActive(true);
            btFlag = false;
        }
    }

    private void OnTriggerExit(Collider pl)
    {
        if (pl.tag.Equals("Player") && !btFlag)
        {
            button.gameObject.SetActive(false);
            btFlag = true;
        }
    }
}
