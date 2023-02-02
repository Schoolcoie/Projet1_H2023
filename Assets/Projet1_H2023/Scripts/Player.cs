using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D PlayerBody;
    [SerializeField] private float PlayerSpeed = 5.0f;
    private Vector2 midPoint;
    private Vector3 midPoint_as_V3;
    private float HalfScreenWidth = Screen.width / 2;
    private float HalfScreenHeight = Screen.height / 2;
    [SerializeField] private float CameraScrollDistance = 50;

    [SerializeField] private GameObject BulletPrefab;


    private Vector2 AttackDirection;

    //Cursor Settings
    [SerializeField] private Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        //Cursor init
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        Cursor.lockState = CursorLockMode.Confined;

        PlayerBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Movement();
        PlayerCamera();

        AttackDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        print(AttackDirection);

        Quaternion BulletRotation = Quaternion.LookRotation(AttackDirection, Vector3.up);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(BulletPrefab, transform.position, BulletRotation);
        }
        
        



    }

    private void Movement()
    {
        //Directional Movement
        float VerticalMovement = Input.GetAxis("Vertical");
        PlayerBody.velocity = new Vector3(PlayerBody.velocity.x, VerticalMovement * PlayerSpeed);

        float HorizontalMovement = Input.GetAxis("Horizontal");
        PlayerBody.velocity = new Vector3(HorizontalMovement * PlayerSpeed, PlayerBody.velocity.y);


        //Dash
        float DashTimer = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position = Vector2.Lerp(transform.position, transform.position + Vector3.up, DashTimer);
        }
    }

    private void PlayerCamera()
    {
        midPoint = (Input.mousePosition - transform.position);
        midPoint_as_V3 = new Vector3(midPoint.x, midPoint.y, -10);
        Camera.main.transform.position = transform.position + new Vector3(midPoint_as_V3.x / HalfScreenWidth * CameraScrollDistance - CameraScrollDistance, midPoint_as_V3.y / HalfScreenHeight * CameraScrollDistance - CameraScrollDistance, midPoint_as_V3.z);
    }
}
