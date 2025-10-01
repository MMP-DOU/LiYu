using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AsyncTest : MonoBehaviour
{
    public Image Image_Effect;
    [Header("转场图片")]
    public Image ToOne;
    [Tooltip("淡出淡入速度")]
    public float speed = 1;
    [Header("视频播放器")]
    public GameObject video;
    AsyncOperation operation;
    public short Scenenumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());//调用场景加载函数 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("加载进度："+operation.progress);//输出加载进度，operation.progress是加载进度的数值0-0.9
     
    }
    //异步加载场景
    IEnumerator LoadScene()
    {
        operation = SceneManager.LoadSceneAsync(Scenenumber);
        operation.allowSceneActivation = false;//不允许自动加载
        yield return operation;
        
    }
     public void changeScene() {
        StartCoroutine(changeToBlack()); //加载场景
    }

    IEnumerator changeToBlack()
    {
        Color temp = Image_Effect.color;
        temp.a = 0;
        Image_Effect.color = temp;
        while (Image_Effect.color.a < 1)
        {
            Image_Effect.color += new Color(0, 0, 0, speed * Time.deltaTime);
            yield return null;
        }
        video.SetActive(false);
        while (ToOne.color.a < 1)
        {
            ToOne.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
            yield return null;
        }
        Invoke("changeToNext", 7);
        
    }
    private void changeToNext()
    {
        StartCoroutine(next());
    }

    IEnumerator next()
    {
        while (ToOne.color.a > 0)
        {
            ToOne.color -= new Color(0, 0, 0, speed * Time.deltaTime);
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
}
