using UnityEngine;

public class TileSpawner : Singleton<TileSpawner>
{
    [SerializeField]
    private Tile _tilePrefab;

    [SerializeField]
    private Tile _lastTile;

    private bool skip;

    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            TileSpawn();
        }
    }

    public void TileSpawn(Tile tile = null)
    {

        Vector3 spawnPosition = _lastTile.transform.position + Vector3.forward * 16;

        if (tile != null)
        {

        }

        if (tile != null)
            _lastTile = tile;
        else
            _lastTile = PoolManager.Instance.SpawnObject(_tilePrefab);

        _lastTile.transform.position = spawnPosition;
        _lastTile.Setting();
    }
}
