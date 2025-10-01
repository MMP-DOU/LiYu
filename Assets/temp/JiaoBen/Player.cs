using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using static UnityEngine.Rendering.HDROutputUtils;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    AsyncOperation operation;
    public static Player Instance;
    public GameObject smithy;
    public float smithyInteractionRange = 3f;
    public GameObject collectionCountUI;
    public Text collectionCountText;
    public GameObject recipePromptUI;
    public Text recipePromptText;
    public GameObject successUI;
    public GameObject failUI;
    public Button retryButton; // 新增：失败界面的重试按钮
    public int collectionLimit = 10; // 玩家拾取数量限制
    public int verificationLimit = 3; // 验证次数限制
    private int currentVerificationCount = 0;
    public float pickUpRange = 2f; // 玩家拾取磁石的范围
    public float putDownRange = 1f; // 玩家放下磁石的范围

    public enum MagnetType
    {
        TypeA,
        TypeB,
        TypeC,
        TypeD,
        TypeE
    }

    public Dictionary<MagnetType, int> collectedMagnets = new Dictionary<MagnetType, int>();
    public Dictionary<MagnetType, int> compassRecipe = new Dictionary<MagnetType, int>();
    private Stack<Magnet> pickedMagnets = new Stack<Magnet>(); // 用于记录捡起磁石的顺序

    private bool isInSmithyRange = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (MagnetType type in System.Enum.GetValues(typeof(MagnetType)))
        {
            collectedMagnets[type] = 0;
        }

        GenerateRecipe();
    }

    private void Start()
    {
        if (smithy == null)
        {
            smithy = GameObject.Find("Smithy");
        }
        // 调整collectionCountUI的锚点和枢轴
        RectTransform rectTransform = collectionCountUI.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(0, 0);

        // 调整位置到左下角
        rectTransform.anchoredPosition = new Vector2(0, 0);
        Debug.Log("collectionCountUI位置设置完成");

        // 初始隐藏提示配方界面、成功界面和失败界面
        if (recipePromptUI != null)
        {
            recipePromptUI.SetActive(false);
        }
        if (successUI != null)
        {
            successUI.SetActive(false);
        }
        if (failUI != null)
        {
            failUI.SetActive(false);
        }

        // 新增：为重试按钮添加点击事件监听
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClick);
        }

        //加载下一个场景
        StartCoroutine(loadScene());
    }

    private void Update()
    {
        UpdateCollectionCountUI();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PickUpNearbyMagnet();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PutDownMagnet();
        }

        float distanceToSmithy = Vector3.Distance(transform.position, smithy.transform.position);
        if (distanceToSmithy <= smithyInteractionRange)
        {
            if (!isInSmithyRange)
            {
                CheckRecipe();
                isInSmithyRange = true;
            }
        }
        else
        {
            if (isInSmithyRange)
            {
                if (recipePromptUI != null)
                {
                    recipePromptUI.SetActive(false);
                }
                if (successUI != null)
                {
                    successUI.SetActive(false);
                }
                if (failUI != null)
                {
                    failUI.SetActive(false);
                }
                isInSmithyRange = false;
            }
        }
    }

    //场景加载函数
    IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync(4);
        operation.allowSceneActivation = false;
        yield return operation;
    }
    private void UpdateCollectionCountUI()
    {
        string text = "收集磁石数量:\n";
        foreach (var entry in collectedMagnets)
        {
            text += entry.Key.ToString() + ": " + entry.Value + "\n";
        }
        if (collectionCountText != null)
        {
            collectionCountText.text = text;
        }
    }

    private void PickUpNearbyMagnet()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRange);
        foreach (Collider collider in colliders)
        {
            Magnet magnet = collider.GetComponent<Magnet>();
            if (magnet != null && TotalCollectedMagnets() < collectionLimit)
            {
                magnet.PickUp();
                break;
            }
        }
    }

    private void PutDownMagnet()
    {
        if (pickedMagnets.Count > 0)
        {
            Magnet magnetToPutDown = pickedMagnets.Peek();
            float distanceToSpawnPoint = Vector3.Distance(transform.position, magnetToPutDown.originalSpawnPoint.position);
            if (distanceToSpawnPoint <= putDownRange)
            {
                pickedMagnets.Pop();
                magnetToPutDown.PutDown();
            }
            else
            {
                Debug.Log("你不在磁石的生成点附近，无法放下磁石。");
            }
        }
    }

    private int TotalCollectedMagnets()
    {
        int total = 0;
        foreach (int count in collectedMagnets.Values)
        {
            total += count;
        }
        return total;
    }

    private GameObject FindMagnetPrefab(MagnetType type)
    {
        MagnetSpawner spawner = FindObjectOfType<MagnetSpawner>();
        if (spawner != null)
        {
            foreach (GameObject prefab in spawner.magnetPrefabs)
            {
                Magnet magnet = prefab.GetComponent<Magnet>();
                if (magnet != null && magnet.magnetType == type)
                {
                    return prefab;
                }
            }
        }
        return null;
    }

    public void CollectMagnet(MagnetType type, Magnet magnet)
    {
        if (collectedMagnets.ContainsKey(type))
        {
            collectedMagnets[type]++;
            pickedMagnets.Push(magnet); // 将捡起的磁石加入栈中
            Debug.Log($"成功收集一个 {type} 类型的磁石，当前字典数量: {collectedMagnets[type]}");
        }
    }

    public void RemoveMagnet(MagnetType type)
    {
        if (collectedMagnets.ContainsKey(type) && collectedMagnets[type] > 0)
        {
            collectedMagnets[type]--;
            Debug.Log($"成功移除一个 {type} 类型的磁石，当前字典数量: {collectedMagnets[type]}");
        }
    }

    private void CheckRecipe()
    {
        bool isRecipeMet = true;
        string clue = "";

        foreach (var entry in compassRecipe)
        {
            if (collectedMagnets[entry.Key] != entry.Value)
            {
                isRecipeMet = false;
                if (collectedMagnets[entry.Key] < entry.Value)
                {
                    clue += entry.Key.ToString() + " 少了 " + (entry.Value - collectedMagnets[entry.Key]) + " 个；";
                }
                else
                {
                    clue += entry.Key.ToString() + " 多了 " + (collectedMagnets[entry.Key] - entry.Value) + " 个；";
                }
            }
        }

        if (isRecipeMet)
        {
            ShowSuccessUI();
            operation.allowSceneActivation = true;
            //
            if (recipePromptUI != null)
            {
                recipePromptUI.SetActive(false);
            }
            if (failUI != null)
            {
                failUI.SetActive(false);
            }
        }
        else
        {
            currentVerificationCount++;
            if (currentVerificationCount >= verificationLimit)
            {
                ShowFailUI();
                if (recipePromptUI != null)
                {
                    recipePromptUI.SetActive(false);
                }
                if (successUI != null)
                {
                    successUI.SetActive(false);
                }
            }
            else
            {
                ShowRecipePrompt(clue);
                if (successUI != null)
                {
                    successUI.SetActive(false);
                }
                if (failUI != null)
                {
                    failUI.SetActive(false);
                }
            }
        }
    }

    private void ShowRecipePrompt(string clue)
    {
        if (recipePromptUI != null)
        {
            recipePromptUI.SetActive(true);
            if (recipePromptText != null)
            {
                recipePromptText.text = "此次收集磁石：" + clue;
            }
        }
    }

    private void ShowSuccessUI()
    {
        if (successUI != null)
        {
            successUI.SetActive(true);
        }
    }

    private void ShowFailUI()
    {
        if (failUI != null)
        {
            failUI.SetActive(true);
        }
        // 不再立即重启游戏，等待用户点击重试按钮
    }

    private void OnRetryButtonClick()
    {
        RestartGame();
    }

    private void RestartGame()
    {
        currentVerificationCount = 0;
        foreach (Player.MagnetType type in System.Enum.GetValues(typeof(Player.MagnetType)))
        {
            collectedMagnets[type] = 0;
        }
        pickedMagnets.Clear(); // 清空栈
        GenerateRecipe();
        MagnetSpawner spawner = FindObjectOfType<MagnetSpawner>();
        if (spawner != null)
        {
            spawner.SpawnMagnets();
        }
        if (recipePromptUI != null)
        {
            recipePromptUI.SetActive(false);
        }
        if (successUI != null)
        {
            successUI.SetActive(false);
        }
        if (failUI != null)
        {
            failUI.SetActive(false);
        }
    }

    private void GenerateRecipe()
    {
        compassRecipe.Clear();
        int numTypes = Random.Range(2, 4); // 配方中磁石种类数量
        List<MagnetType> allTypes = new List<MagnetType>(System.Enum.GetValues(typeof(MagnetType)) as MagnetType[]);
        for (int i = 0; i < numTypes; i++)
        {
            int randomIndex = Random.Range(0, allTypes.Count);
            MagnetType type = allTypes[randomIndex];
            allTypes.RemoveAt(randomIndex);
            int count = Random.Range(1, 3);
            compassRecipe[type] = count;
        }
    }
}