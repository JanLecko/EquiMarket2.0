using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EquiMarket.Common
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoCompleteAttribute : Attribute, IMetadataAware
    {
        private readonly string _controller;
        private readonly string _action;
        private readonly string _label;

        public AutoCompleteAttribute(string controller, string action, string label)
        {
            _controller = controller;
            _action = action;
            _label = label;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetAutoComplete(_controller, _action, _label);
        }
    }
}