// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:3.1.0.0
//      SpecFlow Generator Version:3.1.0.0
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
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class TransactionPercentageFeeFeature : Xunit.IClassFixture<TransactionPercentageFeeFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "TransactionPercentageFee.Feature"
#line hidden
        
        public TransactionPercentageFeeFeature(TransactionPercentageFeeFeature.FixtureData fixtureData, AcceptanceTests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "TransactionPercentageFee", "\t\tAs a MobilePay accountant I want merchants to be charged Transaction Percentage" +
                    " Fee (1% of transaction amount),\r\n\t\tso that MobilePay would still be cheapest so" +
                    "lution in the market and we could earn enough money\r\n\t\tto cover our expenses", ProgrammingLanguage.CSharp, ((string[])(null)));
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
        
        public virtual void TestTearDown()
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
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="CalculateTransactionPercentageFee")]
        [Xunit.TraitAttribute("FeatureTitle", "TransactionPercentageFee")]
        [Xunit.TraitAttribute("Description", "CalculateTransactionPercentageFee")]
        public virtual void CalculateTransactionPercentageFee()
        {
            string[] tagsOfScenario = ((string[])(null));
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("CalculateTransactionPercentageFee", null, ((string[])(null)));
#line 6
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 7
testRunner.Given("Merchant repository is populated with Transaction Percentage Fee business logic", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Date",
                            "MerchantName",
                            "Amount"});
                table7.AddRow(new string[] {
                            "2018-09-02",
                            "CIRCLE_K",
                            "120"});
                table7.AddRow(new string[] {
                            "2018-09-04",
                            "TELIA",
                            "200"});
                table7.AddRow(new string[] {
                            "2018-10-22",
                            "CIRCLE_K",
                            "300"});
                table7.AddRow(new string[] {
                            "2018-10-29",
                            "CIRCLE_K",
                            "150"});
#line 8
testRunner.Given("that transactions where made", ((string)(null)), table7, "Given ");
#line hidden
#line 14
testRunner.When("fees calculation app is executed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "FeeAmount"});
                table8.AddRow(new string[] {
                            "1.20"});
                table8.AddRow(new string[] {
                            "2.00"});
                table8.AddRow(new string[] {
                            "3.00"});
                table8.AddRow(new string[] {
                            "1.50"});
#line 15
testRunner.Then("the output for Transaction Percentage Fee is", ((string)(null)), table8, "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.1.0.0")]
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
