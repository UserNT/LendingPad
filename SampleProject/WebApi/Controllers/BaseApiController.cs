using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http;
using System.Web.UI.WebControls;
using WebApi.Models;

namespace WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected Func<T, object> BuildOrderByLambda<T>(string fieldName)
        {
            var parameter = Expression.Parameter(typeof(T), "e");

            var property = Expression.Property(parameter, fieldName);
            var converted = Expression.Convert(property, typeof(object));

            var orderBy = Expression.Lambda<Func<T, object>>(converted, new List<ParameterExpression>() { parameter });

            return orderBy.Compile();
        }

        protected Predicate<T> BuildFilterPredicate<T>(FilterRequestDTO filter)
        {
            if (filter.Filters == null)
            {
                return p => true;
            }

            var parameter = Expression.Parameter(typeof(T), "e");
            Expression predicateExpression = BuildPredicateExpression(parameter, filter.Filters);

            var expression = Expression.Lambda<Predicate<T>>(predicateExpression, parameter);

            return expression.Compile();
        }

        private Expression BuildPredicateExpression(Expression parameter, FilterDTO filter)
        {
            if (filter.Filters != null && filter.Filters.Any())
            {
                var expressions = filter.Filters.Select(x => BuildPredicateExpression(parameter, x)).ToList();

                var combined = expressions.First();
                for (int i = 1; i < expressions.Count; i++)
                {
                    combined = ApplyLogicOperator(combined, expressions[i], filter.Logic);
                }

                return combined;
            }
            else if (!string.IsNullOrWhiteSpace(filter.Left))
            {
                Expression left = Expression.Property(parameter, filter.Left);
                Expression right = Expression.Constant(filter.Right);
                Expression comparison = BuildComparisonExpression(left, filter.Operator, right);

                return comparison;
            }
            throw new InvalidOperationException($"Unsupported filter");
        }

        private Expression BuildComparisonExpression(Expression left, FilterOperator filterOperator, Expression right)
        {
            if (left.Type != right.Type)
            {
                if (left.Type == typeof(decimal))
                {
                    right = Expression.Convert(right, typeof(decimal));
                }
                else if (left.Type == typeof(int) || left.Type == typeof(long))
                {
                    right = Expression.Convert(right, typeof(long));
                }
            }

            switch (filterOperator)
            {
                case FilterOperator.Contains:
                    return Expression.Call(left, "Contains", null, right);
                case FilterOperator.GreatThan:
                    return Expression.GreaterThan(left, right);
                case FilterOperator.LessThan:
                    return Expression.LessThan(left, right);
                case FilterOperator.GreatThanOrEqual:
                    return Expression.GreaterThanOrEqual(left, right);
                case FilterOperator.LessThanOrEqual:
                    return Expression.LessThanOrEqual(left, right);
                case FilterOperator.Equal:
                    return Expression.Equal(left, right);
                default:
                    throw new InvalidOperationException($"Unsupported operator: {filterOperator}");
            }
        }

        private Expression ApplyLogicOperator(Expression left, Expression right, FilterLogic logic)
        {
            switch (logic)
            {
                case FilterLogic.AND:
                    return Expression.AndAlso(left, right);
                case FilterLogic.OR:
                    return Expression.OrElse(left, right);
                default:
                    throw new InvalidOperationException($"Unsupported logic: {logic}");
            }
        }
    }
}