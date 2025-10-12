using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    [Header("Timing")]
    public float targetTime;           // 이 시간에 판정선에 도달
    public float duration = 0f;        // 롱노트 길이(초)

    [Header("Speed")]
    public float fallSpeedWorld = 5.0f;    // 월드 오브젝트용 (unit/sec)
    public float fallSpeedUI = 300.0f;     // UI용 (px/sec)

    [Header("Info")]
    public bool isLongNote = false;
    public int laneNumber = 0;             // 0~3

    // 내부 상태
    RectTransform rt;                      // UI이면 존재
    float judgeYWorld = -4.0f;             // 월드 판정선 y
    float judgeYUI = -200f;                // UI 판정선 y(px) - 필요시 조정
    float startXWorld;                     // 스폰 시 x 저장
    float startXUI;                        // 스폰 시 x 저장
    float missThresholdWorld = -10f;       // 판정선 아래로 지나가면 제거
    float missThresholdUI = -600f;         // UI 기준 미스 위치(px) - 필요시 조정

    public void Initialize(float target, int lane, bool isLong, float d)
    {
        targetTime = target;
        laneNumber = lane;
        isLongNote = isLong;
        duration = d;
    }

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        if (rt != null) startXUI = rt.anchoredPosition.x;
        else            startXWorld = transform.position.x;
    }

    void Update()
    {
        float timeToTarget = targetTime - Time.time;

        if (rt != null)
        {
            // UI 모드: anchoredPosition.y = judgeYUI + 남은시간*속도
            float y = judgeYUI + (timeToTarget * fallSpeedUI);
            rt.anchoredPosition = new Vector2(startXUI, y);

            if (y < missThresholdUI)
                Destroy(gameObject);
        }
        else
        {
            // 월드 모드: position.y = judgeYWorld + 남은시간*속도
            float y = judgeYWorld + (timeToTarget * fallSpeedWorld);
            transform.position = new Vector3(startXWorld, y, transform.position.z);

            if (y < missThresholdWorld)
                Destroy(gameObject);
        }
    }
}
