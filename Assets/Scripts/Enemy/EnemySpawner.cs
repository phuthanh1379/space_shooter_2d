using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private BossEnemy bossEnemyPrefab;
    [SerializeField] private float delay;
    [SerializeField] private int maxCount;
    [SerializeField] private List<EnemyProfile> profiles = new();
    [SerializeField] private Vector3 baseBossEnemyPosition;
    [SerializeField] private Vector3 startBossEnemyPosition;

    private float _timeCount;
    [SerializeField] private List<Enemy> _enemyList = new();
    [SerializeField] private BezierCurve bezierCurve;
    //private List<Enemy> _disabledEnemyList = new();

    private bool _isSpawnable;
    private BossEnemy _bossEnemy;

    private void Awake()
    {
        GameController.BossEnemyAppear += OnBossEnemyAppear;
        GameController.BossEnemyFightStart += OnBossEnemyFightStart;
    }

    private void OnDestroy()
    {
        GameController.BossEnemyAppear -= OnBossEnemyAppear;
        GameController.BossEnemyFightStart -= OnBossEnemyFightStart;
    }

    private void OnBossEnemyAppear()
    {
        _isSpawnable = false;
        if (_enemyList == null || _enemyList.Count <= 0)
        {
            return;
        }

        foreach (var enemy in _enemyList)
        {
            enemy.Die();
        }

        _bossEnemy = Instantiate(bossEnemyPrefab, baseBossEnemyPosition, Quaternion.identity);
        _bossEnemy.Init(startBossEnemyPosition, 2f);
    }

    private void OnBossEnemyFightStart()
    {
        
    }

    private void Start()
    {
        _isSpawnable = true;
    }

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
        if (!_isSpawnable)
        {
            return;
        }

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
