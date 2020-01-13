using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Scenes/Dev");
    }
}
