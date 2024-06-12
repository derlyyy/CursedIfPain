using UnityEngine;
using DG.Tweening;

public class EyeBlink : MonoBehaviour
{
    public GameObject parent;
    public GameObject pupil; // зрачок
    
    [Header("Blink Interval")]
    public float minBlinkInterval = 1.0f; // Минимальное время между морганиями
    public float maxBlinkInterval = 5.0f; // Максимальное время между морганиями
    
    [Header("Blink Duration")]
    public float blinkDuration = 0.1f; // Длительность моргания
    
    [Header("Eye Blink Scale")]
    public float minClosedScaleY = 0.1f; // Минимальный масштаб глаза при закрытии (моргании)
    public float maxOpenScaleY = 1.0f; // Максимальный масштаб глаза при открытии
    public float minOpenScaleY = 0.8f; // Минимальный масштаб глаза при открытии

    [Header("Eye Scale")]
    public float minScaleEye;
    public float maxScaleEye;

    [Header("Random Rotation Z")]
    public float randomRotationZ;
    
    [Header("Pupil")]
    public float pupilMoveDistance = 0.1f; // Расстояние колебания зрачка
    public float pupilMoveDuration = 0.5f; // Длительность одного колебания зрачка
    public float jitterRadius = 0.05f; // Радиус зоны дергания зрачка

    private float scale;

    private void Start()
    {
        StartBlinking();
        StartPupilMovement();

        scale = Random.Range(minScaleEye, maxScaleEye);
        Quaternion randomRotationZ = Quaternion.Euler(0, 0, Random.Range(0, 360));

        parent.transform.localScale = new Vector3(scale, scale, scale);
        parent.transform.rotation = randomRotationZ;
    }

    private void StartBlinking()
    {
        Blink();
    }

    private void Blink()
    {
        float blinkInterval = Random.Range(minBlinkInterval, maxBlinkInterval);
        float randomOpenScaleY = Random.Range(minOpenScaleY, maxOpenScaleY);

        DOTween.Sequence()
            .AppendInterval(blinkInterval)
            .Append(parent.transform.DOScaleY(minClosedScaleY, blinkDuration).SetEase(Ease.InOutQuad)) // Закрытие глаза
            .Append(parent.transform.DOScaleY(scale, blinkDuration).SetEase(Ease.InOutQuad)) // Открытие глаза до случайного значения
            .AppendCallback(() => Blink()); // Рекурсивный вызов для бесконечного моргания с разным интервалом
    }
    
    private void StartPupilMovement()
    {
        // Начальная позиция зрачка
        Vector3 initialPosition = pupil.transform.localPosition;
        // Центр окружности
        Vector3 circleCenter = initialPosition + Vector3.down * pupilMoveDistance;
        // Радиус окружности
        float radius = pupilMoveDistance;
        // Время одного оборота
        float circleTime = pupilMoveDuration * 2f;

        // Круговое движение зрачка
        Sequence pupilSequence = DOTween.Sequence();
        pupilSequence.Append(pupil.transform.DOLocalMove(circleCenter + Vector3.left * radius, circleTime).SetEase(Ease.Linear))
            .Append(pupil.transform.DOLocalMove(circleCenter + Vector3.up * radius, circleTime).SetEase(Ease.Linear))
            .Append(pupil.transform.DOLocalMove(circleCenter + Vector3.right * radius, circleTime).SetEase(Ease.Linear))
            .Append(pupil.transform.DOLocalMove(circleCenter + Vector3.down * radius, circleTime).SetEase(Ease.Linear))
            .SetLoops(-1);

        // Зрачок дергается в случайных точках в пределах зоны дергания
        Vector3 jitterStartPosition = pupil.transform.localPosition;
        Vector3 jitterEndPosition = Vector2.zero;
        Sequence jitterSequence = DOTween.Sequence().SetLoops(-1);
        jitterSequence.AppendCallback(() =>
        {
            jitterEndPosition = circleCenter + (Vector3)(Random.insideUnitCircle.normalized * jitterRadius); // Приводим Vector2 к Vector3
            pupil.transform.DOLocalMove(jitterEndPosition, pupilMoveDuration).SetEase(Ease.Linear);
        });
        jitterSequence.AppendInterval(pupilMoveDuration);
    }
}