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

            #region Graph Cycle

            /*
             Loop all nodes
                Check each node if has cycle by using a path
                    -> if has, exit and return true
                    -> if not, mark visited and continous 
                        mark visited will be happened in the adjcances also

             */
            public bool HasCicleDfs()
            {
                var visited = new bool[size]; //had checked the cycle
                var path = new List<int>(); //or use a hashset or stack since we will use the removing the last item method

                for(var i = 0; i < size; i++)
                {
                    //run dfs
                    if (HasCicleDfsUtil(i, visited, path))
                        return true;
                }

                return false;
            }

            private bool HasCicleDfsUtil(int item, bool[] visited, List<int> path)
            {
                if (path.Contains(item))
                    return true;

                if (visited[item])
                    return false;
                
                visited[item] = true;
                path.Add(item);

                foreach (var i in adjecancyList[item])
                {
                    if (HasCicleDfsUtil(i, visited, path))
                        return true;
                }
                path.RemoveAt(path.Count-1);
                return false;
            }


            #endregion
        }

        public class GraphMatrix
        {
            private int _ROW = 0;
            private int _COL = 0;
            private int[,] grid;

            public GraphMatrix(int[,] _grid)
            {
                _ROW = _grid.GetLength(0);
                _COL = _grid.GetLength(1);
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
                var visited = new bool[_ROW, _COL];
                var count = 0;
                for (int i = 0; i < _ROW; i++)
                {
                    for (int j = 0; j < _COL; j++)
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
                        if (i >= 0 && i < _ROW && j >= 0 && j < _COL && !(i == row && j == col))
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
                if (start[0] + 1 < _ROW && grid[start[0]+1,start[1]] == 0)
                {
                    neighbors.Add(new int[2] { start[0] + 1, start[1]});
                }
                if (start[1] + 1 < _COL && grid[start[0], start[1]+1] == 0)
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
                if (start[0] + 1 < _ROW && grid[start[0] + 1, start[1]] == 0)
                {
                    neighbors.Add(new int[2] { start[0] + 1, start[1] });
                }
                if (start[1] + 1 < _COL && grid[start[0], start[1] + 1] == 0)
                {
                    neighbors.Add(new int[2] { start[0], start[1] + 1 });
                }
                foreach (var item in neighbors)
                {
                    NumberOfUniquePathsWithObstaclesUtil(item, end);
                }

            }

            public int MinFallingPath()
            {
                var dict = new Dictionary<int[],int> ();
                int min = 0;
                for (int i = 0; i < _COL; i++)
                {
                    var total = MinFallingPathUtil(0, i, dict);
                    
                    if (min == 0 || min > total)
                        min = total;
                }
                return min;
            }
            private int MinFallingPathUtil(int row, int col, Dictionary<int[], int> dict)
            {
                if (row == _ROW-1)
                    return grid[row, col];
                var min = 0;
                for (int i = col-1; i <= col+1; i++)
                {
                    if(i>=0 && i < _COL)
                    {
                        if (dict.ContainsKey(new int[] { row + 1, i}))
                        {
                            return dict[new int[] { row + 1, i }];
                        }
                        var total = MinFallingPathUtil(row + 1, i, dict);
                        if (min == 0 || min > total)
                            min = total;

                    }
                }
                dict.Add(new int[2] { row, col }, min + grid[row, col]);

                return min + grid[row, col];
            }
        }

        public class GraphWithWeight {
            public Dictionary<int, Dictionary<int, int>> adjecancyList = new Dictionary<int, Dictionary<int, int>>();
            private int size = 0;

            public GraphWithWeight(int v)
            {
                size = v;
                for (int i = 0; i < v; i++)
                {
                    adjecancyList.Add(i, new Dictionary<int, int>());
                }
            }
            public void AddEdge(int from, int to, int w)
            {
                adjecancyList[from].Add(to, w);
                adjecancyList[to].Add(from, w);
            }

            public bool PathMoreThanK(int start, int k) {

                var visited = new bool[size];
                var pathAndWeight = new List<KeyValuePair<int, int>>();

                visited[start] = true;
                pathAndWeight.Add(new KeyValuePair<int, int>(start, 0));
                foreach (var item in adjecancyList[start])
                {
                    if (PathMoreThanKUtil(item, k, visited, pathAndWeight))
                        return true;
                }

                return false;
            }

            private bool PathMoreThanKUtil(KeyValuePair<int, int> start, int k, bool[] visited, List<KeyValuePair<int, int>> pathAndWeight)
            {
                var weight = pathAndWeight[pathAndWeight.Count - 1].Value + start.Value;
               
                if (weight > k) {
                    return true;
                }

                visited[start.Key] = true;

                pathAndWeight.Add(new KeyValuePair<int, int>(start.Key, weight));

                //expose adjcances
                foreach (var item in adjecancyList[start.Key])
                {
                    if (!visited[item.Key])
                        if (PathMoreThanKUtil(item, k, visited, pathAndWeight))
                            return true;
                }
                pathAndWeight.RemoveAt(pathAndWeight.Count - 1);
                visited[start.Key] = false;

                return false;
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
            var g = new Graph(6);
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            //g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            //g.AddEdge(3, 3);
            g.AddEdge(3, 4);
            g.AddEdge(3, 5);
            g.AddEdge(5, 4);
            g.AddEdge(5, 1);


            Console.WriteLine("\nhas cicle:" + g.HasCicleDfs());


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

        static void TestMinFallingPath()
        {
            var grid = new int[3, 3] {
                { 1, 2, 3},
                { 4, 1, 1},
                { 7, 8, 9}
            };

            var gm = new GraphMatrix(grid);
            Console.WriteLine("Min Falling Path is: " + gm.MinFallingPath());
        }

        static void TestPathMoreThanK() {
            var g = new GraphWithWeight(9);
            g.AddEdge(0, 1, 4);
            g.AddEdge(0, 7, 8);
            g.AddEdge(1, 2, 8);
            g.AddEdge(1, 7, 11);
            g.AddEdge(2, 3, 7);
            g.AddEdge(2, 8, 2);
            g.AddEdge(2, 5, 4);
            g.AddEdge(3, 4, 9);
            g.AddEdge(3, 5, 14);
            g.AddEdge(4, 5, 10);
            g.AddEdge(5, 6, 2);
            g.AddEdge(6, 7, 1);
            g.AddEdge(6, 8, 6);
            g.AddEdge(7, 8, 7);

            var k = 58;

            Console.WriteLine($"Has path more than {k}: " + g.PathMoreThanK(0,k));

        }

        static void Main(string[] args)
        {

            //TestGraph();

            //TestMaxtricFindUniquePaths();

            //TestMinFallingPath();

            //TestGraphCicle();

            TestPathMoreThanK();

            Console.ReadLine();
        }
    }
}
