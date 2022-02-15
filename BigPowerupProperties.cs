using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPowerupProperties : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            transform.position =  new Vector3(0.0f, 0.2f, 2.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = new Vector3(0.0f, -10.0f, 0.0f);
    }
}
