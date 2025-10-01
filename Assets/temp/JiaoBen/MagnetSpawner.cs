using UnityEngine;
using System.Collections.Generic;

public class MagnetSpawner : MonoBehaviour
{
    public GameObject[] magnetPrefabs;
    public Transform[] spawnPoints;
    private List<bool> isSpawnPointUsed;
    private List<Magnet> allSpawnedMagnets = new List<Magnet>(); // ��¼�������ɵĴ�ʯ

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        isSpawnPointUsed = new List<bool>(new bool[spawnPoints.Length]);
        SpawnMagnets();
    }

    public void SpawnMagnets()
    {
        ClearExistingMagnets(); // ������еĴ�ʯ
        allSpawnedMagnets.Clear(); // ��ռ�¼�б�
        isSpawnPointUsed = new List<bool>(new bool[spawnPoints.Length]);

        // ������ɵ������Ƿ��㹻
        if (spawnPoints.Length < System.Enum.GetValues(typeof(Player.MagnetType)).Length * 3)
        {
            Debug.LogError("���ɵ��������㣬�޷��������д�ʯ��");
            return;
        }

        // ÿ�ִ�ʯ��������
        foreach (Player.MagnetType type in System.Enum.GetValues(typeof(Player.MagnetType)))
        {
            for (int i = 0; i < 3; i++)
            {
                int randomSpawnPointIndex = GetUnusedSpawnPointIndex();
                if (randomSpawnPointIndex != -1)
                {
                    Transform spawnPoint = spawnPoints[randomSpawnPointIndex];
                    GameObject magnetPrefab = GetMagnetPrefab(type);
                    if (magnetPrefab != null)
                    {
                        GameObject magnetObject = Instantiate(magnetPrefab, spawnPoint.position, Quaternion.identity);
                        Magnet magnet = magnetObject.GetComponent<Magnet>();
                        if (magnet != null)
                        {
                            magnet.originalSpawnPoint = spawnPoint;
                            allSpawnedMagnets.Add(magnet); // ��ӵ����ɵĴ�ʯ�б�
                        }
                        isSpawnPointUsed[randomSpawnPointIndex] = true;
                    }
                    else
                    {
                        Debug.Log($"δ�ҵ� {type} ���͵Ĵ�ʯԤ����");
                    }
                }
            }
        }
    }

    private int GetUnusedSpawnPointIndex()
    {
        List<int> unusedIndices = new List<int>();
        for (int i = 0; i < isSpawnPointUsed.Count; i++)
        {
            if (!isSpawnPointUsed[i])
            {
                unusedIndices.Add(i);
            }
        }
        if (unusedIndices.Count > 0)
        {
            return unusedIndices[Random.Range(0, unusedIndices.Count)];
        }
        return -1;
    }

    private GameObject GetMagnetPrefab(Player.MagnetType type)
    {
        foreach (GameObject prefab in magnetPrefabs)
        {
            Magnet magnet = prefab.GetComponent<Magnet>();
            if (magnet != null && magnet.magnetType == type)
            {
                return prefab;
            }
        }
        return null;
    }

    // ������еĴ�ʯ
    private void ClearExistingMagnets()
    {
        foreach (Magnet magnet in allSpawnedMagnets)
        {
            if (magnet != null)
            {
                Destroy(magnet.gameObject);
            }
        }
    }

    // �������һ���������ڻ�ȡ�������ɵĴ�ʯ
    public List<Magnet> GetAllSpawnedMagnets()
    {
        return allSpawnedMagnets;
    }
}