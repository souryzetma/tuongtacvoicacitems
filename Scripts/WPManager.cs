using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPManager : MonoBehaviour
{
    public Transform[] targets;
    readonly Color color = Color.green;

    List<Vector3> pathPoints = new List<Vector3>();
    void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Count < 2)
            return;

        Gizmos.color = color;

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
        }

      
    }
    public void ClearPathPoints()
    {
        pathPoints.Clear();
    }
    public void AddPathPoints(Vector3 pos)
    {
        pathPoints.Add(pos);
    }
}
