using UnityEngine;

public class Player : MonoBehaviour
{
    #region Field
    [SerializeField]
    private int moveSpeed;

    [SerializeField]
    private int jumpPower;

    [SerializeField]
    private GameObject model;

    [SerializeField]
    private TrailRenderer trailRenderer;

    [SerializeField]
    private LayerMask tileLayer;

    [SerializeField]
    private AudioClip moveSound;
    #endregion

    private Rigidbody _rigidbody;

    private bool _isGround = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SoundManager.Instance.PlaySound(moveSound);
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
        transform.Translate(moveSpeed * Time.deltaTime * (Vector3.forward + 1.4f * acceleration * Vector3.right), Space.World);
    }

    private void Rotate(float acceleration)
    {
        model.transform.rotation = Quaternion.Euler(30 * acceleration * Vector3.back);
    }

    private void Jump()
    {
        if (_isGround && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        if (_isGround && Input.acceleration.y >= 1)
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void CheckGround()
    {
        Ray ray = new(transform.position, Vector3.down);

        Debug.DrawRay(transform.position, Vector3.down * 1.8f, Color.red);

        if (Physics.Raycast(ray, 1.8f, tileLayer))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }
        trailRenderer.emitting = _isGround;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}