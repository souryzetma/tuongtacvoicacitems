using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform followedObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = followedObject.position + Vector3.up * 5 + followedObject.rotation * Vector3.back * 5;
        transform.rotation = Quaternion.LookRotation(followedObject.position - transform.position);
    }
}
