using UnityEngine;
using Lean.Touch;
using System;
using Deslab.UI;

public class Player : Character
{
    [SerializeField] private ParticleSystem shield;
    [HideInInspector] public string playerName;

    public static Player Instance { get; private set; }
    public static Action OnLoaded = delegate { };

    private Cannon _myCannon;

    protected override void Init()
    {
        Instance = this;
        base.Init();
        OnLoaded?.Invoke();
        _myCannon = GetComponentInChildren<Cannon>();
    }

    protected override void Start()
    {
        base.Start();
        LeaderboardListElement entity = new LeaderboardListElement(
            true,
            entityID = "YOU",
            charData.MaxHealth,
            "YOU");
        Leaderboard.instance.AddToLeaderboard(entity);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Perk.OnTrigger += OnPerkTriggered;
        Perk.OnEnd += OnPerkEnded;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        LeanTouch.OnFingerDown -= OnFingerDownHandler;
        Perk.OnTrigger -= OnPerkTriggered;
        Perk.OnEnd -= OnPerkEnded;
    }

    protected override void OnLevelStartedHandler()
    {
        base.OnLevelStartedHandler();
        LeanTouch.OnFingerDown += OnFingerDownHandler;
    }

    protected override void OnLevelEndedHandler()
    {
        base.OnLevelEndedHandler();
        LeanTouch.OnFingerDown -= OnFingerDownHandler;
    }

    private void OnFingerDownHandler(LeanFinger finger)
    {
        if (!finger.IsOverGui)
        {
            _cannon.Shoot();
        }
    }

    private void OnPerkTriggered(Perk triggeredPerk)
    {
        if (triggeredPerk is FireballPerk)
        {
            FireballPerk perk = triggeredPerk as FireballPerk;
            _myCannon.SetProjectileToSpawn(perk.FireBallProj);
        }
        if (triggeredPerk is MultiBulletsPerk)
        {
            MultiBulletsPerk perk = triggeredPerk as MultiBulletsPerk;
            _myCannon.MultiShoots = true;
            _myCannon.ProjectilesPerShoot = perk.BulletsCount;
            _myCannon.MultiShootsRate = perk.FireRate;
        }
        if (triggeredPerk is ShieldPerk)
            shield.gameObject.SetActive(true);
    }

    private void OnPerkEnded(Perk endedPerk)
    {
        if (endedPerk is FireballPerk)
            _myCannon.ResetProjectileToSpawn();
        if (endedPerk is MultiBulletsPerk)
            _myCannon.MultiShoots = false;
        if (endedPerk is ShieldPerk)
            shield.gameObject.SetActive(false);
    }

    protected override void TakeDamage(int damage)
    {
        if (ShieldPerk.IsActive) return;
        base.TakeDamage(damage);
    }

    protected override void Die()
    {
        base.Die();
        StaticManager.OnLose?.Invoke();
    }
}
