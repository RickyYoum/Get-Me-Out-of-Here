using System.Collections;
using UnityEngine;

public class Toaster : MonoBehaviour
{
    [SerializeField]
    public float nextSceneDelay = 1f;
    public float pushForce = 5f;
    public float x;
    public float y;
    private Rigidbody2D rb;
    public AudioClip toaster;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.GetComponent<playerMovement>().canMove = false;
            GameObject.Find("Player").GetComponent<Transform>().position = gameObject.transform.position;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            rb.gameObject.GetComponent<playerMovement>().currPos = rb.position;
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Fire(collision));
        }
    }

    private IEnumerator Fire(Collider2D collision)
    {
        yield return new WaitForSeconds(.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.freezeRotation = true;
        collision.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        rb.AddForce(new Vector2(x * pushForce, y * pushForce), ForceMode2D.Impulse);
        GameObject.Find("Player").GetComponent<AudioSource>().clip = toaster;
        GameObject.Find("Player").GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(.5f);
        rb.GetComponent<playerMovement>().canMove = true;
    }



}

