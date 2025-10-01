using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    [Header("�Ի��ı�.csv")]
    public TextAsset dialogDataDile;
    [Header("��ɫ���ı�TMP")]
    public TMP_Text nameText;
    [Header("�Ի������ı�TMP")]
    public TMP_Text dialogText;
    [Header("��ǰ�Ի�����")]
    public int dialogIndex;
    [Header("�Ի��ı������зָ�")]
    public string[] dialogRows;
    [Header("�Ի���ťԤ����")]
    public GameObject optionButton;
    [Header("��ť�����ڵ㣬��������")]
    public Transform buttonGroup;
    [Header("���ؼ���öԻ��İ�ť")]
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
        ReadText(dialogDataDile);
        ShowDialogRow();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
        {
            ShowDialogRow();
        }
    }

    //���µ�ǰ�Ի���ʾ�ı�
    public void UpdateText(string name,string text)
    {
        nameText.text = name;
        dialogText.text = text;
    }

    //��ȡcsv�ļ�
    public void ReadText(TextAsset textAsset)
    {
        dialogRows = textAsset.text.Split('\n');
        //foreach (var row in rows) {
        //    string[] cell = row.Split(',');
        //}
        Debug.Log("��ȡ�ɹ�");
    }

    //���ݵ�ǰ�Ի����������ı���ʾ����
    public void ShowDialogRow()
    {
        button.gameObject.SetActive(false);
        for (int i = 0; i < dialogRows.Length;i++) {
            //�ָ��i��
            string[] cells = dialogRows[i].Split(',');
            //������ʾ�ı�
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[3]);
                //�����л�����һ�仰
                dialogIndex = int.Parse(cells[4]);
                break;
            }
            //ѡ���ı�
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex) 
            {
                GenerateOption(i);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("�Ի�����");
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    //��ʾ��ť
    public void GenerateOption(int index)
    {
        string[] cells = dialogRows[index].Split(",");
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //�󶨰�ť�¼�
            button.GetComponentInChildren<TMP_Text>().text = cells[3];
            button.GetComponent<Button>().onClick.AddListener
                (
                    delegate 
                    { 
                        OnOptionClick(int.Parse(cells[4])); 
                    }
                );
            //��������ʶ�𣬿�����һ���ǲ���ѡ�����͵ĶԻ���һ���԰����жԻ���ť����ʾ������
            GenerateOption(index + 1);
        }
        
    }

    public void OnOptionClick(int ID)
    {
        dialogIndex = ID;
        ShowDialogRow();
        for (int i = 0; i < buttonGroup.childCount; i++)
        {
            Destroy(buttonGroup.GetChild(i).gameObject);
        }
    }
}
