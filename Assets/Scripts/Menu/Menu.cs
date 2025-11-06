using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void OnClickButton()
    {
        AudioController.Instance.PlaySelectButtonSFX();
    }

    public void OnClickPlayButton(int sceneIndex)
    {
        //SceneManager.LoadScene("MainScene"); // Load from scene name
        SceneManager.LoadScene(sceneIndex); // Load from index
        //SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive); // Load new scene but NOT unload current scene
    }
}