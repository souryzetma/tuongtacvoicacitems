using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnThings : MonoBehaviour
{
    public Transform[] cornerInPoints;
    public GameObject prefab;

    public Transform[] cornerOutPoints;
    public GameObject prefab2;

    // Start is called before the first frame update
    void Start()
    {
        SpawnInside(cornerInPoints, prefab, 0);
        SpawnInside(cornerOutPoints, prefab2, 1);
    }
    Vector3 jump(int i)
    {
        switch (i)
        {
            case 0: return new Vector3(0, 0, 5.1f);
            case 1: return new Vector3(5, 0, 0);
            case 2: return new Vector3(0, 0, -5.1f);
            case 3: return new Vector3(-5, 0, 0);
            default: return new Vector3(0, 0, 0);
        }
    }
    Vector3 doo;

    Vector3 howRotate(Transform[] cornerInPoints, int a, int i)
    {
        switch (a)
        {
            case 0: return -cornerInPoints[i].right;
            default: return cornerInPoints[i].right;
        }
    }
    void SpawnInside(Transform[] cornerInPoints, GameObject prefab, int a)
    {
        int t = 0;
        for (int i = 0; i < cornerInPoints.Length; i++)
        {
            doo = cornerInPoints[i].position;
            Instantiate(prefab, doo + cornerInPoints[i].forward * 2.5f, Quaternion.LookRotation(howRotate(cornerInPoints, a, i)));
            if (i < cornerInPoints.Length - 1)
            {
                t = i + 1;
            }
            else
            {
                t = 0;
            }
            while (Vector3.Distance(doo, cornerInPoints[t].position) > 5)
            {
                doo += jump(i);
                Instantiate(prefab, doo, Quaternion.LookRotation(howRotate(cornerInPoints, a, i)));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
