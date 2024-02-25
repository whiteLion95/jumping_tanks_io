using UnityEngine;
using Deslab.Level;
using System;
using Deslab.UI;
using TMPro;
using DG.Tweening;

/// <summary>
/// Base class for player and enemies
/// </summary>
public abstract class Character : MonoBehaviour
{
    [SerializeField] protected CharacterData charData;
    [SerializeField] private SliderBar hP_Bar;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Transform deathParticlePlace;
    internal string entityID;

    protected Cannon _cannon;
    protected Animator _anim;
    protected int _currentHealth = 0;

    public static Action<Projectile, Character> OnShooted = delegate { };
    public static Action<Character> OnDie = delegate { };

    public Transform DeathParticlePlace { get { return deathParticlePlace; } }

    protected virtual void OnEnable()
    {
        LevelManager.OnLevelStarted += OnLevelStartedHandler;
        StaticManager.OnWin += OnLevelEndedHandler;
        StaticManager.OnLose += OnLevelEndedHandler;
        OnShooted += OnShootedHandler;
    }

    protected virtual void OnDisable()
    {
        LevelManager.OnLevelStarted -= OnLevelStartedHandler;
        StaticManager.OnWin -= OnLevelEndedHandler;
        StaticManager.OnLose -= OnLevelEndedHandler;
        OnShooted -= OnShootedHandler;
    }

    protected virtual void Awake()
    {
        Init();
        Animate(false);
    }

    protected virtual void Start()
    {
        SetHealth();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            Projectile shootedProjectile = collision.gameObject.GetComponentInParent<Projectile>();
            if (!_cannon.MyProjectiles.Contains(shootedProjectile) && shootedProjectile != null)
            {
                OnShooted?.Invoke(shootedProjectile, this);
            }
        }
    }

    protected virtual void Init()
    {
        _cannon = GetComponentInChildren<Cannon>();
        _anim = GetComponentInChildren<Animator>();
    }

    protected virtual void OnLevelStartedHandler()
    {
        _cannon.StartShooting();
        Animate(true);
    }

    protected virtual void OnLevelEndedHandler()
    {
        _cannon.StopShooting();
    }

    protected void Animate(bool value)
    {
        _anim.enabled = value;
    }

    private void SetHealth()
    {
        _currentHealth = charData.MaxHealth;
        hP_Bar.SetMaxValue(_currentHealth);
        healthText.text = _currentHealth.ToString();
    }

    private void OnShootedHandler(Projectile shootingProj, Character shootedChar)
    {
        if (shootedChar.Equals(this))
        {
            TakeDamage(shootingProj.Damage);
        }
    }

    protected virtual void TakeDamage(int damage)
    {
        Leaderboard.instance.UpdateEntityHpValue(entityID, _currentHealth);

        if (_currentHealth > 0)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                Die();
            }
            hP_Bar.ChangeValue(_currentHealth);
            healthText.DOText(_currentHealth.ToString(), 0.3f);
        }
    }

    protected virtual void Die()
    {
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }
}
