using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Collections;

namespace StarWars.Types
{
    public abstract class StarWarsCharacter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string[] Friends { get; set; }
        public int[] AppearsIn { get; set; }
    }

    public class Human : StarWarsCharacter
    {
        public string HomePlanet { get; set; }

        public override string ToString()
        {
            return String.Concat(Id, Name, Friends, AppearsIn, HomePlanet);
        }
    }

    public class Droid : StarWarsCharacter
    {
        public string PrimaryFunction { get; set; }
    }

    public class Filter
    {
        public string FilterKey { get; set; }
        public int FilterValueId { get; set; }
    }

    public class QueryTemplate
    {
        public string Query { get; set; }
        public string UserQuery { get; set; }
    }

    public interface IHorusRequestContext
    {
        int SiteId { get; set; }
        int LanguageId { get; set; }
        int CurrencyId { get; set; }
    }

    public interface INavigationRequest
    {
        IList<Filter> Filters { get; set; }
        QueryTemplate QueryTemplate { get; set; }
    }

    public class LandingNavigationAggregationRequest
    {
        public string LandingNavigationAggregationKey { get; set; }
    }

    public class RelatedProductsRequest
    {
        public AlternateColors AlternateColors { get; set; }
        public Recommended Recommended { get; set; }
    }

    public class AlternateColors { }

    public class Recommended
    {
        public int ProductCount { get; set; }
    }

    public class ProductNavigationRequest
    {
        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public string ProductSortMode { get; set; }

        public RelatedProductsRequest RelatedProductsRequest { get; set; }
    }

    public class HorusContainerRequest : INavigationRequest, IHorusRequestContext
    {
        public ProductNavigationRequest ProductNavigationRequest { get; set; }
        public LandingNavigationAggregationRequest LandingNavigationAggregationRequest { get; set; }
        // public SeoRequest SeoRequest { get; set; }

        public IList<Filter> Filters { get; set; }
        public QueryTemplate QueryTemplate { get; set; }
        public int SiteId { get; set; }
        public int LanguageId { get; set; }
        public int CurrencyId { get; set; }
        public int AppId { get; set; }
    }

    public class ObjectDumper
    {
        private int _level;
        private readonly int _indentSize;
        private readonly StringBuilder _stringBuilder;
        private readonly List<int> _hashListOfFoundElements;

        private ObjectDumper(int indentSize)
        {
            _indentSize = indentSize;
            _stringBuilder = new StringBuilder();
            _hashListOfFoundElements = new List<int>();
        }

        public static string Dump(object element)
        {
            return Dump(element, 2);
        }

        public static string Dump(object element, int indentSize)
        {
            var instance = new ObjectDumper(indentSize);
            return instance.DumpElement(element);
        }

        private string DumpElement(object element)
        {
            if (element == null || element is ValueType || element is string)
            {
                Write(FormatValue(element));
            }
            else
            {
                var objectType = element.GetType();
                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    Write("{{{0}}}", objectType.FullName);
                    _hashListOfFoundElements.Add(element.GetHashCode());
                    _level++;
                }

                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            _level++;
                            DumpElement(item);
                            _level--;
                        }
                        else
                        {
                            if (!AlreadyTouched(item))
                                DumpElement(item);
                            else
                                Write("{{{0}}} <-- bidirectional reference found", item.GetType().FullName);
                        }
                    }
                }
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        object value = fieldInfo != null
                                           ? fieldInfo.GetValue(element)
                                           : propertyInfo.GetValue(element, null);

                        if (type.IsValueType || type == typeof(string))
                        {
                            Write("{0}: {1}", memberInfo.Name, FormatValue(value));
                        }
                        else
                        {
                            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                            Write("{0}: {1}", memberInfo.Name, isEnumerable ? "..." : "{ }");

                            var alreadyTouched = !isEnumerable && AlreadyTouched(value);
                            _level++;
                            if (!alreadyTouched)
                                DumpElement(value);
                            else
                                Write("{{{0}}} <-- bidirectional reference found", value.GetType().FullName);
                            _level--;
                        }
                    }
                }

                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    _level--;
                }
            }

            return _stringBuilder.ToString();
        }

        private bool AlreadyTouched(object value)
        {
            if (value == null)
                return false;

            var hash = value.GetHashCode();
            for (var i = 0; i < _hashListOfFoundElements.Count; i++)
            {
                if (_hashListOfFoundElements[i] == hash)
                    return true;
            }
            return false;
        }

        private void Write(string value, params object[] args)
        {
            var space = new string(' ', _level * _indentSize);

            if (args != null)
                value = string.Format(value, args);

            _stringBuilder.AppendLine(space + value);
        }

        private string FormatValue(object o)
        {
            if (o == null)
                return ("null");

            if (o is DateTime)
                return (((DateTime)o).ToShortDateString());

            if (o is string)
                return string.Format("\"{0}\"", o);

            if (o is char && (char)o == '\0')
                return string.Empty;

            if (o is ValueType)
                return (o.ToString());

            if (o is IEnumerable)
                return ("...");

            return ("{ }");
        }
    }
}
