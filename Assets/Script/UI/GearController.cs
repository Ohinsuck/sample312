using UnityEngine;
using UnityEngine.InputSystem;

public class GearController : MonoBehaviour
{
    public float rotationSpeed = 360f;
    private float rotateDir; // -1(좌), 0(정지), +1(우)

    // PlayerInput(Invoke Unity Events)
    // RotateLeft(Action: Button) → 이 메서드 바인딩
    public void RotateLeft(InputAction.CallbackContext ctx)
    {
        // performed일 때 -1, canceled일 때 0
        rotateDir = ctx.performed ? -1f : 0f;
    }

    // RotateRight(Action: Button) → 이 메서드 바인딩
    public void RotateRight(InputAction.CallbackContext ctx)
    {
        rotateDir = ctx.performed ? +1f : 0f;
    }

    private void Update()
    {
        if (Mathf.Abs(rotateDir) > 0f)
            transform.Rotate(Vector3.forward * (-rotateDir) * rotationSpeed * Time.deltaTime);
    }
}
