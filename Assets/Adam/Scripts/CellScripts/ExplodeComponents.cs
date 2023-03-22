using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeComponents : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_Objects;
    [SerializeField] private float m_Scale;
    private float m_MidY;
    private List<Vector3> m_Positions;
    
    private void Start()
    {
        m_Positions = GetPositions();
        m_MidY = GetMidY();
    }

    private List<Vector3> GetPositions()
    {
        List<Vector3> positions = new List<Vector3>();
        foreach (GameObject obj in m_Objects)
        {
            positions.Add(obj.transform.position);
        }

        return positions;
    }

    private float GetMidY()
    {
        float sumY = 0f;
        foreach (Vector3 pos in m_Positions)
        {
            sumY += pos.y;
        }

        return sumY / m_Positions.Count;
    }

    public void ExplodeModel()
    {
        foreach (GameObject obj in m_Objects)
        {
            Vector3 pos = obj.transform.position;
            obj.transform.position = new Vector3(pos.x, m_MidY + (pos.y - m_MidY) * m_Scale, pos.z);
        }
    }
    
    public void ResetModel()
    {
        int i = 0;
        foreach (GameObject obj in m_Objects)
        {
            obj.transform.position = m_Positions[i];
            i++;
        }
    }
}
