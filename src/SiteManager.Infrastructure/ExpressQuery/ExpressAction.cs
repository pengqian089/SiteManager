using System.Collections;
using System.Linq.Expressions;
using System.Reflection;
using SiteManager.EnumLibrary;

namespace SiteManager.Infrastructure.ExpressQuery
{
    public static class ExpressAction
    {
        internal class QueryField
        {
            public ViewModelLabelAttribute Label { get; set; }

            public PropertyInfo Property { get; set; }

            public object Value { get; set; }

            internal Expression HandleField(ParameterExpression parameter)
            {
                if (Value == null) return Expression.Empty();
                if (Property.PropertyType.IsEnum && Convert.ToInt32(Value) == -1)
                    return Expression.Empty();
                var memberExpr = Expression.Property(parameter, Property);
                switch (Label.Comparison)
                {
                    case ExpressComparison.Equal:
                        return Expression.Equal(memberExpr, Expression.Constant(Value));
                    case ExpressComparison.Gt:
                        return Expression.GreaterThan(memberExpr, Expression.Constant(Value));
                    case ExpressComparison.GtOrEqual:
                        return Expression.GreaterThanOrEqual(memberExpr, Expression.Constant(Value));
                    case ExpressComparison.Lt:
                        return Expression.LessThan(memberExpr, Expression.Constant(Value));
                    case ExpressComparison.LtOrEqual:
                        return Expression.LessThanOrEqual(memberExpr, Expression.Constant(Value));
                    case ExpressComparison.NoEqual:
                        return Expression.NotEqual(memberExpr, Expression.Constant(Value));
                    case ExpressComparison.Contains:
                        if (Property.PropertyType == typeof(string) && Value is string)
                        {
                            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            if (containsMethod != null)
                            {
                                // return Expression.Call(memberExpr, containsMethod,
                                //     Expression.Constant(Property.GetValue(null)), Expression.Constant(Value));
                                return Expression.Call(memberExpr, containsMethod, Expression.Constant(Value));
                            }
                        }

                        if (typeof(IEnumerable).IsAssignableFrom(Property.PropertyType))
                        {
                            var containsMethod = typeof(Enumerable)
                                .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                                .FirstOrDefault(x =>
                                    x.Name == "Contains" && x.ReturnType == typeof(bool) &&
                                    x.GetParameters().Length == 2);

                            if (containsMethod != null)
                            {
                                containsMethod = containsMethod.MakeGenericMethod(Value.GetType());
                                return Expression.Call(containsMethod, memberExpr, Expression.Constant(Value));
                            }
                        }

                        throw new NotImplementedException("string、IEnumerable以外类型的Contains表达式未实现！");
                    case ExpressComparison.Custom:
                        throw new NotImplementedException("暂无自定义实现");
                }

                return Expression.Empty();
            }
        }

        public static Expression<Func<TEntity, bool>> GenerateExpressTree<TEntity>(this IMapFrom<TEntity> mapFrom)
            where TEntity : IBaseEntity, new()
        {
            var parameter = Expression.Parameter(typeof(TEntity), "__q");

            var queryFieldsExpression = (from x in mapFrom.GetType().GetProperties()
                let attr = x.GetCustomAttribute<ViewModelLabelAttribute>()
                where attr != null
                let field = new QueryField
                {
                    Label = attr,
                    Property = typeof(TEntity).GetProperty(x.Name),
                    Value = x.GetValue(mapFrom)
                }
                select field.HandleField(parameter)).Where(x => x.NodeType != ExpressionType.Default).ToList();

            var expression =
                (Expression)Expression
                    .Constant(true); //(Expression)Expression.Equal(Expression.Constant(1), Expression.Constant(1));
            foreach (var expr in queryFieldsExpression)
            {
                if (queryFieldsExpression.IndexOf(expr) == 0)
                {
                    expression = expr;
                    continue;
                }

                expression = Expression.AndAlso(expression, expr);
            }

            return Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
        }
    }

}