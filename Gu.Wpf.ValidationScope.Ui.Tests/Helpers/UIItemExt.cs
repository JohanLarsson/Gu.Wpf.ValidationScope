using System.Collections.Generic;
using System.Linq;
using System.Windows.Automation;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;

namespace Gu.Wpf.ValidationScope.Ui.Tests
{
    using Gu.Wpf.ValidationScope.Demo;

    using TestStack.White.InputDevices;
    using TestStack.White.UIItems.TabItems;
    using TestStack.White.UIItems.WindowItems;

    public static class UIItemExt
    {
        public static string ItemStatus(this IUIItem item)
        {
            return (string)item.AutomationElement.GetCurrentPropertyValue(AutomationElementIdentifiers.ItemStatusProperty);
        }

        public static IReadOnlyList<T> GetMultiple<T>(this UIItemContainer contaner, string automationId)
            where T : IUIItem
        {
            return contaner.GetMultiple(SearchCriteria.ByAutomationId(automationId))
                           .OfType<T>()
                           .ToList();
        }

        public static IReadOnlyList<string> GetErrors(this TabPage page)
        {
            return page.GetMultiple<Label>(AutomationIDs.ErrorText)
                       .Select(x => x.Text)
                       .ToList();
        }

        public static void EnterSingle(this TextBox textBox, char @char)
        {
            textBox.DoubleClick();
            Keyboard.Instance.Send(new string(@char, 1), textBox.ActionListener);
        }

        public static IEnumerable<IUIItem> Ancestors(this IUIItem item)
        {
            var parent = item.GetParent<IUIItem>();
            while (parent != null)
            {
                yield return parent;
                parent = parent.GetParent<IUIItem>();
            }
        }
    }
}