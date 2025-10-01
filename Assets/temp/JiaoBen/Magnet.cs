using System.ComponentModel;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [Header("这是注释")]
    public Player.MagnetType magnetType;
    private bool isPickedUp = false;
    public Transform originalSpawnPoint; // 记录磁石的原始生成点

    public void PickUp()
    {
        if (!isPickedUp)
        {
            Debug.Log($"尝试捡起 {magnetType} 类型的磁石");
            Player.Instance.CollectMagnet(magnetType, this);
            isPickedUp = true;
            gameObject.SetActive(false);
        }
    }

    public void PutDown()
    {
        if (isPickedUp)
        {
            Player.Instance.RemoveMagnet(magnetType);
            isPickedUp = false;
            gameObject.transform.position = originalSpawnPoint.position; // 将磁石放回原始生成点
            gameObject.SetActive(true);
        }
    }
}