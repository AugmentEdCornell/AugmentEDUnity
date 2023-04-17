using UnityEngine;
using UnityEngine.UI;

public class MyToggle : MonoBehaviour
{
    [SerializeField] private int m_ToggleID;
    private Toggle m_Toggle;
    [SerializeField] private Quiz m_Quiz;
    
    [SerializeField] private Toggle[] m_Toggles;

    private void Start()
    {
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(MyOnValueChanged);
    }

    private void MyOnValueChanged(bool isOn)
    {
        // act only if this is turned on
        if (!isOn)
        {
            m_Toggle.isOn = false;
            m_Quiz.RemoveAnswer();
        }
        if (isOn)
        {
            //Debug.Log("TurnedOn");
            foreach (Toggle t in m_Toggles)
            {
                t.isOn = false;
                //Debug.Log("off");
            }
            // record answer
            m_Quiz.Answer(m_ToggleID);
        }
    }
}
