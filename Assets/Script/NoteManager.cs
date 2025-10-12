using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NoteManager : MonoBehaviour
{
    public static NoteManager Instance { get; private set; }

    public const int LaneCount = 4;
    private const float JUDGE_Y_POSITION = -4.0f;

    private Dictionary<int, List<NoteBehavior>> activeNotes = new();
    private NoteBehavior[] heldLongNotes = new NoteBehavior[LaneCount];

    [SerializeField] private CatcherController catcherController;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        for (int lane = 0; lane < LaneCount; lane++)
            activeNotes[lane] = new List<NoteBehavior>();
    }

    private void Start()
    {
        if (catcherController == null)
            catcherController = Object.FindFirstObjectByType<CatcherController>();

        if (catcherController == null)
            Debug.LogError("[NoteManager] CatcherController 인스턴스를 찾을 수 없습니다! Inspector에서 연결하세요.");
    }

    public void AddNote(NoteBehavior note)
    {
        if (note == null) return;

        if (!activeNotes.ContainsKey(note.laneNumber))
        {
            Debug.LogWarning($"[NoteManager] 잘못된 laneNumber={note.laneNumber}. 0~{LaneCount - 1} 이어야 함.");
            return;
        }
        activeNotes[note.laneNumber].Add(note);
    }

    // 입력 시작 시
    public void StartJudge(int lane, float inputTime)
    {
        if (lane < 0 || lane >= LaneCount) return;

        NoteBehavior n = FindClosestNote(lane);
        var judgeCtrl = JudgmentController.Instance;
        var scoreMgr  = judgeCtrl != null ? judgeCtrl.scoreManager : null;

        if (n != null && judgeCtrl != null)
        {
            judgeCtrl.JudgeNote(n, inputTime);
            if (n.isLongNote)
                heldLongNotes[lane] = n;
        }
        else
        {
            if (scoreMgr != null) scoreMgr.ResetCombo();
        }
    }

    // 입력 종료 시
    public void EndJudge(int lane, float inputTime)
    {
        if (lane < 0 || lane >= LaneCount) return;

        if (heldLongNotes[lane] != null)
            HandleNoteHit(heldLongNotes[lane]);
    }

    public void ProcessLongNoteTicks(float t)
    {
        var judgeCtrl = JudgmentController.Instance;
        if (catcherController == null || judgeCtrl == null) return;

        for (int lane = 0; lane < LaneCount; lane++)
        {
            if (catcherController.laneInputState[lane] && heldLongNotes[lane] != null)
                judgeCtrl.ScoreLongNoteTick();
        }
    }

    public void HandleNoteHit(NoteBehavior note)
    {
        if (note == null) return;

        if (activeNotes.ContainsKey(note.laneNumber))
            activeNotes[note.laneNumber].Remove(note);

        if (note.isLongNote)
        {
            int lane = note.laneNumber;
            if (lane >= 0 && lane < LaneCount && heldLongNotes[lane] == note)
                heldLongNotes[lane] = null;
        }

        if (note.gameObject != null)
            Destroy(note.gameObject);
    }

    public void HandleNoteMiss(NoteBehavior note)
    {
        JudgmentController.Instance?.HandleMiss(note);
        HandleNoteHit(note);
    }

    private NoteBehavior FindClosestNote(int lane)
    {
        if (!activeNotes.ContainsKey(lane) || activeNotes[lane].Count == 0)
            return null;

        activeNotes[lane].RemoveAll(n => n == null);
        if (activeNotes[lane].Count == 0) return null;

        return activeNotes[lane]
            .OrderBy(n => Mathf.Abs(n.transform.position.y - JUDGE_Y_POSITION))
            .FirstOrDefault();
    }
}
