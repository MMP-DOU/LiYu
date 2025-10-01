using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stonedelet : MonoBehaviour
{
    [Header("显示按钮")]
    public Button button;
    private bool btFlag = true;
    [Header("收集管理器")]
    public GameObject collet;
    // Start is called before the first frame update
    void Start()
    {
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)&&!btFlag)
        {
            this.transform.parent.gameObject.SetActive(false);
            collet.GetComponent<stoneCollect>().num++;
            button.gameObject.SetActive(false);
            btFlag = true;
        }
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
