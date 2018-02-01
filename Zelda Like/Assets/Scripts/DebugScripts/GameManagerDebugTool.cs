using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManagerDebugTool : MonoBehaviour {

    public TextMeshProUGUI damageText;
    public TextMeshProUGUI dashDamageText;
    public TextMeshProUGUI moveSpeedText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI comboEnabledText;
    public TextMeshProUGUI aOERangeText;
    public TextMeshProUGUI firePowerEnabledText;
    public TextMeshProUGUI fireStoringEnabledText;
    public TextMeshProUGUI shieldEnabledText;
    public TextMeshProUGUI shieldRegenText;
    public TextMeshProUGUI shieldRegenEnabledText;
    public TextMeshProUGUI soulsPossessedText;
    public TextMeshProUGUI currentSceneText;

    private float damage;
    private float dashDamage;
    private float moveSpeed;
    private float attackSpeed;
    private bool canCombo;
    private float aoeRange;
    private bool hasFirePower;
    private bool canStoreFire;
    private bool hasShield;
    private float shieldRegen;
    private bool shieldRegens;
    private int soulsPossessed;
    private string currentScene;

    private void Start()
    {
        //give this scene's name to game manager
        GameManager.currentScene = SceneManager.GetActiveScene().name;
        //set starting values
        damage = GameManager.damage;
        damageText.text = ("Damage : " + damage.ToString());
        dashDamage = GameManager.dashDamage;
        dashDamageText.text = ("Dash Damage : " + dashDamage.ToString());
        moveSpeed = GameManager.moveSpeed;
        moveSpeedText.text = ("Move Speed : " + moveSpeed.ToString());
        attackSpeed = GameManager.attackSpeed;
        attackSpeedText.text = ("Attack Speed : " + attackSpeed.ToString());
        canCombo = GameManager.canCombo;
        comboEnabledText.text = ("Combo Enabled : " + canCombo);
        aoeRange = GameManager.aoeRange;
        aOERangeText.text = ("AOE Range : " + aoeRange.ToString());
        hasFirePower = GameManager.hasFirePower;
        firePowerEnabledText.text = ("Fire Power Enabled : " + hasFirePower);
        canStoreFire = GameManager.canStoreFire;
        fireStoringEnabledText.text = ("Fire Storing Enabled : " + canStoreFire);
        hasShield = GameManager.hasShield;
        shieldEnabledText.text = ("Shield Enabled : " + hasShield);
        shieldRegen = GameManager.shieldRegen;
        shieldRegenText.text = ("Shield Regen : " + shieldRegen.ToString());
        shieldRegens = GameManager.shieldRegens;
        shieldRegenEnabledText.text = ("Shiled Regen Enabled" + shieldRegens);
        soulsPossessed = GameManager.soulsPossessed;
        soulsPossessedText.text = ("Souls Possessed : " + soulsPossessed.ToString());
        currentScene = GameManager.currentScene;
        currentSceneText.text = ("Scene : " + currentScene);
    }


    void FixedUpdate () {
        //dynamically change according to game manager
        if(damage != GameManager.damage)
        {
            damage = GameManager.damage;
            damageText.text = ("Damage : " + damage.ToString());
        }
        if (dashDamage != GameManager.dashDamage)
        {
            dashDamage = GameManager.dashDamage;
            dashDamageText.text = ("Dash Damage : " + dashDamage.ToString());
        }
        if (moveSpeed != GameManager.moveSpeed)
        {
            moveSpeed = GameManager.moveSpeed;
            moveSpeedText.text = ("Move Speed : " + moveSpeed.ToString());
        }
        if (attackSpeed != GameManager.attackSpeed)
        {
            attackSpeed = GameManager.attackSpeed;
            attackSpeedText.text = ("Attack Speed : " + attackSpeed.ToString());
        }
        if (canCombo != GameManager.canCombo)
        {
            canCombo = GameManager.canCombo;
            comboEnabledText.text = ("Combo Enabled : " + canCombo);
        }
        if (aoeRange != GameManager.aoeRange)
        {
            aoeRange = GameManager.aoeRange;
            aOERangeText.text = ("AOE Range : " + aoeRange.ToString());
        }
        if (hasFirePower != GameManager.hasFirePower)
        {
            hasFirePower = GameManager.hasFirePower;
            firePowerEnabledText.text = ("Fire Power Enabled : " + hasFirePower);
        }
        if (canStoreFire != GameManager.canStoreFire)
        {
            canStoreFire = GameManager.canStoreFire;
            fireStoringEnabledText.text = ("Fire Storing Enabled : " + canStoreFire);
        }
        if (hasShield != GameManager.hasShield)
        {
            hasShield = GameManager.hasShield;
            shieldEnabledText.text = ("Shield Enabled : " + hasShield);
        }
        if (shieldRegen != GameManager.shieldRegen)
        {
            shieldRegen = GameManager.shieldRegen;
            shieldRegenText.text = ("Shield Regen : " + shieldRegen.ToString());
        }
        if (shieldRegens != GameManager.shieldRegens)
        {
            shieldRegens = GameManager.shieldRegens;
            shieldRegenEnabledText.text = ("Shiled Regen Enabled : " + shieldRegens);
        }
        if (soulsPossessed != GameManager.soulsPossessed)
        {
            soulsPossessed = GameManager.soulsPossessed;
            soulsPossessedText.text = ("Souls Possessed : " + soulsPossessed.ToString());
        }
        if (currentScene != GameManager.currentScene)
        {
            currentScene = GameManager.currentScene;
            currentSceneText.text = ("Scene : " + currentScene);
        }
    }
}
