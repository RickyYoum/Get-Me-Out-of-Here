using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    private bool isRight = true;
    private float horDirection;
    private Rigidbody2D rb;
    private bool jumping;

    public float jumpTime;
    private float jumpTimeCounter;
    public float jumpPower;
    public float speed;
    public float maxSpeed;
    public SpriteRenderer sprite;
    public Sprite oldSprite;
    public Sprite newSprite;
    public BoxCollider2D box1;
    public BoxCollider2D box2;
    public float ratio;
    private bool onGround;
    public float gravity;
    [HideInInspector]
    public bool canMove;
    public Vector2 currPos;
    //public Animator rat;
    public AudioSource jumpSource;
    public AudioSource walkSource;
    public AudioSource windSource;

    public AudioClip jumpSound;
    public AudioClip walkSound;
    public AudioClip windSound;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        oldSprite = sprite.sprite;
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horDirection * speed * 100 * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            /* Limit the toaster movement
            if (rb.position.magnitude > currPos.magnitude + 1)
            {
                canMove = true;
            }
            */
        }

    }

    private void Update()
    {
        // Menu Button
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

        // Ground check
        if (onGround)
        {
            canMove = true;
        }

        // Directions
        horDirection = Input.GetAxisRaw("Horizontal");

        
        if (canMove)
        {
            // Speed Limit
            if (onGround && rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
            }

            // Flip the sprite of the player when direction changes horizontally
            if (horDirection > 0 && !isRight)
            {
                Flip();
            }
            else if (horDirection < 0 && isRight)
            {
                Flip();
            }

            // Coyote Jump
            if (rb.velocity.y >= -10 && onGround && Input.GetKeyDown(KeyCode.W))
            {
                jumpTimeCounter = jumpTime;
                jumpSource.clip = jumpSound;
                jumpSource.Play();
                rb.velocity = Vector2.up * jumpPower;
                jumping = true;
            }

            // Jump done once player lets go 
            if (Input.GetKeyUp(KeyCode.W))
            {
                jumping = false;
            }

            // Long Jump
            if (jumping && Input.GetKey(KeyCode.W) && !onGround && rb.velocity.y > 0)
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

            // Parachute
            if (rb.velocity.y < 0 && Input.GetKey(KeyCode.J))
            {
                sprite.sprite = newSprite;
                //rat.SetTrigger("Parachute Wiggle");
                rb.gravityScale = .5f;
                windSource.clip = windSound;
                if (!windSource.isPlaying) {
                    
                    windSource.Play();
                }

            }
            else
            {
                //rat.SetFloat("Stop", 0f);
                //rat.SetFloat("Stop", 1f);
                sprite.sprite = oldSprite;
                rb.gravityScale = gravity;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Object" || collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Wall")
        {
            onGround = true;
            if (horDirection != 0 && !walkSource.isPlaying)
            {
                windSource.clip = walkSound;
                windSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Object" || collision.gameObject.tag == "Platform" || collision.gameObject.tag == "Wall")
        {
            onGround = false;
        }
    }

    private void Flip()
    {
        isRight = !isRight;
        sprite.flipX = !sprite.flipX;
        if (isRight)
        {
            box1.offset = new Vector2(box1.offset.x + ratio, box1.offset.y);
            box2.offset = new Vector2(box2.offset.x + ratio, box2.offset.y);
        }
        else
        {
            box1.offset = new Vector2(box1.offset.x - ratio, box1.offset.y);
            box2.offset = new Vector2(box2.offset.x - ratio, box2.offset.y);
        }

    }
}
