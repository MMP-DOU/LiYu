using System.ComponentModel;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [Header("����ע��")]
    public Player.MagnetType magnetType;
    private bool isPickedUp = false;
    public Transform originalSpawnPoint; // ��¼��ʯ��ԭʼ���ɵ�

    public void PickUp()
    {
        if (!isPickedUp)
        {
            Debug.Log($"���Լ��� {magnetType} ���͵Ĵ�ʯ");
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
            gameObject.transform.position = originalSpawnPoint.position; // ����ʯ�Ż�ԭʼ���ɵ�
            gameObject.SetActive(true);
        }
    }
}