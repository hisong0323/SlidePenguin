using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private int _speed;

    void Update()
    {
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.World);
    }
}
