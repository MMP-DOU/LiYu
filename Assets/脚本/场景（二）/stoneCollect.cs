using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoneCollect : MonoBehaviour
{
    [Header("��ʯ����")]
    public short num = 0;
    [Header("Ҫ���صİ�ť")]
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
