using System.Collections;
using System.Collections.Generic;
//using Pathfinding;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] int _width;
    [SerializeField] int _height;
    [SerializeField] float _cellSize;
    [SerializeField] Transform _textParent;
    public Grid<GameObject> Grid;


    //private PathFinding _pathFinding;
    private void Start()
    {
        
        //_pathFinding = new PathFinding(_width, _height, _textParent, _cellSize);
    }
    public int NBPath = 1000;
    int nb_calculate = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //take the case unwalkable (no visuale + temporary)
            Vector3 mouseWorldPos = GetMouseWorldPos();
        }
    }

    public static Vector3 GetMouseWorldPos()
    {
        Vector3 vec = GetMouseWorldPosWithZ(Input.mousePosition, Camera.main);
        vec.z = 0;
        return vec;
    }
    public static Vector3 GetMouseWorldPosWithZ(Vector3 screenPos, Camera worldCam)
    {
        Vector3 worldPos = worldCam.ScreenToWorldPoint(screenPos);
        return worldPos;
    }
}
