using System.Collections;
using UnityEngine;
using System;
using Deslab.UI;

public class Enemy : Character
{
    [SerializeField] private EnemyData data;

    private LowLevelTriggers lowLevelTriggers;

    public new static Action<Enemy> OnDie = delegate { };
    public UserBar userBar;


    protected override void Awake()
    {
        base.Awake();
        lowLevelTriggers = GetComponentInChildren<LowLevelTriggers>();
    }

    protected override void Start()
    {
        base.Start();

        LeaderboardListElement entity = new LeaderboardListElement(
            false,
            entityID,
            charData.MaxHealth,
            entityID);
        Leaderboard.instance.AddToLeaderboard(entity);
        CheckLevel();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _cannon.OnReadyToShoot += OnReadyToShootHandler;
    }

    private void OnReadyToShootHandler()
    {
        StartCoroutine(RandomShoot());
    }

    private IEnumerator RandomShoot()
    {
        float randTime = UnityEngine.Random.Range(0f, data.RandTimeRange);
        yield return new WaitForSeconds(randTime);
        _cannon.Shoot();
    }

    private void CheckLevel()
    {
        if (StaticManager.levelID <= data.EasyLevelsCount)
            lowLevelTriggers.gameObject.SetActive(true);
        else
            lowLevelTriggers.gameObject.SetActive(false);
    }

    protected override void Die()
    {
        OnDie?.Invoke(this);
        base.Die();
    }
}
