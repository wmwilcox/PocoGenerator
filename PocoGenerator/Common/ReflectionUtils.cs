using System;
using System.Linq.Expressions;
using System.Reflection;

namespace PocoGenerator.Common
{
    /// <summary>
    /// Class containing helper methods surrounding System.Reflection.
    /// </summary>
    public class ReflectionUtils
    {
        /// <summary>
        /// This method is used to interpret the properties referenced in the .For(x => x.SomeProperty) method in PocoGenerator.
        /// </summary>
        /// <typeparam name="T">The parent type of the expression</typeparam>
        /// <typeparam name="TProperty">The type of the property the expression returns</typeparam>
        /// <param name="expression">The expression</param>
        /// <returns>The MemberInfo for the property that is returned by the expression.</returns>
        public static MemberInfo GetPropertyFromExpression <T, TProperty> (Expression<Func<T, TProperty>> expression) {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null) {
                throw new ArgumentException("Expected MemberExpression as parameter.", "expression");
            }
            return memberExpression.Member;
        }

        /// <summary>
        /// Helper method for setting the value of a member.
        /// </summary>
        /// <param name="member">The MemberInfo for the member to be set.</param>
        /// <param name="target">The parent object of the member.</param>
        /// <param name="value">The value to set.</param>
        public static void SetMemberValue(MemberInfo member, object target, object value) {
            switch (member.MemberType)
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(target, value, null);
                    break;
                default:
                    throw new ArgumentException("MemberInfo must be of type FieldInfo or PropertyInfo", "member");
            }
        }
    }
}
