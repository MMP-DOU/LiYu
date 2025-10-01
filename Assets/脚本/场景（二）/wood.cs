using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class wood : MonoBehaviour
{
    [Header("提示方向")]
    public string tips;
    [Header("对话框")]
    public GameObject gb;
    [Header("提示内容")]
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        gb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        gb.SetActive(true);
        text.text = "此处"+tips+ "向行";
    }

    private void OnTriggerExit(Collider other)
    {
        gb.SetActive(false);
        
    }
}
