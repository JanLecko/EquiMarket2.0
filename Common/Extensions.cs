using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;

namespace EquiMarket.Common
{
    public static class Extensions
    {

       
        public static MvcHtmlString ClientIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression)));
        }
        

        public static IHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html, Expression<Func<TModel, TEnum>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);

            var enumType = Nullable.GetUnderlyingType(metadata.ModelType) ?? metadata.ModelType;

            var enumValues = Enum.GetValues(enumType).Cast<object>();

            var items = from enumValue in enumValues
                        select new SelectListItem
                        {
                            Text = enumValue.ToString(),
                            Value = ((int)enumValue).ToString(),
                            Selected = enumValue.Equals(metadata.Model)
                        };

            return html.DropDownListFor(expression, items, string.Empty, new { @class = "form-control" });
        }

        public static MvcHtmlString AutocompleteFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string actionName, string controllerName)
        {
            string autocompleteUrl = UrlHelper.GenerateUrl(null, actionName, controllerName,
                                                           null,
                                                           html.RouteCollection,
                                                           html.ViewContext.RequestContext,
                                                           includeImplicitMvcValues: true);


            return html.TextBoxFor(expression, new { data_autocomplete_url = autocompleteUrl });
        }


        private const string AutoCompleteControllerKey = "AutoCompleteController";
        private const string AutoCompleteActionKey = "AutoCompleteAction";
        private const string AutoCompleteLabelKey = "AutoCompleteLabel";
        public static void SetAutoComplete(this ModelMetadata metadata, string controller, string action, string label)
        {
            metadata.AdditionalValues[AutoCompleteControllerKey] = controller;
            metadata.AdditionalValues[AutoCompleteActionKey] = action;
            metadata.AdditionalValues[AutoCompleteLabelKey] = label;
        }

        public static string GetAutoCompleteUrl(this HtmlHelper html, ModelMetadata metadata)
        {
            if (metadata.AdditionalValues.Count <= 0)
                return null;
             
            string controller = metadata.AdditionalValues.GetString(AutoCompleteControllerKey);
            string action = metadata.AdditionalValues.GetString(AutoCompleteActionKey);

            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
                return null;
            
            return UrlHelper.GenerateUrl(null, action, controller, null, html.RouteCollection, html.ViewContext.RequestContext, true);
        }

        public static string GetAutocompleteLabel(this HtmlHelper html, ModelMetadata metadata)
        {
            return metadata.AdditionalValues.GetString(AutoCompleteLabelKey);
        }
        
        private static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            object value;
            dictionary.TryGetValue(key, out value);
            return (string)value;
        }

        public static MvcHtmlString Script(this HtmlHelper htmlHelper, Func<object, HelperResult> template)
        {
            htmlHelper.ViewContext.HttpContext.Items["_script_" + Guid.NewGuid()] = template;
            return MvcHtmlString.Empty;
        }

        public static IHtmlString RenderScripts(this HtmlHelper htmlHelper)
        {
            foreach (object key in htmlHelper.ViewContext.HttpContext.Items.Keys)
            {
                if (key.ToString().StartsWith("_script_"))
                {
                    var template = htmlHelper.ViewContext.HttpContext.Items[key] as Func<object, HelperResult>;
                    if (template != null)
                    {
                        htmlHelper.ViewContext.Writer.Write(template(null));
                    }
                }
            }
            return MvcHtmlString.Empty;
        }
    }
}