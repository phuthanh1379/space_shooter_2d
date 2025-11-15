using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private BezierCurve bezierCurve;
    
    private List<Enemy> ActiveEnemyList { get; } = new();
    private List<Enemy> DisabledEnemyList { get; } = new();

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
        if (ActiveEnemyList is not { Count: > 0 })
        {
            return;
        }

        for (var i = 0; i < ActiveEnemyList.Count; i++)
        {
            DisableEnemy(i);
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
            RemoveEmptyEnemy();
            GetEnemy();
            _timeCount = 0;
        }

        _timeCount += Time.deltaTime;
    }

    private EnemyProfile GetRandomProfile()
        => profiles[new System.Random().Next(profiles.Count)];

    private Enemy GetEnemy()
    {
        if (DisabledEnemyList == null || DisabledEnemyList.Count <= 0)
        {
            return SpawnEnemy();
        }
        else
        {
            return ReActivateEnemy();
        }
    }

    private Enemy SpawnEnemy()
    {
        if (!_isSpawnable || ActiveEnemyList.Count >= maxCount)
        {
            return null;
        }

        var enemy = Instantiate(enemyPrefab);
        var rnd = new System.Random();
        var profile = GetRandomProfile();
        enemy.Init(profile.ModelSprite, rnd.Next(10), bezierCurve.PositionList);
        ActiveEnemyList.Add(enemy);
        return enemy;
    }

    private Enemy ReActivateEnemy()
    {
        if (DisabledEnemyList == null || DisabledEnemyList.Count <= 0)
        {
            return null;
        }

        var enemy = DisabledEnemyList[0];
        enemy.gameObject.SetActive(true);
        enemy.Enable();
        DisabledEnemyList.RemoveAt(0);
        ActiveEnemyList.Add(enemy);

        return enemy;
    }

    private void DisableEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            return;
        }

        enemy.Disable();
        enemy.gameObject.SetActive(false);
        if (ActiveEnemyList.Contains(enemy))
        {
            ActiveEnemyList.Remove(enemy);
        }

        DisabledEnemyList.Add(enemy);
    }

    private void DisableEnemy(int index)
    {
        if (ActiveEnemyList == null || index < 0 || index >= ActiveEnemyList.Count)
        {
            return;
        }

        var enemy = ActiveEnemyList[index];
        if (enemy == null)
        {
            return;
        }

        enemy.Disable();
        enemy.gameObject.SetActive(false);
        ActiveEnemyList.RemoveAt(index);
        DisabledEnemyList.Add(enemy);
    }

    private void RemoveEmptyEnemy()
    {
        for (var i = 0; i < ActiveEnemyList.Count; i++)
        {
            var enemy = ActiveEnemyList[i];
            if (enemy == null)
            {
                ActiveEnemyList.Remove(enemy);
            }
        }
    }
}