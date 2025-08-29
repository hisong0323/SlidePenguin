using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int _score;

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * 90 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.ScoreUp(_score);
            PoolManager.Instance.DestroyObject(gameObject);
        }
    }
}
