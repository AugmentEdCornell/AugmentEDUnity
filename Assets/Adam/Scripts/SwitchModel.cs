using UnityEngine;

public class SwitchModel : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Models;

    public void ShowModel(int i)
    {
        HideAll();
        m_Models[i].SetActive(true);
    }

    private void HideAll()
    {
        foreach (GameObject m in m_Models)
        {
            m.SetActive(false);
        }
    }

    private void Start()
    {
        ShowModel(0);
    }
}
