using System;
using System.Linq.Expressions;
using FubuMVC.UI.Configuration;
using FubuMVC.UI.Forms;
using FubuMVC.UI.Tags;
using HtmlTags;
using NUnit.Framework;
using FubuMVC.Core.Util;

namespace FubuMVC.Tests.UI.Forms
{
    [TestFixture]
    public class FormLineExpressionTester
    {
        private DefinitionListLabelAndField layout;
        private FormLineExpression<ViewModel> expression;

        [SetUp]
        public void SetUp()
        {
            layout = new DefinitionListLabelAndField();
            expression = new FormLineExpression<ViewModel>(new StubTagGenerator<ViewModel>(), layout, x => x.Name);
        }

        [Test]
        public void has_label_by_default()
        {
            expression.AlterLabel(l =>
            {
                l.ShouldNotBeNull();
                l.HasClass("label").ShouldBeTrue();
            });
        }

        [Test]
        public void label_id()
        {
            expression.LabelId("id1");
            layout.LabelTag.Id().ShouldEqual("id1");
        }

        [Test]
        public void body_id()
        {
            expression.BodyId("id2");
            layout.BodyTag.Id().ShouldEqual("id2");
        }

        [Test]
        public void replace_label_with_text()
        {
            HtmlTag newLabel = new HtmlTag("span").Text("123");
            expression.Label(newLabel);

            expression.AlterLabel(l =>
            {
                l.ShouldBeTheSameAs(newLabel);
            });
        }


        [Test]
        public void display_by_default_in_the_to_string()
        {
            string html = expression.ToString();
            html.ShouldContain("display");
            html.ShouldNotContain("input");
        }

        [Test]
        public void edit_if_the_condition_is_true()
        {
            string html = expression.Editable(true).ToString();
            html.ShouldContain("input");
            html.ShouldNotContain("display");
        }

        [Test]
        public void display_if_the_condition_is_false()
        {
            string html = expression.Editable(false).ToString();
            html.ShouldNotContain("input");
            html.ShouldContain("display");
        }

        [Test]
        public void to_string_is_non_blank_by_default()
        {
            expression.ToString().Length.ShouldBeGreaterThan(0);
        }

        [Test]
        public void to_string_is_non_blank_when_the_visible_condition_is_true()
        {
            expression.Visible(true).ToString().Length.ShouldBeGreaterThan(0);
        }

        [Test]
        public void to_string_is_blank_when_the_visible_condition_is_false()
        {
            expression.Visible(false).ToString().ShouldEqual(string.Empty);
        }

        public class ViewModel
        {
            public string Name { get; set; }
        }
    }

    public class StubTagGenerator<T> : ITagGenerator<T> where T : class
    {
        public void SetProfile(string profileName)
        {
            throw new NotImplementedException();
        }

        public HtmlTag LabelFor(Expression<Func<T, object>> expression)
        {
            return new HtmlTag("span").AddClass("label").Text(expression.ToAccessor().Name);
        }

        public HtmlTag LabelFor(Expression<Func<T, object>> expression, string profile)
        {
            throw new NotImplementedException();
        }

        public HtmlTag InputFor(Expression<Func<T, object>> expression)
        {
            return new HtmlTag("span").AddClass("input").Text(expression.ToAccessor().Name);
        }

        public HtmlTag InputFor(Expression<Func<T, object>> expression, string profile)
        {
            throw new NotImplementedException();
        }

        public HtmlTag DisplayFor(Expression<Func<T, object>> expression)
        {
            return new HtmlTag("span").AddClass("display").Text(expression.ToAccessor().Name);
        }

        public HtmlTag DisplayFor(Expression<Func<T, object>> expression, string profile)
        {
            throw new NotImplementedException();
        }

        public ElementRequest GetRequest(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public HtmlTag LabelFor(ElementRequest request)
        {
            throw new NotImplementedException();
        }

        public HtmlTag InputFor(ElementRequest request)
        {
            throw new NotImplementedException();
        }

        public HtmlTag DisplayFor(ElementRequest request)
        {
            throw new NotImplementedException();
        }

        public ElementRequest GetRequest<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            throw new NotImplementedException();
        }

        public string ElementPrefix { get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); } }
    }
}