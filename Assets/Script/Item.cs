using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int score;

    [SerializeField]
    private AudioClip pickupSFX;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 90 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseScore(score);
            PoolManager.Instance.DestroyObject(gameObject);
            SoundManager.Instance.PlaySFX(pickupSFX);
        }
    }
}
