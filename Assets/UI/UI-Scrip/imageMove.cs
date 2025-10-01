using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imageMove : MonoBehaviour
{
    public float offsetMul;//偏移乘数
    public float smoothTime;

    private Vector2 startPosition;//起始位置
    private Vector3 velocity;//速度
    private float pos_z;//当前位置z坐标
    // Start is called before the first frame update
    void Start()
    {
        startPosition = new Vector2(this.transform.position.x, this.transform.position.y);//当前位置为初始位置
        pos_z = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = Camera.main.ScreenToViewportPoint(Input.mousePosition);//鼠标位置转化为视口偏移
        transform.position = Vector3.SmoothDamp(transform.position, startPosition+(offset*offsetMul),ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x,transform.position.y,pos_z);
    }
}
