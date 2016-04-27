using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Go.UnionFind
{
    public class Tree<T>
    {
        private Tree<T> _parent;
        public Tree<T> Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != null)
                {
                    _parent.NodesCount -= _nodesCount;
                }
                if (value != null)
                {
                    value.NodesCount += _nodesCount;
                }
                _parent = value;
            }
        }

        public T Value;

        private int _nodesCount;

        public int NodesCount
        {
            get { return _nodesCount; }
            set
            {
                if (_parent != null)
                {
                    if (value > _nodesCount)
                    {
                        Parent.NodesCount += value - _nodesCount;
                    }
                    else
                    {
                        Parent.NodesCount -= _nodesCount - value;
                    }
                }

                _nodesCount = value;
            }
        }

        public Tree(Tree<T> parent, T value)
        {
            _nodesCount = 1;
            _parent = parent;
            Value = value;

            if (parent != null)
            {
                parent.NodesCount += _nodesCount;
            }
        }
    }
}