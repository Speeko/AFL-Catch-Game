using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rotator : MonoBehaviour
{

    public float minRotateSpeed;
    public float maxRotateSpeed;
    private float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
    }
}
