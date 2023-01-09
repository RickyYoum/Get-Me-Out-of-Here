using UnityEngine;

public class Trigger : MonoBehaviour
{
    public float nextSceneDelay = 1f;
    private bool canGoIn = true;
    public Animator transition;

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKey(KeyCode.K) && canGoIn)
        {
            canGoIn = false;
            FindObjectOfType<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            //transition.SetTrigger("transition");
            FindObjectOfType<GameManager>().Invoke("goIn", nextSceneDelay);

        }
    }
}
