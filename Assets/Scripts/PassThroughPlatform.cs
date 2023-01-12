using System.Collections;
using UnityEngine;

public class PassThroughPlatform : MonoBehaviour
{
    private Collider2D coll;
    private bool playeroOnPlatform;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (playeroOnPlatform && Input.GetKeyDown(KeyCode.S))
        {
            coll.enabled = false;
            StartCoroutine(EnableCollider());
        }
    }

    private IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(.5f);
        coll.enabled = true;
    }

    private void SetPlayerOnPlatform(Collision2D other, bool value)
    {
        var player = other.gameObject.GetComponent<playerMovement>();
        if (player != null)
        {
            playeroOnPlatform = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SetPlayerOnPlatform(other, true);
    }
}
