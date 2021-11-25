using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walk : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    public GameObject camcam;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camcam = GameObject.Find("Main Camera");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            StartWalk();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.eulerAngles += new Vector3(0,15,0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.eulerAngles += new Vector3(0,-15,0);
        }
    }

    // Update is called once per frame
    public void StartWalk()
    {
        //print(camcam.transform.forward);
        rb.velocity = new Vector3(camcam.transform.forward.x,0,camcam.transform.forward.z)*10;


    }

    
}
