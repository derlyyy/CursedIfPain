using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardVisual : MonoBehaviour
{
    private bool initalize = false;
    
    public Card parentCard;
    private int savedIndex;
    
    private Transform cardTransform;
    [SerializeField] private Transform shakeParent;
    [SerializeField] private Transform tiltParent;
    private Vector3 rotationDelta;

    [Header("Rotation Parameters")]
    [SerializeField] private float rotationAmount = 20;
    [SerializeField] private float rotationSpeed = 20;
    [SerializeField] private float autoTiltAmount = 30;
    [SerializeField] private float manualTiltAmount = 20;
    [SerializeField] private float tiltSpeed = 20;
    
    [Header("Hover Parameters")]
    [SerializeField] private float hoverPunchAngle = 5;
    [SerializeField] private float hoverTransition = .15f;

    [Header("Scale Parameters")]
    [SerializeField] private float scaleOnHover = 1.15f;
    [SerializeField] private float scaleTransition = 0.15f;
    [SerializeField] private Ease scaleEase = Ease.OutBack;
    
    [Header("Swap Parameters")]
    [SerializeField] private bool swapAnimations = true;
    [SerializeField] private float swapRotationAngle = 30;
    [SerializeField] private float swapTransition = .15f;
    [SerializeField] private int swapVibrato = 5;
    
    [Header("Curve")]
    [SerializeField] private CurveParameters curve;
    
    private float curveYOffset;
    private float curveRotationOffset;
    private Coroutine pressCoroutine;

    private void Start()
    {
        cardTransform = transform;
    }

    void Update()
    {
        //HandPositioning();
        TiltCard();
    }
    
    public void Initialize(Card target, Transform parentTransform, int index = 0)
    {
        //Declarations
        parentCard = target;
        cardTransform = target.transform;
        //canvas = GetComponent<Canvas>();
        //shadowCanvas = visualShadow.GetComponent<Canvas>();
        
        parentCard.PointerEnterEvent.AddListener(PointerEnter);
        parentCard.PointerExitEvent.AddListener(PointerExit);
        
        transform.SetParent(parentTransform);

        //инициализация
        initalize = true;
    }
    
    private void HandPositioning()
    {
        curveYOffset = (curve.positioning.Evaluate(parentCard.NormalizedPosition()) * curve.positioningInfluence) * parentCard.SiblingAmount();
        curveYOffset = parentCard.SiblingAmount() < 5 ? 0 : curveYOffset;
        curveRotationOffset = curve.rotation.Evaluate(parentCard.NormalizedPosition());
    }
    
    public void UpdateIndex(int length)
    {
        transform.SetSiblingIndex(parentCard.transform.parent.GetSiblingIndex());
    }
    
    private void TiltCard()
    {
        savedIndex = parentCard.isDragging ? savedIndex : parentCard.ParentIndex();
        float sine = Mathf.Sin(Time.time + savedIndex) * (parentCard.isHovering ? 0.2f : 1);
        float cosine = Mathf.Cos(Time.time + savedIndex) * (parentCard.isHovering ? 0.2f : 1);

        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float tiltX = parentCard.isHovering ? ((offset.y * -1) * manualTiltAmount * 10f) : 0;
        float tiltY = parentCard.isHovering ? ((offset.x) * manualTiltAmount) : 0;
        float tiltZ = parentCard.isDragging ? tiltParent.eulerAngles.z : (curveRotationOffset * (curve.rotationInfluence * parentCard.SiblingAmount()));

        float lerpX = Mathf.LerpAngle(tiltParent.eulerAngles.x, tiltX + (sine * autoTiltAmount), tiltSpeed * Time.deltaTime);
        float lerpY = Mathf.LerpAngle(tiltParent.eulerAngles.y, tiltY + (cosine * autoTiltAmount), tiltSpeed * Time.deltaTime);
        float lerpZ = Mathf.LerpAngle(tiltParent.eulerAngles.z, tiltZ, tiltSpeed / 2 * Time.deltaTime);

        tiltParent.eulerAngles = new Vector3(lerpX, lerpY, lerpZ);
    }

    private void PointerEnter(Card card)
    {
        transform.DOScale(scaleOnHover, scaleTransition).SetEase(scaleEase);

        DOTween.Kill(2, true);
        shakeParent.DOPunchRotation(Vector3.forward * hoverPunchAngle, hoverTransition, 20, 1).SetId(2);
    }

    private void PointerExit(Card card)
    {
        transform.DOScale(1, scaleTransition).SetEase(scaleEase);
    }
}