using UnityEngine;

public class agroCat : MonoBehaviour
{

    [SerializeField]
    Transform player;

    [SerializeField]
    Transform cat;

    [SerializeField]
    float agroRange;

    [SerializeField]
    float fastMoveSpeed;

    [SerializeField]
    float slowMoveSpeed;

    [SerializeField]
    Vector3 initPosition;

    [SerializeField]
    Vector3 Direction = Vector3.right;

    [SerializeField]
    public Transform AgroRange;

    [SerializeField]
    bool facingRight;

    private bool initDirection;

    static int layerMask;

    private float initRange;

    Rigidbody2D rb2d;
    private void OnEnable()
    {
        layerMask = ~(LayerMask.NameToLayer("PlayerDetection"));

    }
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        initPosition = transform.position;
        initDirection = facingRight;
        initRange = agroRange;

        if (facingRight)
        {
            Direction = Vector3.right;
        }
        else
        {
            Direction = Vector3.left;
        }
        Debug.Log(initPosition);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canSeeRat(agroRange))
        {
            ChaseRat();

        }
        else
        {
            ReturnCat();

        }

    }

    bool canSeeRat(float agroRange)
    {
        bool val = false;
        var lineDist = agroRange;




        Vector2 endPos = cat.position + Direction * lineDist;
        RaycastHit2D agroInfo = Physics2D.Linecast(AgroRange.transform.position, endPos, layerMask);
        Debug.DrawLine(AgroRange.position, endPos, Color.blue);

        if (agroInfo)
        {

            if (agroInfo.collider.gameObject.CompareTag("Player"))
            {
                val = true;
            }
            else
            {
                val = false;
            }
        }
        return val;
    }
    void ChaseRat()
    {
        if (cat.transform.position.x < player.transform.position.x)
        {
            rb2d.velocity = new Vector2(fastMoveSpeed, 0);
            cat.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(-fastMoveSpeed, 0);
            cat.transform.localScale = new Vector2(-1, 1);
        }




    }

    void ReturnCat()
    {

        if (Mathf.Round(cat.transform.position.x) != Mathf.Round(initPosition.x))
        {
            agroRange = 0f;
            cat.transform.position += (initPosition - cat.transform.position) * slowMoveSpeed * Time.deltaTime;
            Debug.Log(cat.transform.position.x);
            Debug.Log(initPosition.x);

        }
        if (Mathf.Round(cat.transform.position.x) == Mathf.Round(initPosition.x))
        {
            agroRange = initRange;

        }




    }
}
