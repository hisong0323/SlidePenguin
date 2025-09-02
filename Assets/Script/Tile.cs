using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstacle;

    [SerializeField]
    private Item _item;

    private WaitForSeconds _waitSecond02;

    private int[] spawnPositions = { -8, -4, 0, 4, 8 };

    private void Awake()
    {
        _waitSecond02 = new WaitForSeconds(2);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            Respawn();
        }
    }

    public void Setting()
    {
        Shuffle();

        Vector3 spawnPositision = transform.position + Vector3.up * 2 + Vector3.right * spawnPositions[0];

        PoolManager.Instance.SpawnObject(_item, spawnPositision, Quaternion.identity, transform);

        for (int i = 1; i < spawnPositions.Length; i++)
        {
            spawnPositision = transform.position + Vector3.up * 2 + Vector3.right * spawnPositions[i];

            if (Random.value < 0.6f)
            {
                PoolManager.Instance.SpawnObject(_obstacle, spawnPositision, Quaternion.identity, transform);
            }
        }
    }

    private void Shuffle()
    {
        for (int i = 0; i < spawnPositions.Length - 1; i++)
        {
            int randomInt = Random.Range(i, spawnPositions.Length);
            int temp = spawnPositions[i];
            spawnPositions[i] = spawnPositions[randomInt];
            spawnPositions[randomInt] = temp;
        }
    }

    private void Respawn()
    {
        StartCoroutine(RespawnCorotine());
    }

    private IEnumerator RespawnCorotine()
    {
        yield return _waitSecond02;
        for (int i = transform.childCount -1 ; i >= 0; i--)
        {
            PoolManager.Instance.DestroyObject(transform.GetChild(i).gameObject);
        }
        TileSpawner.Instance.TileSpawn(this);
    }
}
