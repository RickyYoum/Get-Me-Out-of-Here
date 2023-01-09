using System.Collections;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    [SerializeField]
    public float nextSceneDelay = 1f;
    [SerializeField]
    public float pushDelay = 0.1f;
    [SerializeField]
    public Rigidbody2D rb;
    public float pushForce = 5f;
    public float x;
    public float y;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.GetComponent<playerMovement>().canMove = false;
            rb.velocity = Vector2.zero;
            //rb.gameObject.GetComponent<playerMovement>().currPos = rb.position;
            rb.AddForce(new Vector2(x * pushForce, y * pushForce), ForceMode2D.Impulse);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}

