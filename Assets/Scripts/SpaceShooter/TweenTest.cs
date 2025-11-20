using DG.Tweening;
using UnityEngine;

public class TweenTest : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float duration;

    private Sequence _sequence;

    private void Start()
    {
        _sequence = DOTween.Sequence();

        transform.position = startPosition.position;
        var moveToEndTween = transform.DOMove(endPosition.position, duration);
        var moveToStartTween = transform.DOMove(startPosition.position, duration);
        var rotateTween = transform.DORotate(new Vector3(0f, 0f, 180f), duration);
        var zoomTween = transform.DOScale(2f, duration);
        var colorTween = spriteRenderer.DOColor(Color.red, duration);

        _sequence
            .Append(moveToEndTween)
            .Join(rotateTween)
            //.Append(moveToStartTween)
            .Join(zoomTween)
            .Join(colorTween)
            .OnStart(() => UnityEngine.Debug.Log("START SEQUENCE!"))
            .SetLoops(-1, LoopType.Restart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _sequence.Play();
        }
    }
}
