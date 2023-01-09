using UnityEngine;
using UnityEngine.SceneManagement;

public class Downstairs : MonoBehaviour
{
    public void LoadDownLevell()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
