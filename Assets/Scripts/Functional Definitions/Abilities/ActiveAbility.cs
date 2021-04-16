﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlinkOnUse {}
public interface IChargeOnUseThenBlink {}

/// <summary>
/// Any ability that has a specific active time is an active ability (abilities that are simply click for effect that does not expire are not active abilities)
/// </summary>
public abstract class ActiveAbility : Ability
{
    protected bool isActive = false; // used to check if the ability has been activated
    protected float activeTimeRemaining; // how much active time is remaining on the ability
    protected float activeDuration; // the duration it is active for
    protected float activationTime;
    protected float activationDelay = 0f;

    /// <summary>
    /// Initialization of every active ability
    /// </summary>
    protected override void Awake() {
        base.Awake(); // base awake
        abilityName = "Active Ability";
    }
    /// <summary>
    /// Get the active time remaining
    /// </summary>
    /// <returns>The active time remaining</returns>
    public override float GetActiveTimeRemaining()
    {
        if (isActive) // active
        {
            return activeTimeRemaining; // return active time remaining
        }
        else return 0; // not active
    }

    public bool TrueActive
    {
        get
        {
            return isActive && Time.time > activationTime && !isDestroyed;
        }
    }

    public override void SetDestroyed(bool input)
    {
        base.SetDestroyed(input);
        if (input && isActive && TrueActive) Deactivate();
    }

    private void OnDestroy()
    {
        if(!isDestroyed)
            SetDestroyed(true);
        if (TrueActive)
            Deactivate();
    }

    /// <summary>
    /// Called when active time hits 0, used to rollback whatever change was done on the core
    /// </summary>
    virtual public void Deactivate() { 
        if(this as IBlinkOnUse != null || this as IChargeOnUseThenBlink != null)
        {
            SetIndicatorBlink(false);
        }
        isActive = false;
    }

    override protected void Execute() { 
        if(this as IBlinkOnUse != null)
        {
            SetIndicatorBlink(true);
        }
        if(this as IChargeOnUseThenBlink != null)
        {
            ToggleIndicator();
        }
        base.Execute();
    }

    /// <summary>
    /// Override on tick that accounts for actives for players
    /// </summary>
    /// <param name="key">Associated string on the button to push to activate</param>
    public override void Tick(int key)
    {
        if(isDestroyed)
        {
            return; // Part has been destroyed, ability can't be used
        }

        if(this as IChargeOnUseThenBlink != null)
        {
            if (TrueActive)
            {
                SetIndicatorBlink(true);
            }
        }
        
        if (isActive)
        {
            TickDown(activeDuration, ref activeTimeRemaining, ref isActive); // tick the active time
            if (!isActive) // if the boolean got flipped deactivate
            {
                Deactivate(); // deactivate
            }
        }
        if (isOnCD) // on cooldown
        {
            TickDown(cooldownDuration, ref CDRemaining, ref isOnCD); // tick the cooldown time
        }
        else if (!isActive) { // if not active it can run through the base ability behaviour
            base.Tick(key); // base tick
        }
    }
}

