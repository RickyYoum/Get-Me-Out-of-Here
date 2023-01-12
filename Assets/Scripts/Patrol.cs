using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Patrol : MonoBehaviour
{
    private bool timerOn = false;
    public float speed;
    private float distance = 2f;
    public bool movingRight = true;
    public Transform PlayerDetection;
    public Transform groundDetection;
    public Vector2 Direction = Vector2.right;
    public float maxDuration = 1f;
    float timer = 0f;
    [SerializeField] AudioSource audioSourceCat;
    [SerializeField] AudioSource audioSourceRat;
    [SerializeField] AudioClip catSound;
    [SerializeField] AudioClip deadSound;


    private bool canMove = true;

    public float radiusAttack = 10f;

    static int layerMask;


    private void OnEnable()
    {
        layerMask = ~(LayerMask.NameToLayer("PlayerDetection"));
    }

    private void Start()
    {
        timer = maxDuration;
        if (audioSourceRat != null && audioSourceCat!= null)
        {
            audioSourceCat.clip = catSound;
            audioSourceRat.clip = deadSound;
        }

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the collider is player
        if (col.tag == "Player" && col.gameObject.layer == 7)
        {
            RaycastHit2D playerInfo = Physics2D.Raycast(PlayerDetection.position, col.transform.position - PlayerDetection.position, radiusAttack, layerMask);

            if (playerInfo && playerInfo.collider.tag == "Player")
            {
                // Since the children objects also call this script, we need to check if this is the parent object
                if (transform.childCount != 0)
                {
                    transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                
                // Death 
                StartCoroutine(Dead(col));

            }
        }
        // Turn off the colliders when cat meets another cat
        else if (col.tag == "Cat")
        {
            col.GetComponent<CircleCollider2D>().enabled = false;
            col.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        
        canMove = true;
        timer = maxDuration;
        timerOn = false;
        if (col.tag == "Cat")
            col.GetComponent<CircleCollider2D>().enabled = true;
    }

    private IEnumerator Dead(Collider2D col)
    {
        // Stop cat from moving on its own
        canMove = false;

        // Play Audios
        if (audioSourceCat != null && !audioSourceCat.isPlaying){
            audioSourceCat.Play();
        }
        if (audioSourceRat != null && !audioSourceRat.isPlaying)
        {
            audioSourceRat.Play();
        }
        
        yield return new WaitForSeconds(.5f);

        // Make Cat position the same as the player
        transform.position = col.transform.position;

        yield return new WaitForSeconds(.3f);

        // Flip the rat position
        col.gameObject.GetComponent<SpriteRenderer>().flipY = true;

        yield return new WaitForSeconds(1f);
        timerOn = true;

        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
