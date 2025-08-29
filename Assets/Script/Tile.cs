using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstacle;

    [SerializeField]
    private Item _item;

    private WaitForSeconds _waitSeceond02;

    private void Awake()
    {
        _waitSeceond02 = new WaitForSeconds(2);
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
        bool spawnItem = true;

        for (int i = -2; i < 3; i++)
        {
            Vector3 spawnPositision = transform.position + Vector3.up * 2 + Vector3.right * i * 4;

            if (Random.value < 0.3f && spawnItem)
            {
                PoolManager.Instance.SpawnObject(_item, spawnPositision);
                spawnItem = false;
            }
            else if(Random.value < 0.6f)
            {
                PoolManager.Instance.SpawnObject(_obstacle, spawnPositision);
            }
        }
    }

    private void Respawn()
    {
        StartCoroutine(RespawnCorotine());
    }

    private IEnumerator RespawnCorotine()
    {
        yield return _waitSeceond02;
        TileSpawner.Instance.TileSpawn(this);
    }
}
