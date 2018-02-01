using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Power
{
    public string name;

    [Header("Attributes Given")]
    public float damageGiven;
    public float dashDamageGiven;
    public float moveSpeedGiven;
    public float attackSpeedGiven;
    public bool enablesCombo;
    public float aoeRangeGiven;
    public bool enablesFirePower;
    public bool enablesFireStoring;
    public bool enablesShield;
    public float shieldRegenGiven;
    public bool enablesShieldRegen;

}
