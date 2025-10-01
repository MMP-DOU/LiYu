using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HDROutputUtils;

public class To3 : MonoBehaviour
{
    [Header("显示按钮")]
    public Button button;
    private bool btFlag = true;
    AsyncOperation operation;
    public Image Image_Effect;
    [Tooltip("淡出淡入速度")]
    public float speed = 1;
    [Header("转场图片")]
    public Image ToOne;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadScene());
        StartCoroutine(changeToScene());
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!btFlag && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(changeToBlack());
        }
    }

    private void OnTriggerStay(Collider pl)
    {
        if (pl.tag.Equals("Player") && btFlag)
        {
            button.gameObject.SetActive(true);
            btFlag = false;
        }
    }

    private void OnTriggerExit(Collider pl)
    {
        if (pl.tag.Equals("Player") && !btFlag)
        {
            button.gameObject.SetActive(false);
            btFlag = true;
        }
    }

    IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync(3);
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
    IEnumerator next()
    {
        while (ToOne.color.a > 0)
        {
            ToOne.color -= new Color(0, 0, 0, speed * Time.deltaTime);
            yield return null;
        }
        operation.allowSceneActivation = true;
    }
    private void changeToNext()
    {
        StartCoroutine(next());
    }


}
