using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    [SerializeField] private GameObject backFace;
    [SerializeField] private GameObject frontFace;
    [SerializeField] private Image icon;
    [SerializeField] private Button backFaceButton;
    [SerializeField] CanvasGroup canvasGroup;
    // [SerializeField] private GameObject matchedEffect;

    public int EntityId { get; private set; }
    private PlayingView controller;

    public void Initialize(int entityId, PlayingView gameController, Sprite sprite)
    {
        EntityId = entityId;
        controller = gameController;
        icon.sprite = sprite;
        backFaceButton.onClick.AddListener(OnMouseDown);
        InitWithFaceUp();
    }

    private void InitWithFaceUp()
    {
        backFace.SetActive(false);
        frontFace.SetActive(true);
    }
    public void UpdateView(CardComponent cardData, float withDelay = 0)
    {
        switch (cardData.state)
        {
            case CardState.FaceDown:
                StartCoroutine(Flip(frontFace.transform, backFace.transform, withDelay: withDelay));
                break;

            case CardState.FaceUp:
                StartCoroutine(Flip(backFace.transform, frontFace.transform, withDelay: withDelay));
                break;

            case CardState.Matched:
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                break;
        }
    }

    private void OnMouseDown()
    {
        controller.OnCardClicked(EntityId);
    }

    IEnumerator Flip(Transform imageToFlip, Transform finalImage, float duration = 0.3f, float withDelay = 0f)
    {
        yield return new WaitForSeconds(withDelay);
        Sequence seq = DOTween.Sequence();

        // FIRST HALF — scale up + rotate 0 -> 180
        seq.Append(
            imageToFlip.DOScale(1.2f, duration * 0.5f)
             .SetEase(Ease.InOutQuad)
        );

        seq.Join(
            imageToFlip.DORotate(new Vector3(0, 180, 0), duration * 0.5f, RotateMode.FastBeyond360)
             .SetEase(Ease.InOutQuad)
        );



        // SECOND HALF — rotate back 180 -> 0 + scale down 1.2 -> 1
        seq.Append(
            imageToFlip.DOScale(1f, duration * 0.5f)
             .SetEase(Ease.InOutQuad)
        );

        seq.Join(
            imageToFlip.DORotate(new Vector3(0, 0, 0), duration * 0.5f, RotateMode.FastBeyond360)
             .SetEase(Ease.InOutQuad)
        );

        seq.InsertCallback(duration * 0.5f, () =>
        {
            finalImage.gameObject.SetActive(true);
            imageToFlip.gameObject.SetActive(false);
        });
        seq.Play();
    }

}
