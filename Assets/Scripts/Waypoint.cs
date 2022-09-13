using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Waypoint : MonoBehaviour
{
    public bool isExplored;
    public bool isPlaceable = true;
    public Waypoint exploredFrom;
    
    private int _gridSize = 10;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            FindObjectOfType<TowerFactory>().AddTower(this);
        }
    }

    public int GetGridSize()
    {
        return _gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / _gridSize),
            Mathf.RoundToInt(transform.position.z / _gridSize)
        );
    }

    public void SetTopColor(Color color)
    {
        transform.Find("Top").GetComponent<MeshRenderer>().material.color = color;
    }
}
