using Game.Interface;
using UnityEngine;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour, IMovement
    {       
            
            [SerializeField]  private Animator animator;
            public float MoveSpeed { get; set; } = 5f;
            public float jumpForce = 10f;
            public float groundCheckRadius = 0.1f;
            private bool _isColliding = false;
            private bool _isGrounded = false;
            private bool _isCanJump = true; 
             

            public bool IsColliding => _isColliding;
            public Transform groundCheck;
            public LayerMask groundLayer;
            private Rigidbody2D _rb;
             private void Start()
             {
                 _rb = GetComponent<Rigidbody2D>();
            }

            private void Update()
            {
                _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
                     
                if (_isGrounded && _isCanJump && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Jump();
                }
                if (!_isColliding)
                {
                    var transform1 = transform;
                    Vector3 newPosition = transform1.position + new Vector3(MoveSpeed * Time.deltaTime, 0f, 0f);
                    transform1.position = newPosition;
                
                
                    animator.SetFloat("Run", MoveSpeed);
                }
                else
                {
                    MoveSpeed = 0f;
                    animator.SetFloat("Run", MoveSpeed);
                    
                }
            }
             private void Jump()
           {
               
              //  _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
                _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
           }

            private void OnTriggerEnter2D(Collider2D collision)
            {
                if (collision.CompareTag("Enemy"))
                {
                    _isColliding = true;
                    MoveSpeed = 0f;
                    _isCanJump = false;
                    
                }

                if (!collision.CompareTag("Boss")) return;
                _isColliding = true;
                MoveSpeed = 0f;
                _isCanJump = false;
            }

            private void OnTriggerExit2D(Collider2D collision)
            {
                if (collision.CompareTag("Enemy"))
                {
                    _isColliding = false;
                    MoveSpeed = 5f;
                   _isCanJump = true;
                   
                }

                if (!collision.CompareTag("Boss")) return;
                _isColliding = false;
                MoveSpeed = 5f;
                _isCanJump = true;
            }
        

    }
}


