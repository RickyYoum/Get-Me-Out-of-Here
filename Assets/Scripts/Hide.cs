using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("Hide");
            player.layer = 6;
            player.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //player.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            Debug.Log("Reveal");
            player.layer = 7;
            player.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Reveal");
        player.layer = 7;
        player.transform.GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
