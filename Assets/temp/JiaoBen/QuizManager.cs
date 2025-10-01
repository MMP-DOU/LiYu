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
        "�ڸܸ�ƽ��ʱ��������Ϊ 2 �ף�������Ϊ 1 �ף�����Ϊ 5 ţ����ô�����Ƕ��٣�",
        "ʡ���ܸ˵��ص���ʲô��",
        "�����ܸ�����������ʲôӦ�ã�",
        "�ȱ۸ܸ˵Ķ�����������ϵ�ǣ�",
        "Ҫʹ�ܸ�ƽ�⣬�����붯���۵ĳ˻��������������۵ĳ˻���ʲô��ϵ��",
        "�������������ʡ���ܸ˵�Ӧ�ã�",
        "�ܸ�����ת���Ĺ̶����ʲô��",
        "����ܸ˵Ķ�����С�������ۣ����ǣ�",
        "ʹ�øܸ�һ����ʡ����",
        "���˰���ʯͷ�Ĺ����У��˰��ǣ�"
    };

    private string[][] options = {
        new string[] { "2.5 ţ", "5 ţ", "10 ţ" },
        new string[] { "������С��������", "�����۴���������", "�����۵���������" },
        new string[] { "����", "����", "�˹�" },
        new string[] { "������������", "����С������", "������������" },
        new string[] { "�����붯���۵ĳ˻����������������۵ĳ˻�", "�����붯���۵ĳ˻����������������۵ĳ˻�", "�����붯���۵ĳ˻�С�������������۵ĳ˻�" },
        new string[] { "�����", "��ƽ", "��ƿ��" },
        new string[] { "֧��", "������", "������" },
        new string[] { "ʡ���ܸ�", "�����ܸ�", "�ȱ۸ܸ�" },
        new string[] { "һ����", "��һ��", "һ������" },
        new string[] { "ʡ���ܸ�", "�����ܸ�", "�ȱ۸ܸ�" }
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
            resultText.text = "�ش���ȷ��";
            score += 10;
        }
        else
        {
            resultText.text = "�ش������ȷ���ǣ�" + options[currentQuestionIndex][correctAnswers[currentQuestionIndex]];
        }
        scoreText.text = "�÷֣�" + score;

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
            resultText.text = "��ϲ�㣬�÷ִﵽ 80 �֣���ʤ���ˣ�";
        }
        else
        {
            resultText.text = "���ź�����ĵ÷�δ�ﵽ 80 �֣���������Ŷ��";
        }
    }
}