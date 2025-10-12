using UnityEngine;

public class TestChartLoader : MonoBehaviour
{
    public GameObject standardNotePrefab;

    void Start()
    {
        // lane: 0~3
        SpawnNote(0, Time.time + 1f, false, 0f);
        SpawnNote(1, Time.time + 2f, true,  2f);
        SpawnNote(2, Time.time + 3f, false, 0f);
        SpawnNote(3, Time.time + 4f, false, 0f);
    }

    private void SpawnNote(int lane, float targetTime, bool isLong, float duration)
    {
        var obj = Instantiate(standardNotePrefab, Vector3.zero, Quaternion.identity, transform);

        // UI / 월드 스폰 위치 분기
        if (obj.TryGetComponent<RectTransform>(out var rt))
        {
            // UI(px) 기준 스폰 — 위쪽에서 떨어지도록 큰 y값
            float[] xsUI = { -240f, -80f, 80f, 240f };   // 캔버스 폭에 맞춰 조정
            rt.anchoredPosition = new Vector2(xsUI[lane], 600f);
        }
        else
        {
            // 월드 좌표 스폰
            float[] xsWorld = { -1.5f, -0.5f, 0.5f, 1.5f };
            obj.transform.position = new Vector3(xsWorld[lane], 6f, 0f);
        }

        if (obj.TryGetComponent<NoteBehavior>(out var note))
            note.Initialize(targetTime, lane, isLong, duration);

        // 롱노트 시각 길이 보정(선택)
        if (isLong)
        {
            if (obj.TryGetComponent<RectTransform>(out var rt2))
            {
                var size = rt2.sizeDelta;
                size.y = 120f + duration * 80f;
                rt2.sizeDelta = size;
            }
            else
            {
                var t = obj.transform;
                var s = t.localScale;
                s.y = s.y * (1f + duration * 0.5f);
                t.localScale = s;
            }
        }
    }
}
