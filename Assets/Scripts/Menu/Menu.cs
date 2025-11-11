using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private RectTransform playButtonRectTransform;
    [SerializeField] private Image playButtonImage;
    [SerializeField] private TMP_Text playButtonText;
    [SerializeField] private Color buttonColor;
    [SerializeField] private Color textColor;
    [SerializeField] private float duration;

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnClickPlayButton);
    }

    public void OnClickButton()
    {
        AudioController.Instance.PlaySelectButtonSFX();
    }

    private void OnClickPlayButton()
    {
        //SceneManager.LoadScene(1);
        var sequence = DOTween.Sequence();

        var zoomTween = playButtonRectTransform.DOScale(2f, duration);
        var buttonColorTween = playButtonImage.DOColor(buttonColor, duration);
        var textColorTween = playButtonText.DOColor(textColor, duration);

        sequence
            .Append(zoomTween)
            .Join(buttonColorTween)
            .Join(textColorTween)
            .SetLoops(-1, LoopType.Yoyo)
            .Play()
            ;
    }

    //public void OnClickPlayButton(int sceneIndex)
    //{
    //    //SceneManager.LoadScene("MainScene"); // Load from scene name
    //    SceneManager.LoadScene(sceneIndex); // Load from index
    //    //SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive); // Load new scene but NOT unload current scene
    //}
}