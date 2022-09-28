using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        // Game ends here
        print(collision.ToString());
        SceneManager.LoadScene("Scenes/GameOverScene");
    }
}
