using UnityEngine;

public class JudgmentController : MonoBehaviour
{
    public static JudgmentController Instance { get; private set; }
    public ScoreManager scoreManager;

    public float perfectRange = 0.08f;         // 판정 허용 범위
    public float longNoteTickInterval = 0.1f;  // 롱노트 틱 간격
    private float nextTickTime;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Update()
    {
        // 롱노트 틱 처리
        if (Time.time >= nextTickTime)
        {
            NoteManager.Instance?.ProcessLongNoteTicks(Time.time);
            nextTickTime = Time.time + longNoteTickInterval;
        }
    }

    // 일반/롱노트 시작 판정
    public void JudgeNote(NoteBehavior note, float inputTime)
    {
        float timeDifference = Mathf.Abs(inputTime - note.targetTime);

        if (timeDifference <= perfectRange)
        {
            scoreManager?.IncreaseScore(100);
            scoreManager?.IncreaseCombo();

            if (!note.isLongNote)
                NoteManager.Instance?.HandleNoteHit(note); // 일반노트 제거
        }
        else
        {
            scoreManager?.ResetCombo();
            NoteManager.Instance?.HandleNoteMiss(note);
        }
    }

    public void ScoreLongNoteTick()
    {
        scoreManager?.IncreaseScore(5); // 틱당 점수
    }

    public void HandleMiss(NoteBehavior note)
    {
        // Miss 시 연출/UI 필요하면 여기에
    }
}
