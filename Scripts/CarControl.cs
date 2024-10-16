using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEditor;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }
    [SerializeField]
    [Range(10,50)]
    float speed;
    float move;
    float t = 0;
    Vector3 mouse;
    // Update is called once per frame
    void Update()
    {
        mouse = new Vector3(Input.mousePosition.x + t, transform.position.y, 300);
        if (Input.mousePosition.x <= 0)
        {
            t--;
        }
        else if (Input.mousePosition.x >= Screen.width)
        {
            t++;
        }
        else
        {
            t = 0;
        }
        move = Input.GetAxis("Vertical");
        transform.rotation = Quaternion.LookRotation(mouse - transform.position);
        transform.Translate(0, 0, move * speed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogWarning(mouse.ToString() + " t: " + t);
        }
    }
}