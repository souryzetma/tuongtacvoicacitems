using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class TestDungsCode : MonoBehaviour
{
    public GameObject[] checkPoints;//Game objects đầu tiên trên chặng mới sau khi rẽ
    public float speed = 50.0f;
    private int cur = 0;
    public GameObject[] cornerObjects;//Game objects ở 4 góc
    public GameObject[] pointObjects;//Game objects rỗng cách các phần tử checkPoints một khoảng bằng 10 về phía ngoài, theo hướng di chuyển của xe
    int count = 0;
    Vector3 save;

    private void Update()
    {
        if (count == 0)
        {
            save = pointObjects[cur].transform.position;
            count++;
        }
        if (Vector3.Distance(transform.position, cornerObjects[cur].transform.position) <= Vector3.Distance(checkPoints[cur].transform.position, pointObjects[cur].transform.position))
        {
            Vector3 direction = pointObjects[cur].transform.position - transform.position;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1);
            transform.position += transform.forward * speed * Time.deltaTime;
            pointObjects[cur].transform.position = Vector3.MoveTowards(pointObjects[cur].transform.position, checkPoints[cur].transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, cornerObjects[cur].transform.position, speed * Time.deltaTime);
        }

        if (Vector3.Distance(checkPoints[cur].transform.position, pointObjects[cur].transform.position) == 0)
        {
            Debug.Log("alo");
            pointObjects[cur].transform.position = save;
            count--;
            cur = (cur + 1) % checkPoints.Length;
        }

    }
}