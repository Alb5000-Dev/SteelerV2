using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region Singleton
    public static PlayerMove instance;
    #endregion
    [SerializeField] private string horizontalInputName, verticalInputName;
    public float movementSpeed;

    private Rigidbody charRB;

    public float walkingSpeed;
    private float oldWalkingSpeed;
    //public float runningSpeed;
    [SerializeField] private float movementBuildUp;
    //public bool grounded;
    Vector3 moveDir;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        oldWalkingSpeed = walkingSpeed;
        charRB = GetComponent<Rigidbody>();
    }

    void Update()
    {     
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        
        moveDir = new Vector3(horizInput , charRB.velocity.y, vertInput);
        moveDir = transform.TransformDirection(moveDir);
        
        charRB.velocity = Vector3.Lerp(charRB.velocity, moveDir, Time.fixedDeltaTime * 20f); ;
        SetMovementSpeed();
    }

    private void SetMovementSpeed()
    {
        /*if (Input.GetKey(KeyCode.LeftShift) && movementSpeed!= 0) {
            movementSpeed = Mathf.Lerp(movementSpeed, runningSpeed, movementBuildUp);
        }*/
        
        if (Input.GetAxisRaw(horizontalInputName) == 0 && Input.GetAxisRaw(verticalInputName) == 0) {
            movementSpeed = 0f;
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkingSpeed, movementBuildUp); ;
        }
    }

    public void ChangeSpeed(float value) {
        walkingSpeed = value;
    }

    public void ResetSpeed() {
        walkingSpeed = oldWalkingSpeed;
    }
}
