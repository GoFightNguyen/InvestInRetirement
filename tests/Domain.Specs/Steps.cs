using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace Domain.Specs
{
    [Binding]
    public class Steps
    {
        private decimal _annualSalary;
        private Percent _investmentPercentage;

        [Given(@"my annual salary is \$(.*)")]
        public void GivenMyAnnualSalaryIs(decimal annualSalary)
        {
            _annualSalary = annualSalary;
        }

        [When(@"I want to invest (.*)% into retirement")]
        public void WhenIWantToInvestIntoRetirement(decimal percentage)
        {
            _investmentPercentage = percentage;
        }

        [Then(@"I am told to invest \$(.*)")]
        public void ThenIAmToldToInvest(decimal expected)
        {
            var moniesToInvest = _investmentPercentage.Of(_annualSalary);
            Assert.AreEqual(expected, moniesToInvest);
        }
    }
}
