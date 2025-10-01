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
    [Header("ת��ͼƬ")]
    public Image ToOne;
    [Tooltip("���������ٶ�")]
    public float speed = 1;
    [Header("��Ƶ������")]
    public GameObject video;
    AsyncOperation operation;
    public short Scenenumber;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScene());//���ó������غ��� 
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("���ؽ��ȣ�"+operation.progress);//������ؽ��ȣ�operation.progress�Ǽ��ؽ��ȵ���ֵ0-0.9
     
    }
    //�첽���س���
    IEnumerator LoadScene()
    {
        operation = SceneManager.LoadSceneAsync(Scenenumber);
        operation.allowSceneActivation = false;//�������Զ�����
        yield return operation;
        
    }
     public void changeScene() {
        StartCoroutine(changeToBlack()); //���س���
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
