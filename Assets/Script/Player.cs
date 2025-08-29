using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _moveSpeed;

    [SerializeField]
    private int _jumpPower;

    [SerializeField]
    private GameObject _model;

    [SerializeField]
    private TrailRenderer _trailRenderer;

    [SerializeField]
    LayerMask _tileLayer;

    [SerializeField]
    private AudioClip _moveSound;

    private Rigidbody _rigidbody;

    private bool _isGround = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SoundManager.Instance.PlaySound(_moveSound);    
    }

    private void Update()
    {
        CheckGround();
        Jump();
#if UNITY_EDITOR
        Move(Input.GetAxis("Horizontal"));
        Rotate(Input.GetAxis("Horizontal"));
#elif UNITY_ANDROID
        Move(Input.acceleration.x);
        Rotate(Input.acceleration.x);
#endif
    }

    private void Move(float acceleration)
    {
        transform.Translate((Vector3.forward + Vector3.right * acceleration * 1.4f) * _moveSpeed * Time.deltaTime, Space.World);
    }

    private void Rotate(float acceleration)
    {
        _model.transform.rotation = Quaternion.Euler(Vector3.back * 30 * acceleration);
    }

    private void Jump()
    {
        if (_isGround && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        if (_isGround && Input.acceleration.y >= 1)
        {
            _rigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
    }

    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        Debug.DrawRay(transform.position, Vector3.down * 1.8f, Color.red);

        if (Physics.Raycast(ray, 1.8f, _tileLayer))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }
        _trailRenderer.emitting = _isGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
