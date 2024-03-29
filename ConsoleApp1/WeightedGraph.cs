using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class WeightedGraph
    {
        private readonly int _size;
        private readonly Dictionary<int, List<(int Vertex, double Weight)>> _adjacencyList = new();

        public WeightedGraph(int size, params (int, List<(int Vertex, double Weight)>)[] adjacencyList)
        {
            _size = size;
            adjacencyList.ToList()
                .ForEach(x => _adjacencyList[x.Item1] = x.Item2);
        }

        private SquareMatrix<double, S> GetAdjacencyMatrix<S>() 
            where S : struct, ISemiRing<double>
        {
            var adjacencyMatrix = new SquareMatrix<double, S>(_size);
            for (var i = 0; i < _size; i++)
            {
                adjacencyMatrix[i, i] = default(S).One;
            }

            foreach (var key in _adjacencyList.Keys)
            {
                foreach (var (vertex, weight) in _adjacencyList[key])
                {
                    adjacencyMatrix[key, vertex] = weight;
                }
            }

            return adjacencyMatrix;
        }
        
        public double GetShortestPath(int i, int j, int k) => 
            GetAdjacencyMatrix<MinPlus>()
                .Power(k)[i, j];

        public double GetLongestPath(int i, int j, int k) =>
            GetAdjacencyMatrix<MaxPlus>()
                .Power(k)[i, j];
    }
}