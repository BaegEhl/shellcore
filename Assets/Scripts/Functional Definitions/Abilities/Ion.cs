﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ion : WeaponAbility {
    bool firing = false;
    IonLineController lineController;
    protected override void Awake()
    {
        GameObject gObj = new GameObject("Line Renderer");
        gObj.transform.SetParent(this.transform, false);
        lineController = gObj.AddComponent<IonLineController>();
        cooldownDuration = CDRemaining = 15;
        ID = AbilityID.Ion;
        abilityName = "Ion";
        range = 15;
        category = Entity.EntityCategory.All;
        base.Awake();
    }

    

    protected override void Start() {
        base.Start();
        lineController.Init(targetingSystem, Core, range, part, abilityTier);
    }

    public override void SetDestroyed(bool destroyed)
    {
        if(destroyed && lineController)
        {
            Destroy(lineController.gameObject);
        }
        base.SetDestroyed(true);
    }

    void OnDestroy()
    {
        if(lineController)
            Destroy(lineController.gameObject);
    }

    void LateUpdate()
    {
        if(lineController)
        {
            
        }  
    }

    void Update()
    {
        
        
        /*
        
        */
    }

    

    Vector3 GetMousePos()
    {
        Vector3 vec = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, CameraScript.zLevel));
        vec.z = 0;
        return vec;
    }
 
   

    protected override bool Execute(Vector3 victimPos)
    {
        /*
        if(targetingSystem.GetTarget() && !firing)
        {
            line.positionCount = 2;
            line.SetPosition(0, transform.position);
            line.SetPosition(1, targetingSystem.GetTarget().position);
            firing = true;
        }
        */
            
        if(Core.RequestGCD()) {
            if (targetingSystem.GetTarget() && targetingSystem.GetTarget().GetComponent<IDamageable>() != null) // check if there is actually a target, do not fire if there is not
            {
                if(!lineController.GetFiring())
                {
                    AudioManager.PlayClipByID("clip_bullet2", transform.position);
                    lineController.StartFiring(5);
                    isOnCD = true; // set booleans and return
                    return true;
                }
                
            }
            return false;
        } return false;
    }
}