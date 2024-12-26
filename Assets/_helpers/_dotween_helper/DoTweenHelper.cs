using DG.Tweening;
using UnityEngine;

public static class DoTweenHelper
{
   
   public static void DoMoveXYoYo(Transform target, float moveDistance, float duration, Ease ease, LoopType loopType)
   {
      target.DOMoveX(moveDistance, duration).SetEase(ease).SetLoops(-1, loopType); 
   }
   
   public static void DoMoveZYoYo(Transform target, float moveDistance, float duration, Ease ease, LoopType loopType)
   {
      target.DOMoveZ(moveDistance, duration).SetEase(ease).SetLoops(-1, loopType); 
   }
   
   
   public static void DoHoverY(Transform target, float hoverHeight, float duration, Ease ease, LoopType loopType)
   {
      target.DOMoveY(target.position.y + 0.15f, .4f).SetEase(ease).SetLoops(-1, loopType); 
   }
   
   // Animates the transform to appear, move up a bit, and disappear.
   public static void DoAppearMoveUpDisappear(Transform targetTransform, float moveUpDistance = 1f, float moveDuration = 1f, float fadeDuration = 0.5f)
   {
      Vector3 initialPosition = targetTransform.position;
      targetTransform.gameObject.SetActive(true);
      
      CanvasGroup canvasGroup = targetTransform.GetComponent<CanvasGroup>();
      if (canvasGroup == null)
      {
         canvasGroup = targetTransform.gameObject.AddComponent<CanvasGroup>();
      }
      canvasGroup.alpha = 0;
      canvasGroup.DOFade(1, fadeDuration);
      
      targetTransform.DOMoveY(targetTransform.position.y + moveUpDistance, moveDuration).OnComplete(() =>
      {
         canvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
         {
            targetTransform.position = initialPosition;
            targetTransform.gameObject.SetActive(false);
         });
      });
   }
}
