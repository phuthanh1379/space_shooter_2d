using DG.Tweening;
using TMPro;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    [SerializeField] private string dialogueContent;
    [SerializeField] private TMP_Text dialogueLabel;
    [SerializeField] private float dialogueDuration;
    [SerializeField] private int number;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Show();
        }
    }

    private void Show()
    {
        DOTween.Kill(this);
        DOTween.To(() => string.Empty, x => dialogueLabel.text = x, dialogueContent, dialogueDuration).Play().SetTarget(this);
        DOTween.To(() => 0, x => dialogueLabel.text = $"{x}", number, dialogueDuration).Play().SetTarget(this);
    }
}