using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
   
    private enum DraggedDirection{Up, Down, Right, Left }

    [SerializeField] Transform GroundCheckTransform;
    [SerializeField] LayerMask groundLayerMask;
    [SerializeField] float jumpForce;
    [SerializeField] float minXPos;
    [SerializeField] float maxXPos;
    [SerializeField] float moveXDistance; //distance to move when swipped in xdirection
    [SerializeField] float forwardSpeed;
    [SerializeField] float minSwipeDistance;

    float groundCheckRadius =0.4f;
    Rigidbody rb;

    Vector2 touchStartPos;
    Vector2 touchEndPos;

    float distanceFromCamera = 20;

    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    void Update() {

        transform.Translate(0,0,forwardSpeed*Time.deltaTime);


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera);
            touchStartPos = mainCamera.ScreenToWorldPoint(mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera);
            touchEndPos = mainCamera.ScreenToWorldPoint(mousePosition);

            CheckSwipeDirection();
        }
    }

    
    
   

    void CheckSwipeDirection() {

        float deltaX = touchStartPos.x - touchEndPos.x;
        float deltaY = touchStartPos.y - touchEndPos.y;

        float positiveX = Mathf.Abs(deltaX);
        float positiveY = Mathf.Abs(deltaY);

        if (positiveX > positiveY)
        {
            if (Mathf.Abs(deltaX) < minSwipeDistance)
            {
                return;
            }
            if (deltaX < 0)
            {
                SwipeRight();
                Debug.Log("Swipe right");
            }
            else
            { 
                SwipeLeft();
                Debug.Log("Swipe left");
            }
        }
        else
        {
            if (Mathf.Abs(deltaY) < minSwipeDistance)
            {
                return;
            }
            Jump();
            Debug.Log("jump");
        }

    }


    void SwipeLeft() {

        bool isOnGround = CheckGround();
        if (!isOnGround)
        {
            return;
        }
        float targetPosX = transform.position.x - moveXDistance;
        if (targetPosX <= minXPos)
        {
            targetPosX = minXPos;
        }
        transform.position = new Vector3(targetPosX,transform.position.y,transform.position.z);
    }

    void SwipeRight() {

        bool isOnGround = CheckGround();
        if (!isOnGround)
        {
            return;
        }
        float targetPosX = transform.position.x + moveXDistance;
        if (targetPosX >= maxXPos)
        {
            targetPosX = maxXPos;
        }
        transform.position = new Vector3(targetPosX, transform.position.y, transform.position.z);

    }

    void Jump() {

        bool isOnGround = CheckGround();
        if (isOnGround)
        {
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            Debug.Log("adding jumping force");
        }
    }

    bool CheckGround()
    {
        Collider[] colliders = Physics.OverlapSphere(GroundCheckTransform.position, groundCheckRadius, groundLayerMask);
        bool isOnGround = (colliders.Length > 0) ? true : false;
        return isOnGround;
    }
}
