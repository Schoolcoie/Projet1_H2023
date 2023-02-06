using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField] private Transform CamTransform;

    Quaternion SpriteRotation;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(Camera.main.transform.rotation.x, Camera.main.transform.rotation.y, Camera.main.transform.rotation.z, SpriteRotation.w);
    }
}
