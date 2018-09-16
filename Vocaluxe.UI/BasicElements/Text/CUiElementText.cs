﻿using System.Collections.Generic;
using Vocaluxe.UI.AbstractElements;

namespace Vocaluxe.UI.BasicElements.Text
{
    public class CUiElementText:CUiElement
    {
        public CControllerElementText Controller { get; private set; }
        public override CController BasicController => Controller;

        public CUiElementText()
        {
            Controller = new CControllerElementText();
        }

        public static CUiElementText CreateInstance(Dictionary<string, string> properties, IEnumerable<(CUiElement uiElement, string bindingId)> children)
        {
            var instance = CUiElement.CreateInstance<CUiElementText>(properties, children);
            properties.TryGetValue("value", out string value);

            if (properties.ContainsKey("bindingid"))
            {
                instance.Controller.Text = DummyClass.RegisterBinding<string>(properties["bindingid"], (sender, s) => instance.Controller.Text = s);
            }
            else
            {
                instance.Controller.Text = value;
            }
            
            return instance;
        }
    }
}
