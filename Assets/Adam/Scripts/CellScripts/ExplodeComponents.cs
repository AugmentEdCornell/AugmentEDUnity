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

    [SerializeField] private float m_Duration = 3f;
    
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
        Camera.main.GetComponent<MouseOrbitSample>()
            .RefreshCameraFixedRotation(transform, 2.6f, 0.5f);
        foreach (GameObject obj in m_Objects)
        {
            Vector3 pos = obj.transform.position;
            //obj.transform.position = new Vector3(pos.x, m_MidY + (pos.y - m_MidY) * m_Scale, pos.z);
            StartCoroutine(MoveObjectToNewY(obj, m_MidY + (pos.y - m_MidY) * m_Scale, m_Duration));
        }
    }

    public void ResetModel()
    {
        MouseOrbitSample mouseOrbit = Camera.main.GetComponent<MouseOrbitSample>();
        mouseOrbit.RefreshCameraFixedRotation(transform, 1.5f, 0.5f);
        int i = 0;
        foreach (GameObject obj in m_Objects)
        {
            //obj.transform.position = m_Positions[i];
            StartCoroutine(MoveObjectToNewY(obj, m_Positions[i].y, m_Duration));
            i++;
        }
    }
    
    IEnumerator MoveObjectToNewY(GameObject obj, float newY, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = obj.transform.position;
        Vector3 targetPosition = new Vector3(startPosition.x, newY, startPosition.z);

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final position to ensure accuracy
        obj.transform.position = targetPosition;
    }

    void MoveObjectsToNewY(float newY, float duration)
    {
        foreach (GameObject obj in m_Objects)
        {
            StartCoroutine(MoveObjectToNewY(obj, newY, duration));
        }
    }
}
