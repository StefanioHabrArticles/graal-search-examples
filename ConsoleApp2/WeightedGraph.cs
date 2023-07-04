using System.Text;

namespace ConsoleApp2
{
    public class WeightedGraph
    {
        private readonly int _size;
        private readonly Dictionary<int, List<(int Vertex, int Weight)>> _adjacencyList = new();

        public WeightedGraph(int size, params (int, List<(int Vertex, int Weight)>)[] adjacencyList)
        {
            _size = size;
            adjacencyList.ToList()
                .ForEach(x => _adjacencyList[x.Item1] = x.Item2);
        }

        private SquareMatrix<TSemiRing> GetAdjacencyMatrix<TSemiRing>()
            where TSemiRing : IDoubleSemiRing<TSemiRing>
        {
            var adjacencyMatrix = new SquareMatrix<TSemiRing>(_size);
            for (var i = 0; i < _size; i++)
            {
                adjacencyMatrix[i, i] = TSemiRing.MultiplicativeIdentity;
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
            (double)(GetAdjacencyMatrix<MinPlus>() ^ k)[i, j];

        public double GetLongestPath(int i, int j, int k) =>
            (double)(GetAdjacencyMatrix<MaxPlus>() ^ k)[i, j];

        public override string ToString() =>
            new StringBuilder($"digraph wg_{GetHashCode()} {{\n")
                .Append("\trankdir=LR\n")
                .AppendJoin('\n', _adjacencyList.SelectMany(kvp => kvp.Value
                        .Select(v => new { Vertex = kvp.Key, Edge = v }))
                    .Select(x => $"\t{x.Vertex}->{x.Edge.Vertex} [label=\"{x.Edge.Weight}\"] [weight={x.Edge.Weight}]"))
                .Append("\n}")
                .ToString();
    }
}