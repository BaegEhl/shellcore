﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPropertyDisplay : MonoBehaviour
{
    public RectTransform rectTransform;
    public Dropdown factionDropdown;
    public InputField jsonField;
    public InputField nameField;
    public InputField idField;
    Item currentItem;

    void Start() 
    {
        if(!rectTransform) rectTransform = GetComponent<RectTransform>();
        factionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < FactionManager.FactionCount; i++)
        {
            string option = FactionManager.GetFactionName(i);
            options.Add(option);
        }
        factionDropdown.AddOptions(options);
    }

    void Update() 
    {
        var pos = Camera.main.WorldToScreenPoint(currentItem.pos);
        pos += new Vector3(300, 0);
        rectTransform.anchoredPosition = pos;
    }
    public void DisplayProperties(Item item) 
    {
        currentItem = item;
        rectTransform.gameObject.SetActive(true);
        var pos = Camera.main.WorldToScreenPoint(currentItem.pos);
        pos += new Vector3(300, 0);
        rectTransform.anchoredPosition = pos;
        factionDropdown.value = item.faction;
        jsonField.text = currentItem.shellcoreJSON;
        nameField.text = currentItem.name;
        idField.text = currentItem.ID;
    }

    public void UpdateFaction() 
    {
        currentItem.faction = factionDropdown.value;
        Debug.Log("updated faction: " + currentItem.faction );
    }

    public void UpdateBlueprint()
    {
        currentItem.shellcoreJSON = jsonField.text;
    }

    public void UpdateName()
    {
        currentItem.name = nameField.text;
        currentItem.ID = idField.text;
    }

    public void UpdatePatrolPath()
    {
        if(currentItem.patrolPath == null)
        {
            currentItem.patrolPath = new NodeEditorFramework.Standard.PathData();
            currentItem.patrolPath.waypoints = new List<NodeEditorFramework.Standard.PathData.Node>();
        }
        
        WorldCreatorCursor.finishPath += SetPath;
        WorldCreatorCursor.instance.pathDrawing(WorldCreatorCursor.WCCursorMode.Item, currentItem.patrolPath);
        rectTransform.gameObject.SetActive(false);
    }

    public void Hide() {
        rectTransform.gameObject.SetActive(false);
    }

    public void SetPath(NodeEditorFramework.Standard.PathData path)
    {
        rectTransform.gameObject.SetActive(true);
        currentItem.patrolPath = path;
        WorldCreatorCursor.finishPath -= SetPath;
    }

    public void SetRotation(int rotation)
    {
        currentItem.rotation = rotation;
        currentItem.obj.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
