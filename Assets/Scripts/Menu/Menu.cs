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
    [SerializeField] private TMP_Text titleLabel;
    [SerializeField] private TMP_Text titleLabel2;
    [SerializeField] private string title;
    [SerializeField] private string title2;

    [Header("Settings")]
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private TMP_Text sfxVolumeText;
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private TMP_Text bgmVolumeText;

    private void Awake()
    {
        playButton.onClick.AddListener(OnClickPlayButton);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeSliderValueChanged);
        bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeSliderValueChanged);
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnClickPlayButton);
        sfxVolumeSlider.onValueChanged.RemoveListener(OnSfxVolumeSliderValueChanged);
        bgmVolumeSlider.onValueChanged.RemoveListener(OnBgmVolumeSliderValueChanged);
    }

    private void Start()
    {
        sfxVolumeSlider.maxValue = 1;
        sfxVolumeSlider.minValue = 0;
        sfxVolumeSlider.value = AudioController.Instance.GetVolumeSfx();
        bgmVolumeSlider.maxValue = 1;
        bgmVolumeSlider.minValue = 0;
        bgmVolumeSlider.value = AudioController.Instance.GetVolumeBgm();

        var titleTween = DOTween.To(() => string.Empty, x => titleLabel.text = x, title, 2f);
        var title2Tween = DOTween.To(() => string.Empty, x => titleLabel2.text = x, title2, 2f);
        DOTween.Sequence()
            .Append(titleTween)
            .Append(title2Tween)
            .Play();
    }

    public void OnClickButton()
    {
        AudioController.Instance.PlaySelectButtonSFX();
    }

    private void OnClickPlayButton()
    {
        var sequence = DOTween.Sequence();
        var zoomTween = playButtonRectTransform.DOScale(2f, duration);
        var buttonColorTween = playButtonImage.DOColor(buttonColor, duration);
        var textColorTween = playButtonText.DOColor(textColor, duration);

        sequence
            .Append(zoomTween)
            .Join(buttonColorTween)
            .Join(textColorTween)
            //.SetLoops(-1, LoopType.Yoyo)
            .Play()
            .OnComplete(() => SceneManager.LoadScene(1))
            ;
    }

    private void OnSfxVolumeSliderValueChanged(float value)
    {
        AudioController.Instance.SetVolumeSfx(value);
        sfxVolumeText.text = $"SFX: {Mathf.Round(value * 100)}";
    }

    private void OnBgmVolumeSliderValueChanged(float value)
    {
        AudioController.Instance.SetVolumeBgm(value);
        bgmVolumeText.text = $"BGM: {Mathf.Round(value * 100)}";
    }

    //public void OnClickPlayButton(int sceneIndex)
    //{
    //    //SceneManager.LoadScene("MainScene"); // Load from scene name
    //    SceneManager.LoadScene(sceneIndex); // Load from index
    //    //SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive); // Load new scene but NOT unload current scene
    //}
}