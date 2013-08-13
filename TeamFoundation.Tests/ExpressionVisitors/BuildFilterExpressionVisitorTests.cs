
namespace TeamFoundation.Tests.ExpressionVisitors
{
    using System;
    using System.Linq.Expressions;
    using TeamFoundation.Common.Entities;
    using TeamFoundation.Common.ExpressionVisitors;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BuildFilterExpressionVisitorTests
    {
        [TestMethod]
        public void ItShouldSupportEqualityOperator()
        {
            BinaryExpression expression = Expression.Equal(Expression.Property(Expression.Constant(new Build()), "Project"), Expression.Constant("myproject"));
            var visitor = new BuildFilterExpressionVisitor(expression);
            var node = visitor.Eval();

            Assert.AreEqual(node.Key, "Project");
            Assert.AreEqual(node.Sign, FilterExpressionType.Equal);
            Assert.AreEqual(node.Value, "myproject");
        }

        [TestMethod]
        public void ItShouldSupportInequalityOperator()
        {
            BinaryExpression expression = Expression.NotEqual(Expression.Property(Expression.Constant(new Build()), "Project"), Expression.Constant("myproject"));
            var visitor = new BuildFilterExpressionVisitor(expression);
            var node = visitor.Eval();

            Assert.AreEqual(node.Key, "Project");
            Assert.AreEqual(node.Sign, FilterExpressionType.NotEqual);
            Assert.AreEqual(node.Value, "myproject");
        }

        [TestMethod]
        public void ItShouldSupportAndOperator()
        {
            BinaryExpression expression1 = Expression.Equal(Expression.Property(Expression.Constant(new Build()), "Project"), Expression.Constant("myproject"));
            BinaryExpression expression2 = Expression.NotEqual(Expression.Property(Expression.Constant(new Build()), "Quality"), Expression.Constant("good"));

            var visitor = new BuildFilterExpressionVisitor(Expression.And(expression1, expression2));

            var firstNode = visitor.Eval();
            Assert.AreEqual(firstNode.Key, "Project");
            Assert.AreEqual(firstNode.Sign, FilterExpressionType.Equal);
            Assert.AreEqual(firstNode.Value, "myproject");

            var secondNode = firstNode.NextNode;
            Assert.AreEqual(secondNode.Key, "Quality");
            Assert.AreEqual(secondNode.Sign, FilterExpressionType.NotEqual);
            Assert.AreEqual(secondNode.Value, "good");
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ItShouldNotSupportOrOperator()
        {
            BinaryExpression expression1 = Expression.Equal(Expression.Property(Expression.Constant(new Build()), "Project"), Expression.Constant("myproject"));
            BinaryExpression expression2 = Expression.NotEqual(Expression.Property(Expression.Constant(new Build()), "Quality"), Expression.Constant("good"));

            var visitor = new BuildFilterExpressionVisitor(Expression.OrElse(expression1, expression2));

            var node = visitor.Eval();
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ItShouldNotSupportMoreThanOneExpressionPerField()
        {
            BinaryExpression expression1 = Expression.Equal(Expression.Property(Expression.Constant(new Build()), "Project"), Expression.Constant("myproject"));
            BinaryExpression expression2 = Expression.NotEqual(Expression.Property(Expression.Constant(new Build()), "Project"), Expression.Constant("anotherproject"));

            var visitor = new BuildFilterExpressionVisitor(Expression.And(expression1, expression2));

            var node = visitor.Eval();
        }
    }
}
