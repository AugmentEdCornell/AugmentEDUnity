using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [Serializable]
    public class QuizQuestion
    {
        public string Question;
        public string Answer1;
        public string Answer2;
        public string Answer3;
        public string Answer4;
        public int CorrectAns;
    }

    [SerializeField] private QuizQuestion[] m_QuizQuestions;

    private int m_QuizIdx;

    [SerializeField] private TextMeshProUGUI m_TextQuestion;
    [SerializeField] private TextMeshProUGUI[] m_TextAnswer;
    [SerializeField] private TextMeshProUGUI m_TextId;
    
    [SerializeField] private Toggle m_Toggle1;
    [SerializeField] private Toggle m_Toggle2;
    [SerializeField] private Toggle m_Toggle3;
    [SerializeField] private Toggle m_Toggle4;

    [SerializeField] private int[] m_StudentAns;
    [SerializeField] private bool[] m_StudentAnsBool;

    public void Answer(int ans)
    {
        m_StudentAns[m_QuizIdx] = ans;
        m_StudentAnsBool[m_QuizIdx] = (ans == m_QuizQuestions[m_QuizIdx].CorrectAns);
        //UncheckAll();
    }

    public void RemoveAnswer()
    {
        m_StudentAns[m_QuizIdx] = 0;
    }

    private void Start()
    {
        m_QuizIdx = 0;
        
        DisplayQuestion(m_QuizIdx);
        m_StudentAns = new int[m_QuizQuestions.Length];
        m_StudentAnsBool = new bool[m_QuizQuestions.Length];
        UncheckAll();
    }

    public void UncheckAll()
    {
        m_Toggle1.isOn = false;
        m_Toggle2.isOn = false;
        m_Toggle3.isOn = false;
        m_Toggle4.isOn = false;

    }

    public void NextQuestion()
    {
        if (m_QuizIdx + 1 <= m_QuizQuestions.Length)
        {
            m_QuizIdx++;
            DisplayQuestion(m_QuizIdx);
        }
    }
    
    public void PreviousQuestion()
    {
        if (m_QuizIdx - 1 >= 0)
        {
            m_QuizIdx--;
            DisplayQuestion(m_QuizIdx);
        }
    }

    private void DisplayQuestion(int i)
    {
        UncheckAll();
        QuizQuestion q = m_QuizQuestions[i];
        m_TextQuestion.text = q.Question;
        m_TextAnswer[0].text = q.Answer1;
        m_TextAnswer[1].text = q.Answer2;
        m_TextAnswer[2].text = q.Answer3;
        m_TextAnswer[3].text = q.Answer4;
        m_TextId.text = (i+1) + "/" + m_QuizQuestions.Length;
    }
    
}
