using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class WeightedGraph
    {
        private readonly int _size;
        private readonly Dictionary<int, List<(int Vertex, double Weight)>> _adjancencyList = new();

        public WeightedGraph(int size, params (int, List<(int Vertex, double Weight)>)[] adjancencyList)
        {
            _size = size;
            adjancencyList.ToList()
                .ForEach(x => _adjancencyList[x.Item1] = x.Item2);
        }

        private SquareMatrix<double, S> GetAdjacencyMatrix<S>() 
            where S : struct, ISemiRing<double>
        {
            var adjancencyMatrix = new SquareMatrix<double, S>(_size);
            for (var i = 0; i < _size; i++)
            {
                adjancencyMatrix[i, i] = default(S).One;
            }

            foreach (var key in _adjancencyList.Keys)
            {
                foreach (var (vertex, weight) in _adjancencyList[key])
                {
                    adjancencyMatrix[key, vertex] = weight;
                }
            }

            return adjancencyMatrix;
        }
        
        public double GetShortestPath(int i, int j, int k) => 
            GetAdjacencyMatrix<MinPlus>()
                .Power(k)[i, j];

        public double GetLongestPath(int i, int j, int k) =>
            GetAdjacencyMatrix<MaxPlus>()
                .Power(k)[i, j];
    }
}