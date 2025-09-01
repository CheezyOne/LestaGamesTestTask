using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Attack))]
public class CharacterInfo : MonoBehaviour
{
    [SerializeField] protected Characteristics _characteristics;
    protected Health _health;
    protected Attack _attack;

    public Characteristics Characteristics => _characteristics;
    public Health Health => _health;
    public Attack Attack => _attack;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _attack = GetComponent<Attack>();
        _attack.SetCharacterInfo(this);
    }
}

[Serializable] 
public class Characteristics
{
    public int Agility;
    public int Strength;
    public int Durability;

    private List<int> _randomCharacteristics = new() { 1, 2, 3 };

    public void CreateRandomCharacteristics()
    {
        int randomInt = Random.Range(0, _randomCharacteristics.Count);
        Agility = _randomCharacteristics[randomInt];
        _randomCharacteristics.RemoveAt(randomInt);
        randomInt = Random.Range(0, _randomCharacteristics.Count);
        Strength = _randomCharacteristics[randomInt];
        _randomCharacteristics.RemoveAt(randomInt);
        Durability = _randomCharacteristics[0];
    }
}