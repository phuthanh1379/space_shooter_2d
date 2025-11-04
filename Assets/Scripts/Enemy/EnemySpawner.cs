using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float delay;
    [SerializeField] private int maxCount;

    private float _timeCount;
    [SerializeField] private List<Enemy> _enemyList = new();
    //private List<Enemy> _disabledEnemyList = new();

    private void Update()
    {
        if (_timeCount >= delay)
        {
            Spawn();
            _timeCount = 0;
        }

        _timeCount += Time.deltaTime;
    }

    private void Spawn()
    {
        ReCheckEnemyList();
        if (_enemyList == null || _enemyList.Count >= maxCount)
        {
            return;
        }

        var enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<EnemyMove>().Init(transform.position, transform.position + Vector3.down * 10f, 0.3f);
        _enemyList.Add(enemy);
    }

    private void ReCheckEnemyList()
    {
        for (var i = 0; i < _enemyList.Count; i++)
        {
            var enemy = _enemyList[i];
            if (enemy == null)
            {
                _enemyList.Remove(enemy);
            }
        }
    }
}
