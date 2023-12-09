using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.RegularExpressions;
using System.Web;

namespace Ripley.Api.Provider.CrossCutting.Helpers
{
    public enum FilterOperator
    {
        eq = 1,
        ne = 2,
        ge = 3,
        le = 4
    }
    /// <summary>
    /// OData filter $filter=attribute eq 'value value'
    /// </summary>
    public class Filter
    {
        public string Attribute { get; set; }
        public FilterOperator Operator { get; set; }
        public string Value { get; set; }
    }

    public class Expand
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// ODATA query sintax support
    /// </summary>
    [ModelBinder(typeof(ODataQueryModelBinder))]
    public class ODataQuery
    {
        public ODataQuery()
        {
            this.Filters = new Filter[] { };
            this.PageOption = new PagingOptions();
            this.OrderBys = new OrderByOptions[] { };
            this.Expands = new Expand[] { };
            this.QueryParams = new Dictionary<string, string>();
        }
        public bool Metadata { get; set; }

        /// <summary>
        /// $filter=attribute eq value 
        /// </summary>
        public Filter[] Filters { get; set; }

        /// <summary>
        /// $select=attribute,attribute,attribute
        /// </summary>
        public string Select { get; set; } = string.Empty;

        /// <summary>
        /// $order
        /// </summary> 
        /// <summary>

        public OrderByOptions[] OrderBys { get; set; }

        public PagingOptions PageOption { get; set; }
        /// <summary>
        /// $expand
        /// </summary>
        public Expand[] Expands { get; set; }

        public Dictionary<string, string> QueryParams { get; set; }

        public string ToQueryString()
        {
            return string.Empty;
        }
    }
    public class ODataQueryModelBinder : IModelBinder
    {
        //public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        //{
        //    if (bindingContext.ModelType != typeof(ODataQuery))
        //    {
        //        return false;
        //    }
        //    var query = actionContext.Request.RequestUri.Query;
        //    if (string.IsNullOrWhiteSpace(query))
        //    {
        //        bindingContext.ModelState.AddModelError(bindingContext.ModelName, "wrong value");
        //    }
        //    var model = ODataExpressions.Parse(query);
        //    bindingContext.Model = model;
        //    return true;
        //}

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ModelType != typeof(ODataQuery))
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var query = bindingContext.ActionContext.HttpContext.Request.QueryString;
            if (string.IsNullOrWhiteSpace(query.ToString()))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "wrong value");
            }
            var model = ODataExpressions.Parse(query.ToString());
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }

    public class ODataExpressions
    {

        private static string FilterRegex = @"(?<Filter>" +
         "\n" + @"     (?<Resource>.+?)\s+" +
         "\n" + @"     (?<Operator>lk|eq|ne|gt|ge|lt|le|add|sub|mul|div|mod)\s+" +
         "\n" + @"     '?(?<Value>.+?)'?" +
         "\n" + @")" +
         "\n" + @"(?:" +
         "\n" + @"    \s*$" +
         "\n" + @"   |\s+(?:or|and|not)\s+" +
         "\n" + @")" +
         "\n";
        private static string FilterFormat = @"${Resource}§${Operator}§${Value}" + Environment.NewLine;
        private static System.Text.RegularExpressions.Regex Filter = new Regex(FilterRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        private static string OrderByRegex = @"(?<Field>.+?){1}\s+(?<Dir>.+){0,1}";
        private static string OrderByFormat = @"${Field},${Dir}";
        private static System.Text.RegularExpressions.Regex OrderBy = new Regex(OrderByRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);


        public static ODataQuery Parse(string input)
        {
            input = HttpUtility.UrlDecode(input);
            ODataQuery options = new ODataQuery();

            string[] resources = input.Split(new[] { '?' }, 2);

            string filter = resources.Length == 2 ? resources[1] : null;
            if (filter == null)
                return options;

            var parts = filter.Split('&');
            // Default Options.
            options.PageOption = new PagingOptions();

            for (int i = 0; i < parts.Length; i++)
            {
                string[] elementParts = parts[i].Split(new[] { '=' }, 2);

                string key = HttpUtility.UrlDecode(elementParts[0]);
                string value = elementParts.Length == 2 ? HttpUtility.UrlDecode(elementParts[1]) : "";

                switch (key.ToLower())
                {
                    case "$filter":
                        options.Filters = ProcessFilter(value);
                        options.QueryParams.Add("$filter", value);
                        break;
                    case "$orderby":
                        options.OrderBys = ProcessOrderBy(value);
                        options.QueryParams.Add("$orderby", value);
                        break;
                    case "$pagesize":
                        options.PageOption.PageSize = ProcessTop(value);
                        break;
                    case "$pagenumber":
                        options.PageOption.PageNumber = ProcessSkip(value);
                        break;
                    case "$metadata":
                        options.Metadata = ProcessMetadata(value);
                        break;
                    case "$select":
                        options.Select = ProcessSelect(value);
                        break;
                    case "$expand":
                        options.Expands = ProcessExpand(value);
                        break;
                }
            }
            return options;
        }

        private static Expand[] ProcessExpand(string value)
        {
            var clauses = value.Split(new[] { ',' });
            Expand[] results = new Expand[clauses.Length];
            for (int i = 0; i < clauses.Length; i++)
            {
                results[i] = new Expand() { Name = clauses[i] };
                // TODO:
            }
            return results;
        }

        private static string ProcessSelect(string value)
        {
            return value;
        }

        private static bool ProcessMetadata(string value)
        {
            return true;
        }
        private static OrderByOptions[] ProcessOrderBy(string value)
        {
            List<OrderByOptions> result = new List<OrderByOptions>();
            var clauses = value.Split(new[] { ',' });
            for (int i = 0; i < clauses.Length; i++)
            {
                var clause = OrderBy.Replace(clauses[i], OrderByFormat).Split(',');

                var orderBy = new OrderByOptions() { Name = clause[0] };
                if (clause.Length == 2)
                {
                    switch (clause[1].ToLower())
                    {
                        case "asc":
                            orderBy.Ascending = true;
                            break;
                        case "desc":
                            orderBy.Ascending = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    orderBy.Ascending = true;
                }

                result.Add(orderBy);
            }

            return result.ToArray();
        }
        public static Filter[] ProcessFilter(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return new Filter[0];
            List<Filter> result = new List<Filter>();
            var parse = Filter.Replace(value, FilterFormat);

            var reader = new StringReader(parse);

            var item = reader.ReadLine();
            while (!string.IsNullOrWhiteSpace(item))
            {
                var line = item.Split('§');
                var filter = new Filter();
                filter.Attribute = line[0];
                filter.Operator = (FilterOperator)Enum.Parse(typeof(FilterOperator), line[1]);
                filter.Value = line[2];
                result.Add(filter);
                item = reader.ReadLine();
            }
            return result.ToArray();
        }
        private static int ProcessTop(string value)
        {
            return GetPositiveInteger("pageSize", value);
        }
        private static int ProcessSkip(string value)
        {
            return GetPositiveInteger("pageNumber", value);
        }
        private static int GetPositiveInteger(string key, string value)
        {
            int intValue;
            if (!int.TryParse(value, out intValue) || intValue < 0)
            {
                throw new Exception(String.Format(" {1} must be positive", key));
            }
            return intValue;
        }

    }

    public class PagingOptions
    {
        public PagingOptions(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }

        public PagingOptions()
            : this(1, 20) // Default Setting.
        {

        }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class OrderByOptions
    {
        public string Name { get; set; }
        public bool Ascending { get; set; }
    }
    public enum DateOptions
    {
        LastWeek = 0,
        Last15Days = 1,
        LastMonth = 2
    }
    public class QueryFilterOptions
    {
        public QueryFilterOptions()
        {
            this.PagingOptions = new PagingOptions();
            this.OrderByOptions = new OrderByOptions();
        }

        public PagingOptions PagingOptions { get; set; }
        public OrderByOptions OrderByOptions { get; set; }
        public DateOptions DateOptions { get; set; }

        public string RequestContinuation { get; set; }

        public string GetRequestContinuation()
        {
            if (!string.IsNullOrEmpty(this.RequestContinuation) && this.RequestContinuation.Contains("RID"))
            {
                Regex pattern_size = new Regex(@"TRC:(?<page>\d+)");

                Match match_size = pattern_size.Match(this.RequestContinuation);

                this.RequestContinuation = this.RequestContinuation.Replace(match_size.Value, string.Format("TRC:{0}", (this.PagingOptions.PageNumber - 1) * this.PagingOptions.PageSize));

                this.RequestContinuation = this.RequestContinuation.Replace("¡", "#");
                this.RequestContinuation = this.RequestContinuation.Replace("¿", "+");
                this.RequestContinuation = this.RequestContinuation.Replace("?", "-");

                return this.RequestContinuation;
            }

            return null;
        }

    }
}
