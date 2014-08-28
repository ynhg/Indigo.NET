﻿using Indigo.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Indigo.Web.Mvc.Util
{
    public static class HtmlUtils
    {
        public static IEnumerable<SelectListItem> GetSelectList(Type enumType, Enum defaultValue = null)
        {
            IList<SelectListItem> result = new List<SelectListItem>();

            if (enumType.IsEnum)
            {
                var elems = Enum.GetValues(enumType);

                if (defaultValue == null)
                    defaultValue = (Enum)elems.GetValue(0);

                foreach (var elem in elems)
                {
                    string text = elem.GetDescription();
                    string value = elem.ToString();
                    bool selected = Enum.Equals(elem, defaultValue);

                    result.Add(new SelectListItem { Text = text, Value = value, Selected = selected });
                }
            }

            return result;
        }
    }
}
