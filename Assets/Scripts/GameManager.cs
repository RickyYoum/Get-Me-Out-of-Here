using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public float restartDelay = 1f;
    public GameObject UI;
    //public Animator transition;

    public void CompleteLevel()
    {
        UI.SetActive(true);
    }
    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        if (gameHasEnded)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //transition.SetTrigger("transition");
        }
    }

    public void goIn()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //transition.SetTrigger("transition");
    }
}
