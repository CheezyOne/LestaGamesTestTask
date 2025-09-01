using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public enum HealthModificator
{
    None,
    Slimey,
    StoneSkin,
    Fragile,
    Shield
}

public class Health : MonoBehaviour
{
    [SerializeField] private WeaponInfo _lootWeapon;
    [SerializeField] private FloatingText _damageTakenText;
    [SerializeField] private Vector3 _damageTakenTextSpawnPosition;
    [SerializeField] private Animator _animator;
    [SerializeField] private string[] _deathTriggers;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Canvas _healthCanvas;
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private int _shieldDamageReduction = 3;

    [SerializeField] protected float _maxHealth;
    [SerializeField] protected List<HealthModificator> _healthModificators = new();

    public List<HealthModificator> HealthModificators { get => _healthModificators; }

    private CharacterInfo _characterInfo;
    private CharacterInfo _enemyInfo;
    private Transform _camera;
    private float _currentHealth;

    private void Awake()
    {
        SetStartHealth();
        _healthCanvas.worldCamera = Camera.main;
        _camera = _healthCanvas.worldCamera.transform;
    }

    public void SetOpponentsInfos(CharacterInfo characterInfo, CharacterInfo enemyInfo)
    {
        _characterInfo = characterInfo;
        _enemyInfo = enemyInfo;
    }

    protected void SetStartHealth()
    {
        _currentHealth = _maxHealth;
    }

    private void LateUpdate()
    {
        if (_camera != null)
        {
            _healthCanvas.transform.rotation = _camera.rotation;
        }

        _healthText.text = _currentHealth + "/" + _maxHealth;
    }

    public void GetAttacked(int damage, float delay = 0)
    {
        StartCoroutine(TakeDamageRoutine(delay, damage));
    }

    private IEnumerator TakeDamageRoutine(float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        ReduceDamage(ref damage);
        _currentHealth -= damage;
        _healthImage.fillAmount = _currentHealth / _maxHealth;

        if (_currentHealth < 0)
            _currentHealth = 0;

        _healthText.text = _currentHealth + "/" + _maxHealth;
        Instantiate(_damageTakenText, _damageTakenTextSpawnPosition + transform.position, Quaternion.identity).SetText("-" + damage);

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void ReduceDamage(ref int damage)
    {
        if (HealthModificators.Contains(HealthModificator.Shield) && _characterInfo.Characteristics.Strength > _enemyInfo.Characteristics.Strength)
        {
            damage -= _shieldDamageReduction;
        }

        if (HealthModificators.Contains(HealthModificator.StoneSkin))
        {
            damage -= _characterInfo.Characteristics.Durability;
        }

        if (damage<0)
        {
            damage = 0;
        }    
    }

    private void Die()
    {
        if (_deathTriggers.Length == 1)
        {
            _animator.SetTrigger(_deathTriggers[0]);
        }
        else
        {
            _animator.SetTrigger(_deathTriggers[Random.Range(0, _deathTriggers.Length)]);
        }

        OnDie();
    }

    protected virtual void OnDie()
    {
        EventBus.OnEnemyDeath?.Invoke(_lootWeapon);
    }
}