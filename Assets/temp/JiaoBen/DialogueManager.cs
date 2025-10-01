using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Button startQuizButton;

    private string[] dialogueLines = {
        "欢迎来到我的药店，最近药材配比遇到点杠杆原理的问题。",
        "你来帮我解答一下吧，开始答题吧！"
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