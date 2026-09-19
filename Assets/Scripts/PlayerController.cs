using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{   
    //Rotation variables
    [SerializeField] float torqueAmmount = 1f;
    //Jump variables
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float invalidateJumpDelay = 0.2f;
    bool jump =false;
    //Speed variables
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 30f;
    //Input actions
    InputAction moveAction;
    InputAction jumpAction;
    Vector2 moveVector;
    SurfaceEffector2D surfaceEffector2D;
    Rigidbody2D rigidBody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rigidBody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindAnyObjectByType<SurfaceEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector =  moveAction.ReadValue<Vector2>();
        rotatePlayer();
        jumpPlayer();
        boostPlayer();
    }
    void rotatePlayer()
    {
        if (moveVector.x < 0)
        {
            rigidBody2D.AddTorque(torqueAmmount);
        }
        if (moveVector.x > 0)
        {
            rigidBody2D.AddTorque(-torqueAmmount);
        }
    }

    #region Jump Player
    void jumpPlayer()
    {
        if (jumpAction.WasPressedThisFrame() && jump)
        {
            rigidBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jump = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (other.gameObject.layer == layerIndex)
        {
            jump=true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        int layerIndex = LayerMask.NameToLayer("Floor");
        if (other.gameObject.layer == layerIndex)
        {
            Invoke("invalidateJump",invalidateJumpDelay);
        }
    }

    void invalidateJump()
    {
        jump=false;
    }
    #endregion

    void boostPlayer()
    {
        if (moveVector.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        } else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

}
