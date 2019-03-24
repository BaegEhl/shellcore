﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZoneManager : MonoBehaviour
{

    static List<Entity> targets;
    bool playing;

    public void AlertPlayers(int faction, string message) {
        foreach(Entity target in targets) {
            if(target as PlayerCore && target.faction == faction) {
                ((PlayerCore)target).alerter.showMessage(message, "clip_stationlost");
            }
        }
    }
    public void UpdateCounters()
    {
        if (playing)
        {
            Dictionary<int, int> alive = new Dictionary<int, int>();

            for (int i = 0; i < targets.Count; i++)
            {
                if (!alive.ContainsKey(targets[i].faction))
                    alive.Add(targets[i].faction, 0); // adds a dictionary field with current value of zero
                if (targets[i] && !targets[i].GetIsDead())
                    alive[targets[i].faction]++;
            }
            int factionCount = 0;
            foreach (var pair in alive)
            {
                if (pair.Value > 0)
                    factionCount++;
            }
            if (factionCount < 2)
            {
                int winningFaction = -1;
                foreach (var pair in alive)
                {
                    if (pair.Value > 0)
                        winningFaction = pair.Key;
                }
                foreach(Entity ent in targets) {
                    if(ent as PlayerCore && ent.faction == winningFaction) {
                        ResourceManager.PlayClipByID("clip_victory", ent.transform.position);
                    }
                }
                DialogueSystem.ShowPopup("Faction " + winningFaction + " won!");
                playing = false;
            }
        }
    }

    public void AddTarget(Entity target)
    {
        if (targets == null)
            targets = new List<Entity>();
        if (!playing)
            targets.Clear();
        if (target)
            playing = true;
        targets.Add(target);
    }

    public static Entity[] getTargets()
    {
        return targets != null ? targets.ToArray() : null;
    }
}
