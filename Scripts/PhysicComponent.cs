using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicComponent : MonoBehaviour
{
    Rigidbody dragHere;
    // Start is called before the first frame update
    void Start()
    {
        dragHere = GetComponent<Rigidbody>();
        Debug.Log(dragHere.mass);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
