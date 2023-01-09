using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    private float jumpTimeCounter;
    private bool jumping;
    private bool isRight = true;
    private float direction;

    [HideInInspector]
    public bool onGround;
    [HideInInspector]
    public bool canMove = true;
    public Rigidbody2D rb;
    public float jumpPower;
    public float speed;
    //public Transform playerGround;
    public float checkRadius;
    //public LayerMask groundLayer;
    public float jumpTime;
    //public Animator moving;
    //public bool hasParachute = false;
    public float maxSpeed;
    
    //public Vector2 currPos;
    public SpriteRenderer sprite;
    public Sprite oldSprite;
    public Sprite newSprite;
    public BoxCollider2D box;
    public float ratio;

    void Start()
    {
        oldSprite = sprite.sprite;
    }
    void FixedUpdate()
    {
        //Debug.Log(onGround);
        direction = Input.GetAxisRaw("Horizontal");
        if (canMove)
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        }
        else
        {
            //if rb.position.magnitude > currPos.magnitude + 1
            canMove = true;
            
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
        if (rb.velocity.y == 0)
        {
            onGround = true;
        }
        //onGround = Physics2D.OverlapCircle(playerGround.position, checkRadius, groundLayer);
        if (canMove)
        {
            
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            }
            if (direction > 0 && !isRight )
            {
                Flip();
            }
            else if (direction < 0 && isRight)
            {
                Flip();
            }
            if (rb.velocity.y >= -10 && onGround && Input.GetKeyDown(KeyCode.J))
            {
                onGround = false;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpPower;
                //moving.SetTrigger("Jump");
            }
            /*
            if (Input.GetKeyUp(KeyCode.Space))
            {
                jumping = false;
            }

            
            if (Input.GetKey(KeyCode.Space) && jumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpPower;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    jumping = false;
                }
            }
            */
            if (rb.velocity.y < 0 && Input.GetKey(KeyCode.K))
            {
                sprite.sprite = newSprite;
                //moving.SetTrigger("Parachute Wiggle");
                rb.gravityScale = 0.5f;
            }
            else
            {
                //moving.SetFloat("Stop", 0f);
                //moving.SetFloat("Stop", 1f);

                sprite.sprite = oldSprite;
                rb.gravityScale = 5f;
            }
        }

    }
    private void Flip()
    {
        isRight = !isRight;
        sprite.flipX = !sprite.flipX;
        if (isRight)
        {
            box.offset = new Vector2(box.offset.x + ratio, box.offset.y);
        }
        else
        {
            box.offset = new Vector2(box.offset.x - ratio, box.offset.y);
        }

    }
}
