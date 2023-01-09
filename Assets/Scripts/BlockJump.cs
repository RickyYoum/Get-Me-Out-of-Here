using UnityEngine;

public class BlockJump : MonoBehaviour
{
    public Collider2D block_go;


    private void OnTriggerStay2D(Collider2D collision)
    {


        if (FindObjectOfType<Rigidbody2D>().velocity.y > 0)
        {
            //Debug.Log("Go");
            block_go.enabled = false;
        }

        else
        {

            if (Input.GetKey(KeyCode.S))
            {
                //Debug.Log("Down");
                block_go.enabled = false;
            }
            else
            {
                //Debug.Log("Block");
                block_go.enabled = true;
            }
        }
    }
}
