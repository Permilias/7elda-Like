using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    lanternless,
    lantern,
    fiery
}

public static class GameManager {

    public static float damage = 1;
    public static float dashDamage = 1;
    public static float moveSpeed = 4;
    public static float attackSpeed = 0.2f;
    public static bool canCombo = false;
    public static float aoeRange = 100;
    public static bool hasFirePower = false;
    public static bool canStoreFire = false;
    public static bool hasShield = false;
    public static float shieldRegen = 0;
    public static bool shieldRegens = false;

    public static int soulsPossessed = 0;
    public static int soulsTotal;
    public static int soulsSpent;
    public static int spirit1Souls;
    public static int spirit2Souls;
    public static int spirit3Souls;
    public static int spirit4Souls;


    public static string currentScene;

    public static void ChangeDamage(float _changeAmount)
    {
        damage += _changeAmount;
    }

    public static void ChangeMoveSpeed(float _changeAmount)
    {
        moveSpeed += _changeAmount;
    }

}
