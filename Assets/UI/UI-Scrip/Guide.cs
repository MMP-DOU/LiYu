using System.Collections;
using System.Collections.Generic;
//using System.Drawing;
using UnityEngine;
using UnityEngine.UI;


public class Guide : MonoBehaviour
{
    [Header("�ű����")]
    public int ID;
    [Header("Ŀ��ͼ��")]
    public Image image;
    [Header("������")]
    public bool condtion = false;
    [Header("���뵭���ٶ�")]
    public float speed = 1;
    private bool IsDisplay=false,IsRun=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (condtion == true && IsDisplay == false && !IsRun) {
            StartCoroutine(imageIn());
        }
        if (condtion == false && IsDisplay == true && !IsRun) {
            StartCoroutine(imageOut());
        }
    }

    public void SetConditionT()
    {
        condtion=true;
    }
    public void SetConditionF()
    {
        condtion=false;
        Debug.Log("condition set false!");
    }
    IEnumerator imageIn()
    {
        IsRun = true;
        Color temp = image.color;
        temp.a = 0;
        image.color = temp;
        while (image.color.a < 1)
        {
            image.color += new Color(0, 0, 0, speed * Time.deltaTime);
            yield return null;
        }
        IsRun = false;
        IsDisplay =true;
    }
    IEnumerator imageOut()
    {
        IsRun = true;
        Color temp = image.color;
        temp.a = 1;
        image.color = temp;
        while (image.color.a >0)
        {
            image.color -= new Color(0, 0, 0, speed * Time.deltaTime);
            yield return null;
        }
        IsRun =false;
        IsDisplay = false;
    }
}
