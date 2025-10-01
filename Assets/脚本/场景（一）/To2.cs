using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class To2 : MonoBehaviour
{
    [Header("ת��ͼƬ")]
    public Image ToOne;
    public Image Image_Effect;
    [Tooltip("���������ٶ�")]
    public float speed = 1;
    public Camera cam;
    private bool flag = false;
    AsyncOperation operation;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadScene());
        StartCoroutine(changeToScene());
    }
    IEnumerator loadScene()
    {
      operation =  SceneManager.LoadSceneAsync(2);
        operation.allowSceneActivation = false;
        yield return operation;
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
        while (ToOne.color.a < 1)
        {
            ToOne.color += new Color(0, 0, 0, 0.5f * Time.deltaTime);
            yield return null;
        }
        Invoke("changeToNext", 7);

    }
    IEnumerator changeToScene()
    {
        Color temp = Image_Effect.color;
        temp.a = 1;
        Image_Effect.color = temp;
        while (Image_Effect.color.a > 0)
        {
            Image_Effect.color -= new Color(0, 0, 0, speed * Time.deltaTime);
            yield return null;
        }
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
    // Update is called once per frame
    void Update()
    {
        if (flag && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(changeToBlack());
        }
        Debug.Log(operation.progress);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("��ҽ��봥����Χ");
            showUI();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("����뿪������Χ");
            hindUI();
        }
    }


    private void showUI()
    {
        cam.farClipPlane = 114f;
        flag = true;

    }

    private void hindUI()
    {
        cam.farClipPlane = 98f;
        flag = false;
    }
}
