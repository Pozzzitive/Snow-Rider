using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmmount = 1f;
    InputAction moveAction;
    Rigidbody2D rigidBody2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveVector;
        moveVector =  moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            rigidBody2D.AddTorque(torqueAmmount);
        }
        if (moveVector.x > 0)
        {
            rigidBody2D.AddTorque(-torqueAmmount);
        }
    }
}
