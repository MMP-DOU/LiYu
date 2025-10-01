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
    public Button retryButton; // ������ʧ�ܽ�������԰�ť
    public int collectionLimit = 10; // ���ʰȡ��������
    public int verificationLimit = 3; // ��֤��������
    private int currentVerificationCount = 0;
    public float pickUpRange = 2f; // ���ʰȡ��ʯ�ķ�Χ
    public float putDownRange = 1f; // ��ҷ��´�ʯ�ķ�Χ

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
    private Stack<Magnet> pickedMagnets = new Stack<Magnet>(); // ���ڼ�¼�����ʯ��˳��

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
        // ����collectionCountUI��ê�������
        RectTransform rectTransform = collectionCountUI.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(0, 0);

        // ����λ�õ����½�
        rectTransform.anchoredPosition = new Vector2(0, 0);
        Debug.Log("collectionCountUIλ���������");

        // ��ʼ������ʾ�䷽���桢�ɹ������ʧ�ܽ���
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

        // ������Ϊ���԰�ť��ӵ���¼�����
        if (retryButton != null)
        {
            retryButton.onClick.AddListener(OnRetryButtonClick);
        }

        //������һ������
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

    //�������غ���
    IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync(4);
        operation.allowSceneActivation = false;
        yield return operation;
    }
    private void UpdateCollectionCountUI()
    {
        string text = "�ռ���ʯ����:\n";
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
                Debug.Log("�㲻�ڴ�ʯ�����ɵ㸽�����޷����´�ʯ��");
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
            pickedMagnets.Push(magnet); // ������Ĵ�ʯ����ջ��
            Debug.Log($"�ɹ��ռ�һ�� {type} ���͵Ĵ�ʯ����ǰ�ֵ�����: {collectedMagnets[type]}");
        }
    }

    public void RemoveMagnet(MagnetType type)
    {
        if (collectedMagnets.ContainsKey(type) && collectedMagnets[type] > 0)
        {
            collectedMagnets[type]--;
            Debug.Log($"�ɹ��Ƴ�һ�� {type} ���͵Ĵ�ʯ����ǰ�ֵ�����: {collectedMagnets[type]}");
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
                    clue += entry.Key.ToString() + " ���� " + (entry.Value - collectedMagnets[entry.Key]) + " ����";
                }
                else
                {
                    clue += entry.Key.ToString() + " ���� " + (collectedMagnets[entry.Key] - entry.Value) + " ����";
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
                recipePromptText.text = "�˴��ռ���ʯ��" + clue;
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
        // ��������������Ϸ���ȴ��û�������԰�ť
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
        pickedMagnets.Clear(); // ���ջ
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
        int numTypes = Random.Range(2, 4); // �䷽�д�ʯ��������
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