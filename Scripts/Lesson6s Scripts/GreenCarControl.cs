using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GreenCarControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    [SerializeField]
    Transform gate;
    [SerializeField]
    [Range(1000, 5000)]
    float speed;
    [SerializeField]
    [Range(1200, 2000)]
    float turnSpeed;
    float turn, move;
    public static float damaged = 0;
    public static float fuel = 100;
    public static float capacity = 100;
    public static int laps = -1;
    bool getIt = false;

    int stuffLayerID;
    void Start()
    {
        stuffLayerID = LayerMask.NameToLayer("Hurt");

        rb = GetComponent<Rigidbody>();
        transform.position = new Vector3(gate.position.x, transform.position.y, gate.position.z * 5);
        transform.rotation = Quaternion.LookRotation(gate.position - transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        turn = Input.GetAxis("Horizontal");
        move = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            getIt = true;
        }
    }
    void FixedUpdate()
    {
        //them joint
        if (fuel >= 20)
        {
            rb.AddForce(transform.forward * speed * move);
            rb.AddTorque(transform.up * turn * turnSpeed);
        }

        if (getIt)
        {
            rb.AddForce(Vector3.up * 7.5f, ForceMode.VelocityChange);
            getIt = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == stuffLayerID)
        {
            if (damaged == 100)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            damaged += 5;
        }
    }
}
