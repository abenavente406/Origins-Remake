using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using OriginsLib.TileEngine;

namespace Origins_Remake.Util
{
    /// <summary>
    /// Reresents one node in the search space
    /// </summary>
    public class SearchNode
    {
        public SearchNode Parent { get; set; }
        public bool InOpenList { get; set; }
        public bool InClosedList { get; set; }
        public float DistanceToGoal { get; set; }
        public float DistanceTraveled { get; set; }
        public Point Position { get; set; }
        public bool Walkable { get; set; }
        public SearchNode[] Neighbors { get; set; }
    }

    public class Pathfinder
    {
        private static SearchNode[,] searchNodes;

        private static List<SearchNode> openList = new List<SearchNode>();
        private static List<SearchNode> closedList = new List<SearchNode>();

        private static int levelWidth;
        private static int levelHeight;

        public Pathfinder(TileMap map)
        {
            levelWidth = map.WidthInTiles;
            levelHeight = map.HeightInTiles;

            InitializeSearchNodes(map);
        }

        private static void InitializeSearchNodes(TileMap map)
        {
            searchNodes = new SearchNode[levelHeight, levelWidth];

            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    SearchNode node = new SearchNode()
                    {
                        Position = new Point(x, y),
                        Walkable = !map.GetIsCollision(x, y)
                    };

                    if (node.Walkable)
                    {
                        node.Neighbors = new SearchNode[4];
                        searchNodes[y, x] = node;
                    }
                }
            }

            for (int x = 0; x < levelWidth; x++)
            {

                for (int y = 0; y < levelHeight; y++)
                {

                    SearchNode node = searchNodes[y, x];



                    // We only want to look at the nodes that 

                    // our enemies can walk on.

                    if (node == null || node.Walkable == false)
                    {

                        continue;

                    }



                    // An array of all of the possible neighbors this 

                    // node could have. (We will ignore diagonals for now.)

                    Point[] neighbors = new Point[]

    {

        new Point (x, y - 1), // The node above the current node

        new Point (x, y + 1), // The node below the current node.

        new Point (x - 1, y), // The node left of the current node.

        new Point (x + 1, y), // The node right of the current node

    };



                    // We loop through each of the possible neighbors

                    for (int i = 0; i < neighbors.Length; i++)
                    {

                        Point position = neighbors[i];



                        // We need to make sure this neighbour is part of the level.

                        if (position.X < 0 || position.X > levelWidth - 1 ||

                            position.Y < 0 || position.Y > levelHeight - 1)
                        {

                            continue;

                        }



                        SearchNode neighbor = searchNodes[position.Y, position.X];


                        // We will only bother keeping a reference 

                        // to the nodes that can be walked on.

                        if (neighbor == null || neighbor.Walkable == false)
                        {
                            continue;
                        }

                        // Store a reference to the neighbor.
                        node.Neighbors[i] = neighbor;
                    }
                }
            }
        }

        private static void ResetSearchNodes()
        {
            openList.Clear();
            closedList.Clear();

            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelHeight; y++)
                {
                    SearchNode node = searchNodes[y, x];

                    if (node == null)
                    {
                        continue;
                    }

                    node.InOpenList = false;
                    node.InClosedList = false;

                    node.DistanceTraveled = float.MaxValue;
                    node.DistanceToGoal = float.MaxValue;
                }
            }
        }

        private static SearchNode FindBestNode()
        {
            SearchNode currentTile = openList[0];

            float smallestDistanceToGoal = float.MaxValue;

            // Find the closest node to the goal.
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].DistanceToGoal < smallestDistanceToGoal)
                {
                    currentTile = openList[i];
                    smallestDistanceToGoal = currentTile.DistanceToGoal;
                }
            }
            return currentTile;
        }

        private static List<Vector2> FindFinalPath(SearchNode startNode, SearchNode endNode)
        {
            closedList.Add(endNode);

            SearchNode parentTile = endNode.Parent;

            // Trace back through the nodes using the parent fields
            // to find the best path.
            while (parentTile != startNode)
            {
                closedList.Add(parentTile);
                parentTile = parentTile.Parent;
            }

            List<Vector2> finalPath = new List<Vector2>();

            // Reverse the path and transform into world space.
            for (int i = closedList.Count - 1; i >= 0; i--)
            {
                finalPath.Add(new Vector2(closedList[i].Position.X * 32,
                                          closedList[i].Position.Y * 32));
            }

            return finalPath;
        }

        public static List<Vector2> FindPath(Point startPoint, Point endPoint)
        {
            // Only try to find a path if the start and end points are different.
            if (startPoint == endPoint)
            {
                return new List<Vector2>();
            }

            if (startPoint == null)
            {
                return new List<Vector2>();
            }

            ResetSearchNodes();

            SearchNode startNode = searchNodes[startPoint.Y, startPoint.X];
            SearchNode endNode = searchNodes[endPoint.Y, endPoint.X];

            startNode.InOpenList = true;
            startNode.DistanceToGoal = Heuristic(startPoint, endPoint);
            startNode.DistanceTraveled = 0;

            openList.Add(startNode);

            while (openList.Count > 0)
            {
                SearchNode currentNode = FindBestNode();

                if (currentNode == null)
                {
                    break;
                }

                if (currentNode == endNode)
                {
                    return FindFinalPath(startNode, endNode);
                }

                for (int i = 0; i < currentNode.Neighbors.Length; i++)
                {
                    SearchNode neighbor = currentNode.Neighbors[i];

                    if (neighbor == null || neighbor.Walkable == false)
                    {
                        continue;
                    }

                    float distanceTraveled = currentNode.DistanceTraveled + 1;

                    float heuristic = Heuristic(neighbor.Position, endPoint);

                    if (neighbor.InOpenList == false && neighbor.InClosedList == false)
                    {
                        neighbor.DistanceTraveled = distanceTraveled;
                        neighbor.DistanceToGoal = distanceTraveled + heuristic;
                        neighbor.Parent = currentNode;
                        neighbor.InOpenList = true;
                        openList.Add(neighbor);
                    }
                    else if (neighbor.InOpenList || neighbor.InClosedList)
                    {
                        if (neighbor.DistanceTraveled > distanceTraveled)
                        {
                            neighbor.DistanceTraveled = distanceTraveled;
                            neighbor.DistanceToGoal = distanceTraveled + heuristic;

                            neighbor.Parent = currentNode;
                        }
                    }
                }

                openList.Remove(currentNode);
                currentNode.InClosedList = true;
            }

            return new List<Vector2>();
        }

        private static float Heuristic(Point point1, Point point2)
        {
            return Math.Abs(point1.X - point2.X) +
                   Math.Abs(point1.Y - point2.Y);
        }
    }
}
