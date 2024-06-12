using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform shakeParent;
    [SerializeField] private Transform tiltParent;
    private Transform cardTransform;

    [Header("Hover Parameters")]
    [SerializeField] private float hoverPunchAngle = 5;
    [SerializeField] private float hoverTransition = .15f;

    [Header("Scale Parameters")]
    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleTransition = 0.15f;
    [SerializeField] private Ease scaleEase = Ease.OutBack;

    [Header("Tilt Parameters")]
    [SerializeField] private float autoTiltAmount = 30;
    [SerializeField] private float manualTiltAmount = 20;
    [SerializeField] private float tiltSpeed = 20;

    private void Start()
    {
        if (shakeParent == null || tiltParent == null)
        {
            Debug.LogError("Components not assigned properly.");
            enabled = false;
            return;
        }

        cardTransform = transform;

        // Если дочерние объекты ShakeParent и TiltParent не указаны, попробуйте найти их по имени
        if (shakeParent == null)
            shakeParent = transform.Find("ShakeParent");
        if (tiltParent == null)
            tiltParent = transform.Find("TiltParent");
    }

    void Update()
    {
        TiltCard();
    }

    private void TiltCard()
    {
        float sine = Mathf.Sin(Time.time) * (IsHovering() ? 0.2f : 1);
        float cosine = Mathf.Cos(Time.time) * (IsHovering() ? 0.2f : 1);

        Vector3 offset = cardTransform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float tiltX = IsHovering() ? ((offset.y * -1) * manualTiltAmount * 10f) : 0;
        float tiltY = IsHovering() ? ((offset.x) * manualTiltAmount) : 0;
        float tiltZ = tiltParent.eulerAngles.z;

        float lerpX = Mathf.LerpAngle(tiltParent.eulerAngles.x, tiltX + (sine * autoTiltAmount), tiltSpeed * Time.deltaTime);
        float lerpY = Mathf.LerpAngle(tiltParent.eulerAngles.y, tiltY + (cosine * autoTiltAmount), tiltSpeed * Time.deltaTime);
        float lerpZ = Mathf.LerpAngle(tiltParent.eulerAngles.z, tiltZ, tiltSpeed / 2 * Time.deltaTime);

        tiltParent.eulerAngles = new Vector3(lerpX, lerpY, lerpZ);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);

        DOTween.Kill(2, true);
        shakeParent.DOPunchRotation(Vector3.forward * hoverPunchAngle, hoverTransition, 20, 1).SetId(2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }

    private bool IsHovering()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
