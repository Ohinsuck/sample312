using UnityEngine;

public class CatcherLineCtrl : MonoBehaviour
{
    // 판정선 충돌 체크용 (Trigger Collider2D + Rigidbody2D(Kinematic) 권장)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            Debug.Log("Note judged");
            // TODO: NoteManager/JudgmentController와 연동 가능
        }
    }
}
