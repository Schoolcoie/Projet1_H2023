using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(Camera.main.transform, Vector3.up);

        gameObject.transform.forward = Camera.main.transform.forward;

        //transform.rotation = new Quaternion(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, SpriteRotation.w);
    }
}
