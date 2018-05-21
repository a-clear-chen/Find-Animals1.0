using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boolean2D
{
    [HideInInspector]
    public bool[] array;
    [HideInInspector]
    public int oneSideSize;
    public Boolean2D(int size)
    {
        array = new bool[size * size];
        oneSideSize = size;
    }

    public Boolean2D(Boolean2D copy)
    {
        array = new bool[copy.oneSideSize * copy.oneSideSize];
        oneSideSize = copy.oneSideSize;
        for (int i = 0; i < array.Length; i++)
            array[i] = copy.array[i];
    }

    public bool IsEmpty()
    {
        int count = 0;
        foreach (var value in array)
        {
            if (value)
                count++;
            if (count > 1)
                return false;
        }
        return true;
    }

    public bool SetElement(int i, int j, bool value)
    {
        if (i >= 0 && i < oneSideSize && j >= 0 && j < oneSideSize)
        {
            array[i * oneSideSize + j] = value;
            return true;
        }
        else
            return false;
    }
    public bool GetElement(int i, int j, out bool value)
    {
        if (i >= 0 && i < oneSideSize && j >= 0 && j < oneSideSize)
        {
            value = array[i * oneSideSize + j];
            return true;
        }
        else
        {
            value = false;
            return false;
        }
    }
}

[System.Serializable]

public class RotationMap : MonoBehaviour {


    public Vector2 currentPosition;

    public int mapSize;

    public bool useObliqueLine;

    public bool canWalkBack;

    [HideInInspector]
    public int backUpSize;
    [HideInInspector]
    public Boolean2D pointArray;

    private Boolean2D _pointArray;
    private List<List<int>> _mapList;
    private Vector2 _position = Vector2.down;

    void Awake()
    {
        ResetMap();
        if (!canWalkBack)
            Debug.Log("canWalkBack取消时，请确保无孤立点且路径唯一，否则将出错！");
    }

    public void ResetMap()
    {
        if (_position == Vector2.down)
            _position = currentPosition;
        else
            currentPosition = _position;

        _pointArray = new Boolean2D(pointArray);
        _mapList = GetListFromBoolean2D(pointArray);
    }

    public List<int> GetArrayFromMap()
    {
        if (_pointArray.IsEmpty())
            _pointArray = new Boolean2D(pointArray);

        List<List<int>> mapList = canWalkBack ? _mapList : GetListFromBoolean2D(_pointArray);

        if (!canWalkBack)
            _pointArray.SetElement((int)currentPosition.x, (int)currentPosition.y, false);

        int index = (int)(currentPosition.x * mapSize + currentPosition.y);

        if (mapList[index] == null)
        {
            Debug.LogError("请将PositionInMap的初始值设在路径上！");
            return null;
        }

        return mapList[index];
    }

    public void SetPositionToNext(int index)
    {
        switch (index)
        {
            case 0:
                currentPosition.x--;
                break;
            case 1:
                currentPosition.x--;
                currentPosition.y++;
                break;
            case 2:
                currentPosition.y++;
                break;
            case 3:
                currentPosition.x++;
                currentPosition.y++;
                break;
            case 4:
                currentPosition.x++;
                break;
            case 5:
                currentPosition.x++;
                currentPosition.y--;
                break;
            case 6:
                currentPosition.y--;
                break;
            case 7:
                currentPosition.x--;
                currentPosition.y--;
                break;
        }
    }


    List<List<int>> GetListFromBoolean2D(Boolean2D pArray)
    {
        List<List<int>> mapList = new List<List<int>>();

        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                bool value;

                if (pArray.GetElement(i, j, out value) && !value)
                {
                    mapList.Add(null);
                    continue;
                }
                List<int> resultList = new List<int>();

                AddResult(i - 1, j, resultList, pArray, 0);
                AddResult(i, j + 1, resultList, pArray, 2);
                AddResult(i + 1, j, resultList, pArray, 4);
                AddResult(i, j - 1, resultList, pArray, 6);

                if (useObliqueLine)
                {
                    AddResult(i - 1, j + 1, resultList, pArray, 1);
                    AddResult(i + 1, j + 1, resultList, pArray, 3);
                    AddResult(i + 1, j - 1, resultList, pArray, 5);
                    AddResult(i - 1, j - 1, resultList, pArray, 7);
                }

                mapList.Add(resultList);
            }
        }
        return mapList;
    }

    void AddResult(int i, int j, List<int> list, Boolean2D pArray, int result)
    {
        bool value;
        if (pArray.GetElement(i, j, out value) && value)
            list.Add(result);
    }
}
