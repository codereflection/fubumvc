using System;
using System.Linq;
using FubuMVC.Core.Registration.Nodes;
using HtmlTags;

namespace FubuMVC.Core.Diagnostics.HtmlWriting
{
    public class ActionColumn : IColumn
    {
        public string Header()
        {
            return "Action(s)";
        }

        public void WriteBody(BehaviorChain chain, HtmlTag cell)
        {
            string text = Text(chain);

            cell.Text(text);
        }

        public string Text(BehaviorChain chain)
        {
            var descriptions = chain.Calls.Select(x => x.Description).ToArray();
            return descriptions.Length == 0 ? " -" : descriptions.Join(", ");
        }
    }
}