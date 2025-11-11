using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float delay;
    [SerializeField] private int maxCount;
    [SerializeField] private List<EnemyProfile> profiles = new();

    private float _timeCount;
    [SerializeField] private List<Enemy> _enemyList = new();
    [SerializeField] private BezierCurve bezierCurve;
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
        var rnd = new System.Random();
        var profile = profiles[rnd.Next(profiles.Count)];
        enemy.Init(profile.ModelSprite, rnd.Next(10), bezierCurve.PositionList);
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
