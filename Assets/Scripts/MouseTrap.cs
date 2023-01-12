using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseTrap : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("MouseIsDead");
        }

    }
}
