using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Button startQuizButton;

    private string[] dialogueLines = {
        "��ӭ�����ҵ�ҩ�꣬���ҩ�����������ܸ�ԭ������⡣",
        "�������ҽ��һ�°ɣ���ʼ����ɣ�"
    };
    private int currentLine = 0;

    private void Start()
    {
        startQuizButton.gameObject.SetActive(false);
        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            dialogueText.text = dialogueLines[currentLine];
            currentLine++;
            if (currentLine == dialogueLines.Length)
            {
                startQuizButton.gameObject.SetActive(true);
            }
        }
    }

    public void StartQuiz()
    {
        SceneManager.LoadScene("QuizScene");
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentLine < dialogueLines.Length)
        {
            DisplayNextLine();
        }
    }
}