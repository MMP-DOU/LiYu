using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Button[] optionButtons;
    public Text[] optionTexts;
    public Text resultText;
    public Text scoreText;

    private string[] questions = {
        "在杠杆平衡时，动力臂为 2 米，阻力臂为 1 米，动力为 5 牛，那么阻力是多少？",
        "省力杠杆的特点是什么？",
        "费力杠杆在生活中有什么应用？",
        "等臂杠杆的动力和阻力关系是？",
        "要使杠杆平衡，动力与动力臂的乘积和阻力与阻力臂的乘积有什么关系？",
        "以下哪种情况是省力杠杆的应用？",
        "杠杆绕着转动的固定点叫什么？",
        "如果杠杆的动力臂小于阻力臂，它是？",
        "使用杠杆一定能省力吗？",
        "在撬棒撬石头的过程中，撬棒是？"
    };

    private string[][] options = {
        new string[] { "2.5 牛", "5 牛", "10 牛" },
        new string[] { "动力臂小于阻力臂", "动力臂大于阻力臂", "动力臂等于阻力臂" },
        new string[] { "剪刀", "镊子", "撬棍" },
        new string[] { "动力大于阻力", "动力小于阻力", "动力等于阻力" },
        new string[] { "动力与动力臂的乘积大于阻力与阻力臂的乘积", "动力与动力臂的乘积等于阻力与阻力臂的乘积", "动力与动力臂的乘积小于阻力与阻力臂的乘积" },
        new string[] { "钓鱼竿", "天平", "开瓶器" },
        new string[] { "支点", "动力点", "阻力点" },
        new string[] { "省力杠杆", "费力杠杆", "等臂杠杆" },
        new string[] { "一定能", "不一定", "一定不能" },
        new string[] { "省力杠杆", "费力杠杆", "等臂杠杆" }
    };

    private int[] correctAnswers = { 2, 1, 1, 2, 1, 2, 0, 1, 1, 0 };
    private int currentQuestionIndex = 0;
    private int score = 0;

    private void Start()
    {
        DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        if (currentQuestionIndex < questions.Length)
        {
            questionText.text = questions[currentQuestionIndex];
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionTexts[i].text = options[currentQuestionIndex][i];
                optionButtons[i].interactable = true;
            }
            resultText.text = "";
        }
        else
        {
            CheckResult();
        }
    }

    public void OnOptionSelected(int optionIndex)
    {
        if (optionIndex == correctAnswers[currentQuestionIndex])
        {
            resultText.text = "回答正确！";
            score += 10;
        }
        else
        {
            resultText.text = "回答错误！正确答案是：" + options[currentQuestionIndex][correctAnswers[currentQuestionIndex]];
        }
        scoreText.text = "得分：" + score;

        foreach (Button button in optionButtons)
        {
            button.interactable = false;
        }

        currentQuestionIndex++;
        Invoke("DisplayQuestion", 2f);
    }

    private void CheckResult()
    {
        if (score >= 80)
        {
            resultText.text = "恭喜你，得分达到 80 分，你胜利了！";
        }
        else
        {
            resultText.text = "很遗憾，你的得分未达到 80 分，继续加油哦！";
        }
    }
}