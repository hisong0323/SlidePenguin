using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool IsJump { get; private set; }

    private void Update()
    {
#if UNITY_EDITOR
        Horizontal = Input.GetAxis("Horizontal");
#elif UNITY_ANDROID
        Horizontal = Input.acceleration.x;
#endif
    }
}
