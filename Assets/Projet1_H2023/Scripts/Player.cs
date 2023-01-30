using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D PlayerBody;
    private float PlayerSpeed = 5.0f;
    private Vector2 midPoint;
    private Vector3 midPoint_as_V3;
    private float HalfScreenWidth = Screen.width / 2;
    private float HalfScreenHeight = Screen.height / 2;
    [SerializeField] private float CameraScrollDistance = 50;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float VerticalMovement = Input.GetAxis("Vertical");
        PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, VerticalMovement * PlayerSpeed);

        float HorizontalMovement = Input.GetAxis("Horizontal");
        PlayerBody.velocity = new Vector3(HorizontalMovement * PlayerSpeed, PlayerBody.velocity.y);

        Vector2 midPoint = (Input.mousePosition - transform.position);
        Vector3 midPoint_as_V3 = new Vector3(midPoint.x, midPoint.y, -10);
        Camera.main.transform.position = transform.position + new Vector3(midPoint_as_V3.x / HalfScreenWidth * CameraScrollDistance - CameraScrollDistance, midPoint_as_V3.y / HalfScreenHeight * CameraScrollDistance - CameraScrollDistance, midPoint_as_V3.z);

        float DashTimer = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up, DashTimer);
        }
    }
}
