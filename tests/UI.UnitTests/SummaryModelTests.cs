using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UI.Pages;

namespace UI.UnitTests
{
    [TestClass]
    public class SummaryModelTests
    {
        [TestMethod]
        public void HasTheCorrectDefaults()
        {
            var pageModel = new SummaryModel();
            pageModel.AnnualSalary.Should().Be(decimal.Zero);
            pageModel.InvestmentPercentage.Should().Be(decimal.Zero);
            pageModel.MoniesToInvest.Should().Be(decimal.Zero);
        }

        [TestClass]
        public class OnPost
        {
            [TestMethod]
            public void ShouldCalculateHowMuchToInvest()
            {
                var pageModel = new SummaryModel
                {
                    AnnualSalary = 106_156,
                    InvestmentPercentage = 15
                };
                pageModel.OnPost();
                pageModel.MoniesToInvest.Should().Be(15_923.40m);
            }

            [TestMethod]
            public void MyTestMethod()
            {
                var pageModel = new SummaryModel
                {
                    AnnualSalary = 106_156,
                    InvestmentPercentage = 15
                };
                var result = pageModel.OnPost();
                result.Should().BeOfType<PageResult>();
            }
        }
    }
}
