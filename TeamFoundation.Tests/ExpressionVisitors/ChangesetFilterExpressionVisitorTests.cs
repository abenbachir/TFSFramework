
namespace TeamFoundation.Tests.ExpressionVisitors
{
    using System;
    using System.Linq.Expressions;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.ExpressionVisitors;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ChangesetFilterExpressionVisitorTests
    {
        [TestMethod]
        public void ItShouldSupportEqualityOperator()
        {
            BinaryExpression expression = Expression.Equal(Expression.Property(Expression.Constant(new Changeset()), "Comment"), Expression.Constant("updating the config file"));
            var visitor = new ChangesetFilterExpressionVisitor(expression);
            var node = visitor.Eval();

            Assert.AreEqual(node.Key, "Comment");
            Assert.AreEqual(node.Sign, FilterExpressionType.Equal);
            Assert.AreEqual(node.Value, "updating the config file");
        }

        [TestMethod]
        public void ItShouldSupportInequalityOperator()
        {
            BinaryExpression expression = Expression.NotEqual(Expression.Property(Expression.Constant(new Changeset()), "Comment"), Expression.Constant("updating the config file"));
            var visitor = new ChangesetFilterExpressionVisitor(expression);
            var node = visitor.Eval();

            Assert.AreEqual(node.Key, "Comment");
            Assert.AreEqual(node.Sign, FilterExpressionType.NotEqual);
            Assert.AreEqual(node.Value, "updating the config file");
        }

        [TestMethod]
        public void ItShouldSupportAndOperator()
        {
            BinaryExpression expression1 = Expression.Equal(Expression.Property(Expression.Constant(new Changeset()), "Comment"), Expression.Constant("updating the config file"));
            BinaryExpression expression2 = Expression.NotEqual(Expression.Property(Expression.Constant(new Changeset()), "Committer"), Expression.Constant("johndoe"));

            var visitor = new ChangesetFilterExpressionVisitor(Expression.And(expression1, expression2));

            var firstNode = visitor.Eval();
            Assert.AreEqual(firstNode.Key, "Comment");
            Assert.AreEqual(firstNode.Sign, FilterExpressionType.Equal);
            Assert.AreEqual(firstNode.Value, "updating the config file");

            var secondNode = firstNode.NextNode;
            Assert.AreEqual(secondNode.Key, "Committer");
            Assert.AreEqual(secondNode.Sign, FilterExpressionType.NotEqual);
            Assert.AreEqual(secondNode.Value, "johndoe");
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ItShouldNotSupportOrOperator()
        {
            BinaryExpression expression1 = Expression.Equal(Expression.Property(Expression.Constant(new Changeset()), "Comment"), Expression.Constant("updating the config file"));
            BinaryExpression expression2 = Expression.NotEqual(Expression.Property(Expression.Constant(new Changeset()), "Committer"), Expression.Constant("johndoe"));

            var visitor = new ChangesetFilterExpressionVisitor(Expression.OrElse(expression1, expression2));

            var node = visitor.Eval();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ItShouldNotSupportMoreThanOneExpressionPerField()
        {
            BinaryExpression expression1 = Expression.Equal(Expression.Property(Expression.Constant(new Changeset()), "Comment"), Expression.Constant("updating the config file"));
            BinaryExpression expression2 = Expression.NotEqual(Expression.Property(Expression.Constant(new Changeset()), "Comment"), Expression.Constant("deleting a folder"));

            var visitor = new ChangesetFilterExpressionVisitor(Expression.And(expression1, expression2));

            var node = visitor.Eval();
        }
    }
}
