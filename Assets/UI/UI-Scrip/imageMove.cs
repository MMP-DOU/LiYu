using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageMove : MonoBehaviour
{
    public float offsetMul;//ƫ�Ƴ���
    public float smoothTime;

    private Vector2 startPosition;//��ʼλ��
    private Vector3 velocity;//�ٶ�
    private float pos_z;//��ǰλ��z����
    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector2(this.transform.position.x, this.transform.position.y);//��ǰλ��Ϊ��ʼλ��
        pos_z = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);//���λ��ת��Ϊ�ӿ�ƫ��
        transform.position = Vector3.SmoothDamp(transform.position, startPosition+(offset*offsetMul),ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x,transform.position.y,pos_z);
    }
}
