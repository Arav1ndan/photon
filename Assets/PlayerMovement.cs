using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;
    private CharacterController character;
    public float PlayerSpeed = 5f;
    public float JumpForce = 7f;
    public float GravityValue = -9.81f;
    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }
     void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }
    public override void FixedUpdateNetwork()
    {
        if(HasStateAuthority == false)
        {
            return;
        }
        if (character.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"),0 ,Input.GetAxis("Vertical")) * Runner.DeltaTime * PlayerSpeed;
        
        _velocity.y += GravityValue * Runner.DeltaTime;
        if (_jumpPressed && character.isGrounded)
        {
            _velocity.y += JumpForce;
        }
        character.Move(move + _velocity * Runner.DeltaTime);
        if(move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        _jumpPressed = false;
    }
}
