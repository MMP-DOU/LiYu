using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Dialogue : MonoBehaviour
{
    [Header("对话文本.csv")]
    public TextAsset dialogDataDile;
    [Header("角色名文本TMP")]
    public TMP_Text nameText;
    [Header("对话内容文本TMP")]
    public TMP_Text dialogText;
    [Header("当前对话索引")]
    public int dialogIndex;
    [Header("对话文本，按行分割")]
    public string[] dialogRows;
    [Header("对话按钮预制体")]
    public GameObject optionButton;
    [Header("按钮树父节点，用以排列")]
    public Transform buttonGroup;
    [Header("隐藏激活该对话的按钮")]
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

    //更新当前对话显示文本
    public void UpdateText(string name,string text)
    {
        nameText.text = name;
        dialogText.text = text;
    }

    //读取csv文件
    public void ReadText(TextAsset textAsset)
    {
        dialogRows = textAsset.text.Split('\n');
        //foreach (var row in rows) {
        //    string[] cell = row.Split(',');
        //}
        Debug.Log("读取成功");
    }

    //根据当前对话的索引，改变显示内容
    public void ShowDialogRow()
    {
        button.gameObject.SetActive(false);
        for (int i = 0; i < dialogRows.Length;i++) {
            //分割第i行
            string[] cells = dialogRows[i].Split(',');
            //正常显示文本
            if (cells[0] == "#" && int.Parse(cells[1]) == dialogIndex)
            {
                UpdateText(cells[2], cells[3]);
                //索引切换到下一句话
                dialogIndex = int.Parse(cells[4]);
                break;
            }
            //选项文本
            else if (cells[0] == "&" && int.Parse(cells[1]) == dialogIndex) 
            {
                GenerateOption(i);
            }
            else if (cells[0] == "END" && int.Parse(cells[1]) == dialogIndex)
            {
                Debug.Log("对话结束");
                this.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    //显示按钮
    public void GenerateOption(int index)
    {
        string[] cells = dialogRows[index].Split(",");
        if (cells[0] == "&")
        {
            GameObject button = Instantiate(optionButton, buttonGroup);
            //绑定按钮事件
            button.GetComponentInChildren<TMP_Text>().text = cells[3];
            button.GetComponent<Button>().onClick.AddListener
                (
                    delegate 
                    { 
                        OnOptionClick(int.Parse(cells[4])); 
                    }
                );
            //继续向下识别，看看下一个是不是选项类型的对话（一次性把所有对话按钮都显示出来）
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
