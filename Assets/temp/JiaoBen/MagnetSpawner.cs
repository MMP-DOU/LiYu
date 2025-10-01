using UnityEngine;
using System.Collections.Generic;

public class MagnetSpawner : MonoBehaviour
{
    public GameObject[] magnetPrefabs;
    public Transform[] spawnPoints;
    private List<bool> isSpawnPointUsed;
    private List<Magnet> allSpawnedMagnets = new List<Magnet>(); // 记录所有生成的磁石

    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        isSpawnPointUsed = new List<bool>(new bool[spawnPoints.Length]);
        SpawnMagnets();
    }

    public void SpawnMagnets()
    {
        ClearExistingMagnets(); // 清空现有的磁石
        allSpawnedMagnets.Clear(); // 清空记录列表
        isSpawnPointUsed = new List<bool>(new bool[spawnPoints.Length]);

        // 检查生成点数量是否足够
        if (spawnPoints.Length < System.Enum.GetValues(typeof(Player.MagnetType)).Length * 3)
        {
            Debug.LogError("生成点数量不足，无法生成所有磁石！");
            return;
        }

        // 每种磁石生成三个
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
                            allSpawnedMagnets.Add(magnet); // 添加到生成的磁石列表
                        }
                        isSpawnPointUsed[randomSpawnPointIndex] = true;
                    }
                    else
                    {
                        Debug.Log($"未找到 {type} 类型的磁石预制体");
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

    // 清空现有的磁石
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

    // 可以添加一个方法用于获取所有生成的磁石
    public List<Magnet> GetAllSpawnedMagnets()
    {
        return allSpawnedMagnets;
    }
}