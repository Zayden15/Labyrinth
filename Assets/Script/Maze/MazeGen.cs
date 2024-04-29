using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.AI.Navigation;


public class MazeGen : MonoBehaviour
{
    [SerializeField] private Maze _mazeCellPrefab;
    [SerializeField] private int _mazeWidth;
    [SerializeField] private int _mazeDepth;
    [SerializeField] private Maze[,] _mazeGrid;
    

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.SetActive(false);
        }


        _mazeGrid = new Maze[_mazeWidth, _mazeDepth];
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                _mazeGrid[x, z] = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity, transform);
                _mazeGrid[x, z].transform.localPosition = new Vector3(x, 0, z);
            }
        }

        //yield return GenerateMaze(null, _mazeGrid[0, 0]);
        GenerateMaze(null, _mazeGrid[0, 0]);
        GetComponent<NavMeshSurface>().BuildNavMesh();
        
        if (player != null) 
        {
            player.SetActive(true);
        }

    }

    private void GenerateMaze(Maze prevCell, Maze currCell)
    {
        currCell.Visit();
        ClearWalls(prevCell, currCell);

        //yield return new WaitForSeconds(0.05f);

        Maze nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currCell);

            if (nextCell != null)
            {
                GenerateMaze(currCell, nextCell);
                //yield return GenerateMaze(currCell, nextCell);
            }
        } while (nextCell != null);

    }

    private Maze GetNextUnvisitedCell(Maze currCell)
    {
        var unvisitedCells = GetUnvisitedCell(currCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<Maze> GetUnvisitedCell(Maze currCell)
    {
        int x = (int)currCell.transform.localPosition.x;
        int z = (int)currCell.transform.localPosition.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];

            if (cellToRight.isVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.isVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.isVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.isVisited == false)
            {
                yield return cellToBack;
            }
        }
    }


    private void ClearWalls(Maze prevCell, Maze currCell)
    {
        if (prevCell == null) { return; }

        if (prevCell.transform.localPosition.x < currCell.transform.localPosition.x)
        {
            prevCell.ClearRightWall();
            currCell.ClearLeftWall();
            return;
        }

        if (prevCell.transform.localPosition.x > currCell.transform.localPosition.x)
        {
            prevCell.ClearLeftWall();
            currCell.ClearRightWall();
            return;
        }

        if (prevCell.transform.localPosition.z < currCell.transform.localPosition.z)
        {
            prevCell.ClearFrontWall();
            currCell.ClearBackWall();
            return;
        }

        if (prevCell.transform.localPosition.z > currCell.transform.localPosition.z)
        {
            prevCell.ClearBackWall();
            currCell.ClearFrontWall();
            return;
        }
    }

    public int GetWidth()
    {
        return _mazeWidth;
    }

    public int GetHeight()
    {
        return _mazeDepth;
    }
}
