
namespace TeamFoundation.Common.ExpressionVisitors
{
    using System.Collections.Generic;

    public sealed class FilterNodeEnumerator : IEnumerator<FilterNode>
    {
        private readonly FilterNode rootNode;
        private FilterNode currentNode;

        public FilterNodeEnumerator(FilterNode rootNode)
        {
            this.rootNode = rootNode;
        }

        public FilterNode Current
        {
            get
            {
                return this.currentNode;
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return this.currentNode;
            }
        }

        public bool MoveNext()
        {
            if (this.currentNode == null && this.rootNode != null)
            {
                this.currentNode = this.rootNode;
                return true;
            }
            else if (this.currentNode != null && this.currentNode.NextNode != null)
            {
                this.currentNode = this.currentNode.NextNode;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            this.currentNode = this.rootNode;
        }

        public void Dispose()
        {
        }
    }
}
