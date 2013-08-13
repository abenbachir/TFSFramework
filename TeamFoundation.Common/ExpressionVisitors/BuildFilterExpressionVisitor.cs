
namespace TeamFoundation.Common.ExpressionVisitors
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using TeamFoundation.Common.ExpressionVisitors;

    public class BuildFilterExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression expression;
        private FilterNode rootFilterNode;

        public BuildFilterExpressionVisitor(Expression expression)
        {
            this.expression = expression;
        }

        public FilterNode Eval()
        {
            if (this.expression == null)
            {
                return this.rootFilterNode;
            }

            this.Visit(this.expression);

            return this.rootFilterNode;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            var allowedProperties = new[] { "Project", "Definition", "Number", "Reason", "Quality", "Status", "RequestedBy", "RequestedFor", "StartTime", "FinishTime", "BuildFinished" };

            if (node.NodeType == ExpressionType.OrElse)
            {
                throw new NotSupportedException("Logical OR operators are not supported for Build Custom filters");
            }

            if (node.Left is MemberExpression && node.Right is ConstantExpression)
            {
                var fieldName = (node.Left as MemberExpression).Member.Name;
                var value = (node.Right as ConstantExpression).Value;

                if (!allowedProperties.ToList().Contains(fieldName))
                {
                    throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "You can only filter by the following properties: {0}. (e.g. /Builds/$filter=Number gt 100 and  Quality eq 'Healthy') ", string.Join(", ", allowedProperties)));
                }

                this.AddFilterNode(fieldName, value, FilterNode.ParseFilterExpressionType(node.NodeType), FilterNodeRelationship.And);
            }
            else if (node.Left.NodeType == ExpressionType.Conditional)
            {
                throw new NotSupportedException("Only equality and inequality operators between fields and constant expressions are allowed with Build Custom filters");
            }

            return base.VisitBinary(node);
        }

        private void AddFilterNode(string fieldName, object value, FilterExpressionType expressionType, FilterNodeRelationship filterNodeRelationship)
        {
            if (this.rootFilterNode != null && this.rootFilterNode.SingleOrDefault(p => p.Key.Equals(fieldName, StringComparison.OrdinalIgnoreCase)) != null)
            {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "More than one filter expression found for attribute {0}. Only one filter expression per attribute is supported.", fieldName));
            }

            var filterNode = new FilterNode { Key = fieldName, Value = (value == null) ? "null" : value.ToString(), Sign = expressionType, NodeRelationship = filterNodeRelationship };
            if (this.rootFilterNode != null)
            {
                this.rootFilterNode.AddNode(filterNode);
            }
            else
            {
                this.rootFilterNode = filterNode;
            }
        }
    }
}
