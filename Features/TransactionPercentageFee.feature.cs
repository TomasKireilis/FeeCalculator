// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.0.0.0
//      SpecFlow Generator Version:3.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace AcceptanceTests.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class TransactionPercentageFeeFeature : Xunit.IClassFixture<TransactionPercentageFeeFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "TransactionPercentageFee.feature"
#line hidden
        
        public TransactionPercentageFeeFeature(TransactionPercentageFeeFeature.FixtureData fixtureData, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "TransactionPercentageFee", "\t\tAs a MobilePay accountant I want merchants to be charged Transaction Percentage" +
                    " Fee (1% of transaction \r\n\t\tamount), so that MobilePay would still be cheapest s" +
                    "olution in the market and we could earn enough money\r\n\t\tto cover our expenses", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.ScenarioTearDown();
        }
        
        [Xunit.FactAttribute(DisplayName="CalculateFees")]
        [Xunit.TraitAttribute("FeatureTitle", "TransactionPercentageFee")]
        [Xunit.TraitAttribute("Description", "CalculateFees")]
        public virtual void CalculateFees()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("CalculateFees", null, ((string[])(null)));
#line 6
this.ScenarioInitialize(scenarioInfo);
            this.ScenarioStart();
#line 7
testRunner.Given("that <Amount> DKK transaction is made to <MerchantName> on <date>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 8
testRunner.Given("that <Amount> DKK transaction is made to <MerchantName> on <date>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
testRunner.Given("that <Amount> DKK transaction is made to <MerchantName> on <date>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 10
testRunner.Given("that <Amount> DKK transaction is made to <MerchantName> on <date>", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 11
testRunner.When("fees calculation app is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
            TechTalk.SpecFlow.Table table1 = new TechTalk.SpecFlow.Table(new string[] {
                        "Date",
                        "MerchantName",
                        "Amount",
                        "FeeAmount"});
            table1.AddRow(new string[] {
                        "2018-09-02",
                        "CIRCLE_K",
                        "120",
                        "1.20"});
            table1.AddRow(new string[] {
                        "2018-09-04",
                        "TELIA",
                        "200",
                        "2.00"});
            table1.AddRow(new string[] {
                        "2018-10-22",
                        "CIRCLE_K",
                        "300",
                        "3.00"});
            table1.AddRow(new string[] {
                        "2018-10-29",
                        "CIRCLE_K",
                        "150",
                        "1.50"});
#line 12
testRunner.Then("the output is", ((string)(null)), table1, "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.0.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                TransactionPercentageFeeFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                TransactionPercentageFeeFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion