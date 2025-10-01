using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("Second");//加载第二个场景
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);
        Debug.Log(scene.isLoaded);
        Debug.Log(scene.path);
        Debug.Log(scene.buildIndex);
        Debug.Log(SceneManager.sceneCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
