using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainGame : MonoBehaviour
{
    //this starts the audio as soon as the game loads. didn't feel like making another script
    public AudioClip mainTheme;
    void Start()
    {
        MusicManager.Instance.PlayMusic(mainTheme);
    }

    public void LoadNextScene()
    {
        Debug.Log("load scene");
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentIndex + 1);
    }
}
