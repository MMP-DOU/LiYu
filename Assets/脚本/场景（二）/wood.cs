using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class wood : MonoBehaviour
{
    [Header("��ʾ����")]
    public string tips;
    [Header("�Ի���")]
    public GameObject gb;
    [Header("��ʾ����")]
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
        text.text = "�˴�"+tips+ "����";
    }

    private void OnTriggerExit(Collider other)
    {
        gb.SetActive(false);
        
    }
}
