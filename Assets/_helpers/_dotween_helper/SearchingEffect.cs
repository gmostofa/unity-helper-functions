using UnityEngine;
using DG.Tweening;

public class SearchingEffect : MonoBehaviour
{
    public RectTransform searchImage; // Image to animate
    public float radius = 50f;        // Radius of the circular motion
    public float duration = 2f;       // Time to complete one full circle

    private Vector3 initialLocalPosition; // Store the original local position
    private Tween circularTween;

    private void Start()
    {
        initialLocalPosition = searchImage.localPosition; // Save the starting local position
        StartCircularSearch();
    }

    public void StartCircularSearch()
    {
        // Kill any existing tween to prevent overlap
        circularTween?.Kill();

        // Animate in a circular motion using trigonometric interpolation
        circularTween = DOTween.To(
                () => 0f,
                angle => MoveInCircle(angle),
                360f,
                duration
            ).SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart); // Infinite seamless looping

        // Pulse effect (size change)
        searchImage.DOScale(1.1f, 0.5f).SetLoops(-1, LoopType.Yoyo); // Pulsing effect

        // Fade effect
        CanvasGroup canvasGroup = searchImage.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.DOFade(0.5f, 1f).SetLoops(-1, LoopType.Yoyo); // Fading effect
        }
    }

    private void MoveInCircle(float angle)
    {
        float radians = Mathf.Deg2Rad * angle;
        float x = Mathf.Cos(radians) * radius;
        float y = Mathf.Sin(radians) * radius;

        searchImage.localPosition = initialLocalPosition + new Vector3(x, y, 0);
    }

    public void StopSearch()
    {
        circularTween?.Kill(); // Stop the animation
        searchImage.localPosition = initialLocalPosition; // Reset to the initial position smoothly
    }
}