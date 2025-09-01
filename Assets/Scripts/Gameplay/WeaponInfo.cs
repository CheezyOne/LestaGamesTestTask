using UnityEngine;

public enum WeaponType
{
    Chopping,
    Crushing,
    Peircing
}

[CreateAssetMenu]
public class WeaponInfo : ScriptableObject
{
    public int Damage;
    public GameObject Weapon;
    public WeaponType WeaponType;
}