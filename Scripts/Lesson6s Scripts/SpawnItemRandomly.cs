using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemRandomly : MonoBehaviour
{

    [SerializeField]
    GameObject[] items;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThrowThem());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ThrowThem()
    {
        while (true)
        {
            Vector3 spawnPos;
            do
            {
                spawnPos = new Vector3(
                Random.Range(-17.0f, 101.0f),
                Random.Range(1/3f, 10.0f),
                Random.Range(-17.0f, 113.0f));
            }
            while(isInTheArea(-6.5f,90.5f,-6.5f,102.5f,spawnPos));
            Instantiate(items[Random.Range(0, items.Length)], spawnPos, Quaternion.identity);

            float timeWait = Random.Range(1,10);
            yield return new WaitForSeconds(timeWait);
        }
    }
    bool isInTheArea(float x0, float x, float z0, float z, Vector3 result)
    {
        return result.x > x0 && result.x < x && result.z > z0 && result.z < z;
    }
}
//x: -17, 101; z:-17, 113 out
//x: -6.5, 90.5; z: -6.5, 102.5 in