using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritLadder : MonoBehaviour {

    [Header("TWEAKING")]
    public int bonusStartingSouls;

    [Header("Power Settings")]
    public List<Power> powers = new List<Power>();
    public List<Power> activePowers = new List<Power>();
    public int powersCost;
    public int costLadder;
    public int latestReference;
    public int powerReference = 0;

    [Header("Souls")]
    public int soulsInvested;
    public int soulsToNextPower;


    [Header("Elements Transform")]
    public Transform stepperTransform;
    public Transform basePositionTransform;
    public Transform upMostPositionTransform;

    private float distanceBetweenSteps;


    private void Start()
    {
        latestReference = 0;
        costLadder = powersCost;
        powerReference = -1;
        //debug bonus souls
        GameManager.soulsPossessed += bonusStartingSouls;
        //reset cursor positions
        stepperTransform.position = basePositionTransform.position;
        //get distances for cursor
        distanceBetweenSteps = (Vector2.Distance(basePositionTransform.position, upMostPositionTransform.position)) / (powers.Count * powersCost);
        //set souls to invest
        soulsToNextPower = powersCost;
    }

    public void AddPower(Power _power)
    {
        //take every Power parameter and apply it to game manager values
        GameManager.damage += _power.damageGiven;
        GameManager.dashDamage += _power.dashDamageGiven;
        GameManager.moveSpeed += _power.moveSpeedGiven;
        GameManager.attackSpeed += _power.attackSpeedGiven;
        GameManager.aoeRange += _power.aoeRangeGiven;
        GameManager.shieldRegen += _power.shieldRegenGiven;
        //enable powers if they aren't enabled
        if(_power.enablesCombo && !GameManager.canCombo)
        {
            GameManager.canCombo = true;
        }
        if (_power.enablesFirePower && !GameManager.hasFirePower)
        {
            GameManager.hasFirePower = true;
        }
        if (_power.enablesFireStoring && !GameManager.canStoreFire)
        {
            GameManager.canStoreFire = true;
        }
        if (_power.enablesShield && !GameManager.hasShield)
        {
            GameManager.hasShield = true;
        }
        if (_power.enablesShieldRegen && !GameManager.shieldRegens)
        {
            GameManager.shieldRegens = true;
        }
        //store this power in active power list, give its reference
        activePowers.Add(_power);
    }

    public void RemoveLatestPower()
    {
        Power latestPower = activePowers[powerReference];
        //take every Power parameter and remove it from game manager values
        GameManager.damage -= latestPower.damageGiven;
        GameManager.dashDamage -= latestPower.dashDamageGiven;
        GameManager.moveSpeed -= latestPower.moveSpeedGiven;
        GameManager.attackSpeed -= latestPower.attackSpeedGiven;
        GameManager.aoeRange -= latestPower.aoeRangeGiven;
        GameManager.shieldRegen -= latestPower.shieldRegenGiven;
        //enable powers if they aren't enabled
        if (latestPower.enablesCombo && GameManager.canCombo)
        {
            GameManager.canCombo = false;
        }
        if (latestPower.enablesFirePower && GameManager.hasFirePower)
        {
            GameManager.hasFirePower = false;
        }
        if (latestPower.enablesFireStoring && GameManager.canStoreFire)
        {
            GameManager.canStoreFire = false;
        }
        if (latestPower.enablesShield && GameManager.hasShield)
        {
            GameManager.hasShield = false;
        }
        if (latestPower.enablesShieldRegen && GameManager.shieldRegens)
        {
            GameManager.shieldRegens = false;
        }
        activePowers.Remove(activePowers[powerReference]);
    }

    public void GoUp()
    {
        if(GameManager.soulsPossessed >= 1)
        {
            if (soulsInvested < powersCost * powers.Count)
            {
                GameManager.soulsPossessed -= 1;
                stepperTransform.position = new Vector3(stepperTransform.position.x, stepperTransform.position.y + distanceBetweenSteps, stepperTransform.position.z);
                soulsInvested++;
            }
            else
            {
                stepperTransform.position = upMostPositionTransform.position;
            }
        }

    }

    public void GoDown()
    {
        if (soulsInvested > 0)
        {
            GameManager.soulsPossessed += 1;
            stepperTransform.position = new Vector3(stepperTransform.position.x, stepperTransform.position.y - distanceBetweenSteps, stepperTransform.position.z);
            soulsInvested--;
        }
        else
        {
            stepperTransform.position = basePositionTransform.position;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown("a"))
        {
            GoUp();
        }
        else if (Input.GetKeyDown("e"))
        {
            GoDown();
        }
    }

    private void FixedUpdate()
    {
        if(soulsInvested >= costLadder)
        {
            AddPower(powers[powerReference + 1]);
            costLadder += powersCost;
            powerReference += 1;
            latestReference += powersCost;
        }
        else if(soulsInvested < latestReference)
        {
            RemoveLatestPower();
            costLadder -= powersCost;
            powerReference -= 1;
            latestReference -= powersCost;
        }
    }
}
