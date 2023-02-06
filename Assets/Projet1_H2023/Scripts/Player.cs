using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody PlayerBody;
    [SerializeField] private float PlayerSpeed = 5.0f;
    private Vector2 midPoint;
    private Vector3 midPoint_as_V3;
    private float HalfScreenWidth = Screen.width / 2;
    private float HalfScreenHeight = Screen.height / 2;
    [SerializeField] private float CameraScrollDistance = 50;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private GameObject BulletPrefab;

    [SerializeField] private Camera logicCamera;


    private Vector3 AttackDirection;

    //Cursor Settings
    [SerializeField] private Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        //Cursor init
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        //Cursor.lockState = CursorLockMode.Confined;

        PlayerBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        PlayerCamera();

        AttackDirection = (logicCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1.0f)) - transform.position);


        Quaternion BulletRotation = Quaternion.LookRotation(new Vector3(AttackDirection.x, 0, AttackDirection.z), Vector3.up);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(BulletPrefab, transform.position, BulletRotation);
        }
        
        



    }

    private void Movement()
    {
        //Directional Movement
        float VerticalMovement = Input.GetAxis("Vertical");
        float HorizontalMovement = Input.GetAxis("Horizontal");
        PlayerBody.velocity = (Camera.main.transform.forward * VerticalMovement * PlayerSpeed) + (Camera.main.transform.right * HorizontalMovement * PlayerSpeed);






        if (PlayerBody.velocity.x != 0 || PlayerBody.velocity.z != 0)
        {
            playerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }


        //Dash
        float DashTimer = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.position = Vector2.Lerp(transform.position, transform.position - Vector3.up, DashTimer);
        }
    }

    private void PlayerCamera()
    {
        midPoint = (Input.mousePosition - transform.position);
        midPoint_as_V3 = new Vector3(midPoint.x, 3.5f, midPoint.y);
       // Camera.main.transform.position = transform.position + new Vector3(midPoint_as_V3.x / HalfScreenWidth * CameraScrollDistance - CameraScrollDistance, midPoint_as_V3.y, midPoint_as_V3.z / HalfScreenHeight * CameraScrollDistance - CameraScrollDistance);
        logicCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 6, transform.position.z);
        //Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z -3.5f);
    }
}
