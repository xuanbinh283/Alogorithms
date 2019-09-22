using System;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    class Program
    {
        public class Graph
        {
            public Dictionary<int, HashSet<int>> adjecancyList = new Dictionary<int, HashSet<int>>();
            private int size = 0;

            public Graph(int v)
            {
                size = v;
                for (int i = 0; i < v; i++)
                {
                    adjecancyList.Add(i, new HashSet<int>());
                }
            }
            public void AddEdge(int from, int to)
            {
                adjecancyList[from].Add(to);
            }

            #region DFS - recursive & no recursive
            public void PrintDfsUsingStack(int start)
            {
                // validate
                if (!adjecancyList.ContainsKey(start))
                    return;

                var visited = new bool[size];

                var stack = new Stack<int>();
                stack.Push(start);

                while (stack.Count > 0)
                {
                    int cur = stack.Pop();
                    visited[cur] = true;
                    Console.Write(cur + " ");
                    foreach (int i in adjecancyList[cur])
                    {
                        if (visited[i] == false)
                            stack.Push(i);
                    }
                }
            }

            public void PrintDfsUsingRecursive(int start)
            {
                var visited = new bool[size];
                PrintDfsUsingRecursiveUtil(start, visited);
            }

            private void PrintDfsUsingRecursiveUtil(int node, bool[] visited)
            {
                if (visited[node] == true)
                    return;
                Console.Write(node + " ");
                visited[node] = true;
                foreach (var i in adjecancyList[node])
                {
                    if (visited[i] == false)
                        PrintDfsUsingRecursiveUtil(i, visited);
                }
            }

            public void PrintAllPathsIterative(int start, int end)
            {
                if (!adjecancyList.ContainsKey(start) || !adjecancyList.ContainsKey(end))
                    return;

                var visited = new bool[size];

                var stack = new Stack<int>();
                stack.Push(start);

                while (stack.Count > 0)
                {
                    int cur = stack.Pop();
                    visited[cur] = true;
                    Console.Write(cur + " ");
                    foreach (int i in adjecancyList[cur])
                    {
                        if (visited[i] == false)
                            stack.Push(i);
                    }
                }
            }

            public void PrintAllPathsReursive(int start, int end)
            {
                var visited = new bool[size];
                var path = new List<int>();

                //visited[start] = true;
                //path.Add(start);
                PrintAllPathsReursiveUtil(start, end, visited, path);
            }

            private void PrintAllPathsReursiveUtil(int start, int end, bool[] visited, List<int> path)
            {
                visited[start] = true;
                path.Add(start);

                if (start == end)
                {
                    // print path
                    Console.WriteLine(String.Join(" ", path));
                    visited[start] = false;
                    path.RemoveAt(path.Count - 1);
                    return;
                }

                foreach (int i in adjecancyList[start])
                {
                    if (!visited[i])
                        PrintAllPathsReursiveUtil(i, end, visited, path);
                }

                visited[start] = false;
                path.RemoveAt(path.Count - 1);
            }

            public bool HasCicle()
            {
                var isCicle = false;



                return isCicle;
            }
            #endregion


            #region BFS
            public void PrintBfsQueue(int start)
            {
                var visited = new bool[size];
                var queue = new Queue<int>();

                queue.Enqueue(start);

                while (queue.Count > 0)
                {
                    var cur = queue.Dequeue();
                    Console.Write(cur + " ");
                    visited[cur] = true;
                    foreach (var item in adjecancyList[cur])
                    {
                        if (!visited[item])
                            queue.Enqueue(item);
                    }
                }
            }
            #endregion
        }

        public class GraphMatrix
        {
            private int _row = 0;
            private int _col = 0;
            private int[,] grid;

            public GraphMatrix(int[,] _grid)
            {
                _row = _grid.GetLength(0);
                _col = _grid.GetLength(1);
                grid = _grid;
            }

            public void AddNode(int row, int col)
            {
                grid[row, col] = 1;
            }

            /*
             run DFS for all nodes
             mark visited node
             count++ when there is an island: finish one dfs recursive loop
             */
            public int NumberOfIslands()
            {
                var visited = new bool[_row, _col];
                var count = 0;
                for (int i = 0; i < _row; i++)
                {
                    for (int j = 0; j < _col; j++)
                    {
                        if (!visited[i, j] && grid[i,j] == 1)
                        {
                            NumberOfIslandsRecursive(i, j, visited);
                            ++count;
                        }
                    }
                }
                return count;
            }

            private void NumberOfIslandsRecursive(int row, int col, bool[,] visited)
            {
                visited[row, col] = true;
                //run recursive all the neighbors
                // neighbor: valid (<row, <col); not itself; row, col: -1 -> +1
                for (int i = row - 1; i <= row + 1; i++)
                {
                    for (int j = col - 1; j <= col + 1; j++)
                    {
                        //check valid
                        if (i >= 0 && i < _row && j >= 0 && j < _col && !(i == row && j == col))
                        {
                            if (!visited[i, j] && grid[i, j] == 1) {
                                NumberOfIslandsRecursive(i, j, visited);
                            }

                        }
                    }
                }
            }

            private int numPaths = 0;

            public int NumberOfUniquePathsWithObstacles(int[] start, int[] end)
            {
                var count = 0;

                var neighbors = new List<int[]>();
                if (start[0] + 1 < _row && grid[start[0]+1,start[1]] == 0)
                {
                    neighbors.Add(new int[2] { start[0] + 1, start[1]});
                }
                if (start[1] + 1 < _col && grid[start[0], start[1]+1] == 0)
                {
                    neighbors.Add(new int[2] { start[0], start[1] + 1 });
                }
                foreach (var item in neighbors)
                {
                    NumberOfUniquePathsWithObstaclesUtil(item, end);
                    count++;
                }
                
                return count;
            }

            public void NumberOfUniquePathsWithObstaclesUtil(int[] start, int[] end)
            {
                if (start[0] == end[0] && start[1] == end[1])
                {
                    Console.Write("a ");
                    return;
                }

                var neighbors = new List<int[]>();
                if (start[0] + 1 < _row && grid[start[0] + 1, start[1]] == 0)
                {
                    neighbors.Add(new int[2] { start[0] + 1, start[1] });
                }
                if (start[1] + 1 < _col && grid[start[0], start[1] + 1] == 0)
                {
                    neighbors.Add(new int[2] { start[0], start[1] + 1 });
                }
                foreach (var item in neighbors)
                {
                    NumberOfUniquePathsWithObstaclesUtil(item, end);
                }

            }
        }

        static void TestGraph()
        {
            var g = new Graph(4);
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(0, 3);
            g.AddEdge(2, 0);
            g.AddEdge(2, 1);
            g.AddEdge(1, 3);


            Console.WriteLine("\nPrintDfsUsingStack:");
            g.PrintDfsUsingStack(2);

            Console.WriteLine("\nPrintDfsUsingRecursive:");
            g.PrintDfsUsingRecursive(2);

            Console.WriteLine("\nPrintBfsQueue:");
            g.PrintBfsQueue(2);

            Console.WriteLine("\nPrintAllPathsReursive:");
            g.PrintAllPathsReursive(2, 3);
        }

        static void TestGraphCicle()
        {
            var g = new Graph(4);
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            g.AddEdge(3, 3);

            Console.WriteLine("\nhas cicle:" + g.HasCicle());


        }


        static void TestMatrixGraph()
        {
            var grid = new int[5, 5] {
                { 1, 1, 0, 0, 0 },
                { 0, 1, 0, 0, 1 },
                { 1, 0, 0, 1, 1 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 1 }
            };

            var gm = new GraphMatrix(grid);

            Console.WriteLine("Number of island is: " + gm.NumberOfIslands());

        }

        static void TestMaxtricFindUniquePaths()
        {
            var grid = new int[3, 3] {
                { 0, 0, 0},
                { 0, 0, 1},
                { 0, 0, 0}
            };

            var gm = new GraphMatrix(grid);
            Console.WriteLine("Number of island is: " + gm.NumberOfUniquePathsWithObstacles(new int[2]{ 0,0}, new int[2]{2,2}));

        }

        static void Main(string[] args)
        {

            //TestGraph();

            TestMaxtricFindUniquePaths();

            Console.ReadLine();
        }
    }
}
