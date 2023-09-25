using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashMovement : MonoBehaviour
{
    public int hitForce;
    Rigidbody objectRb;
    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objectRb.AddForce(new Vector3(2, 3, 0) * hitForce, ForceMode.Impulse);
        }
    }
}
