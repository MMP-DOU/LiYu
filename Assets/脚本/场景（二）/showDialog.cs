using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showDialog : MonoBehaviour
{
    [Header("对话框1")]
    public GameObject dialog;
    [Header("对话框2")]
    public GameObject dialog1;
    [Header("对话框3")]
    public GameObject dialog2;
    [Header("NPC事件统计处")]
    public GameObject NPC;
    [Header("磁石收集统计处")]
    public GameObject collect;
    [Header("指南针相机")]
    public GameObject camera0;
    // Start is called before the first frame update
    void Start()
    {
        camera0.SetActive(false);
        dialog.SetActive(false);
        dialog1.SetActive(false);
        dialog2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(NPC.GetComponent<NPC2>().num == 3)
        {
            camera0.SetActive (true);
        }
    }

    public void setDialogActive() 
    {
        if (NPC.GetComponent<NPC2>().num == 0) { dialog.SetActive(true); NPC.GetComponent<NPC2>().num++; }
            
        else if (NPC.GetComponent<NPC2>().num == 1&& collect.GetComponent<stoneCollect>().num >= 3) {  dialog1.SetActive(true); NPC.GetComponent<NPC2>().num++; }
            
        else if (NPC.GetComponent<NPC2>().num == 2) {  dialog2.SetActive(true); NPC.GetComponent<NPC2>().num++; }
            
        
    }
}
