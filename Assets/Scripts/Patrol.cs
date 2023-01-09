using UnityEngine;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{

    public float speed;
    public float distance = 2f;

    public bool movingRight = true;

    public Transform PlayerDetection;

    public Transform groundDetection;

    public Vector2 Direction = Vector2.right;

    private bool timerOn = false;
    public float maxDuration = 1f;
    float timer = 0f;

    private bool canMove = true;

    public float radiusAttack = 10f;


    static int layerMask;


    private void OnEnable()
    {
        layerMask = ~(LayerMask.NameToLayer("PlayerDetection"));
        Debug.Log(layerMask);
    }

    private void Awake()
    {
    
    }
    private void Start()
    {
        timer = maxDuration;

    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }


        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);

        if (groundInfo.collider == false || groundInfo.collider.tag == "Wall")
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                Direction = Vector2.left;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                Direction = Vector2.right;
            }
        }


        if (timerOn)
        {
            timer -= 1f * Time.deltaTime;
            if (timer < 0)
            {
                timerOn = false;
                canMove = false;
                transform.Translate(Vector2.up * speed * Time.deltaTime);
                
            }
        }



    }

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            RaycastHit2D playerInfo = Physics2D.Raycast(PlayerDetection.position, col.transform.position - PlayerDetection.position, radiusAttack, layerMask);
            
            if (playerInfo && playerInfo.collider.tag == "Player")
            {
                Debug.Log("player is there");
                canMove = false;
                timerOn = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        canMove = true;
        timer = maxDuration;
        timerOn = false;

    }


}
