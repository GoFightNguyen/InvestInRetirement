using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace Domain.Specs
{
    [Binding]
    public class Steps
    {
        private decimal _annualSalary;
        private Percent _desiredInvestmentPercentage;
        private Percent _investmentPercentage;
        private Dictionary<string, Percent> _investmentPercentages = new Dictionary<string, Percent>();

        [Given(@"my annual salary is \$(.*)")]
        public void GivenMyAnnualSalaryIs(decimal annualSalary)
        {
            _annualSalary = annualSalary;
        }

        [When(@"I want to invest (.*)% into retirement")]
        public void WhenIWantToInvestIntoRetirement(decimal percentage)
        {
            _desiredInvestmentPercentage = percentage;
        }

        [When(@"I invest (.*)% into an investment")]
        public void WhenIInvestIntoAn(decimal percentange)
        {
            _investmentPercentage = percentange;
        }

        [When(@"I invest")]
        public void WhenIInvest(Table table)
        {
            foreach (var row in table.Rows)
            {
                _investmentPercentages.Add(row["investment"], decimal.Parse(row["percentage"]));
            }
        }

        [Then(@"I am told to invest \$(.*)")]
        public void ThenIAmToldToInvest(decimal expected)
        {
            var moniesToInvest = _desiredInvestmentPercentage.Of(_annualSalary);
            Assert.AreEqual(expected, moniesToInvest);
        }

        [Then(@"I am investing \$(.*)")]
        public void ThenIAmInvesting(decimal expected)
        {
            var moniesInvested = _investmentPercentage.Of(_annualSalary);
            Assert.AreEqual(expected, moniesInvested);
        }

        [Then(@"I am investing")]
        public void ThenIAmInvesting(Table table)
        {
            foreach (var row in table.Rows)
            {
                var investment = row["investment"];
                var expected = decimal.Parse(row["moniesInvested"], System.Globalization.NumberStyles.Currency);
                var actual = _investmentPercentages[investment].Of(_annualSalary);
                Assert.AreEqual(expected, actual, investment);
            }
        }
    }
}
