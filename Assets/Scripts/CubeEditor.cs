using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    private Waypoint _waypoint;

    private void Awake()
    {
        _waypoint = GetComponent<Waypoint>();
    }

    private void Update()
    {
        UpdateLabel();
        SnapToGrid();
    }

    private void UpdateLabel()
    {
        int gridSize = _waypoint.GetGridSize();
        transform.position = new Vector3(_waypoint.GetGridPos().x * gridSize, 0f, _waypoint.GetGridPos().y * gridSize);
    }
    
    private void SnapToGrid()
    {
        var labelName = _waypoint.GetGridPos().x + ", " + _waypoint.GetGridPos().y;
        gameObject.name = labelName;
    }
}
