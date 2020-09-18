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
    Renderer rend;

    Vector2 touchStartPos;
    Vector2 touchEndPos;

    float distanceFromCamera = 20;

    [SerializeField]CustomColor playerColor;

    Camera mainCamera;

    Gamestate currentGameState;

    private void OnEnable()
    {
        CustomEvents.OnChangeGameState += CustomEvents_OnChangeGameState;
        CustomEvents.OnChangeColorScheme += CustomEvents_OnChangeColorScheme;

    }

    private void CustomEvents_OnChangeColorScheme(CustomColor color)
    {
        SetColor(color);
    }

    private void CustomEvents_OnChangeGameState(Gamestate state)
    {
        currentGameState = state;
    }

    private void OnDisable()
    {
        CustomEvents.OnChangeGameState -= CustomEvents_OnChangeGameState;
        CustomEvents.OnChangeColorScheme -= CustomEvents_OnChangeColorScheme;
    }

   



        // Start is called before the first frame update
     void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        rend = GetComponentInChildren<Renderer>();

        int r  = Random.Range(1,4);
        CustomColor tempColor = r == 1 ? CustomColor.Red : r == 2 ? CustomColor.Green : CustomColor.Yellow;
        SetColor(tempColor);
    }

    void Update() {

        if (currentGameState != Gamestate.Game)
        {
            return;
        }

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


        if (Input.GetKeyDown(KeyCode.A))
        {
            SwipeLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SwipeRight();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
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
                //Debug.Log("Swipe right");
            }
            else
            { 
                SwipeLeft();
                //Debug.Log("Swipe left");
            }
        }
        else
        {
            if (Mathf.Abs(deltaY) < minSwipeDistance)
            {
                return;
            }
            Jump();
           // Debug.Log("jump");
        }


    }


    void SwipeLeft() {

        //bool isOnGround = CheckGround();
        //if (!isOnGround)
        //{
        //    return;
        //}
        float targetPosX = transform.position.x - moveXDistance;
        if (targetPosX <= minXPos)
        {
            targetPosX = minXPos;
        }
        transform.position = new Vector3(targetPosX,transform.position.y,transform.position.z);
    }

    void SwipeRight() {

        //bool isOnGround = CheckGround();
        //if (!isOnGround)
        //{
        //    return;
        //}
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
           // Debug.Log("adding jumping force");
        }
    }

    bool CheckGround()
    {
        Collider[] colliders = Physics.OverlapSphere(GroundCheckTransform.position, groundCheckRadius, groundLayerMask);
        bool isOnGround = (colliders.Length > 0) ? true : false;
        if (isOnGround)
        {
            rb.velocity = Vector3.zero;
        }
        return isOnGround;
    }

    private void SetColor(CustomColor color) {
        Color baseColor = Color.white;
        switch (color)
        {
            case CustomColor.Red:
                baseColor = Color.red;
                break;
            case CustomColor.Green:
                baseColor = Color.green;
                break;
            case CustomColor.Yellow:
                baseColor = Color.yellow;
                break;
            default:
                break;
        }

        playerColor = color;
        rend.material.SetColor("_BaseColor", baseColor);
       

    }

    public CustomColor GetColor() {

        return playerColor;
    }

    public void Playerdead() {

        GameController.Instance.sceneController.clearedLevel = false;
        CustomEvents.GameStateChanged(Gamestate.GameOver);
        //gameObject.SetActive(false);
    }
}
