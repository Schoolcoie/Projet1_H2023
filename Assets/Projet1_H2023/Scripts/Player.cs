using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Speed = 5.0f;
    private Vector2 midPoint;
    private Vector3 midPoint_as_V3;
    private float HalfScreenWidth = Screen.width / 2;
    private float HalfScreenHeight = Screen.height / 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 midPoint = (Input.mousePosition - transform.position);
        Vector3 midPoint_as_V3 = new Vector3(midPoint.x, midPoint.y, -10);
        Camera.main.transform.position = transform.position + new Vector3(midPoint_as_V3.x / HalfScreenWidth - 1, midPoint_as_V3.y / HalfScreenHeight - 1, midPoint_as_V3.z);

        

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, Speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -Speed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(Speed * Time.deltaTime, 0 , 0);
        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(Screen.width);
            Debug.Log(Screen.height);
        }
    }
}
