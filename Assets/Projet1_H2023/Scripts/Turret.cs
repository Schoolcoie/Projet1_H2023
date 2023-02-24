using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    private Quaternion IdleRotation;
    // Start is called before the first frame update
    void Start()
    {
        IdleRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - Target.transform.position).magnitude < 4)
        {
            gameObject.transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
        }
        else
        {
            transform.rotation = IdleRotation;
        }
        
    }
}
