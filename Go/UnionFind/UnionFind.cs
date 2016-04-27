using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.UnionFind
{
    public class UnionFind<T>
    {
        public Tree<T>[] Trees;

        private int _treesCount;

        public int TressCount => _treesCount;

        public UnionFind(int disjoinedSetsCount)
        {
            Trees = new Tree<T>[disjoinedSetsCount];
            _treesCount = 0;
        }

        public void Clear()
        {
            for (int i = 0; i < Trees.Length; i++)
            {
                Trees[i] = null;
            }
        }

        public void Add(Tree<T> tree)
        {
            _treesCount++;
        }

        public Tree<T> Find(Tree<T> tree)
        {
            while (tree.Parent != null)
            {
                tree = tree.Parent;
            }

            return tree;
        }

        public Tree<T> Union(Tree<T> node1, Tree<T> node2)
        {
            return Find(node1).Parent = Find(node2);
        }
    }
}