using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CatcherController : MonoBehaviour
{
    [Header("UI refs")]
    public RectTransform catcherImg;   // 과일바구니(RectTransform)
    public Image[] lineLights;         // 선택적: 라인 이펙트

    [Header("Lane config")]
    public int laneCount = 4;          // 4칸
    public float sectionWidth = 160f;  // 칸 간격(px)
    public float startX = 0f;          // 왼쪽 기준 x (UI 앵커 기준)
    public int laneIndex = 1;          // 시작 칸 (0~3)

    // 롱노트 입력 상태(옵션)
    public bool[] laneInputState = new bool[4];

    private void Start()
    {
        UpdateCatcherPos();
    }

    // PlayerInput 이벤트 바인딩: LeftShift → CatcherMoveLeft
    public void CatcherMoveLeft(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (laneIndex > 0)
        {
            laneIndex--;
            UpdateCatcherPos();
            Debug.Log($"Catcher ←  laneIndex={laneIndex}");
        }
    }

    // PlayerInput 이벤트 바인딩: RightShift → CatcherMoveRight
    public void CatcherMoveRight(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        if (laneIndex < laneCount - 1)
        {
            laneIndex++;
            UpdateCatcherPos();
            Debug.Log($"Catcher →  laneIndex={laneIndex}");
        }
    }

    // Lane Judge (Z, X, ., /)
    public void Lane1Judge(InputAction.CallbackContext ctx)
    {
        laneInputState[0] = ctx.ReadValueAsButton();
        if (ctx.started)   Debug.Log("Lane1 START");
        if (ctx.performed) Debug.Log("Lane1 PERFORMED");
        if (ctx.canceled)  Debug.Log("Lane1 CANCEL");
        // TODO: NoteManager.Instance?.StartJudge(0, Time.time) 등 연동
    }
    public void Lane2Judge(InputAction.CallbackContext ctx)
    {
        laneInputState[1] = ctx.ReadValueAsButton();
        if (ctx.started)   Debug.Log("Lane2 START");
        if (ctx.performed) Debug.Log("Lane2 PERFORMED");
        if (ctx.canceled)  Debug.Log("Lane2 CANCEL");
    }
    public void Lane3Judge(InputAction.CallbackContext ctx)
    {
        laneInputState[2] = ctx.ReadValueAsButton();
        if (ctx.started)   Debug.Log("Lane3 START");
        if (ctx.performed) Debug.Log("Lane3 PERFORMED");
        if (ctx.canceled)  Debug.Log("Lane3 CANCEL");
    }
    public void Lane4Judge(InputAction.CallbackContext ctx)
    {
        laneInputState[3] = ctx.ReadValueAsButton();
        if (ctx.started)   Debug.Log("Lane4 START");
        if (ctx.performed) Debug.Log("Lane4 PERFORMED");
        if (ctx.canceled)  Debug.Log("Lane4 CANCEL");
    }

    private void UpdateCatcherPos()
    {
        if (catcherImg == null) return;
        var pos = catcherImg.anchoredPosition;
        pos.x = startX + sectionWidth * laneIndex;
        catcherImg.anchoredPosition = pos;
    }

    private void TriggerImpact(int lane)
    {
        // if (lineLights != null && lane < lineLights.Length)
        //     lineLights[lane].color = Color.yellow;
    }
}
