using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;

namespace ParkingFeeCalculator.SeleniumTests;

[TestFixture]
[Category("UI")]
public class ParkingCalculatorUiTests
{
    // define test class (stores web app, browser driver, and base URL)
    private Process? _webProcess;
    private IWebDriver? _driver;
    private string _baseUrl = string.Empty;

    //clean slate
    [SetUp]
    public void SetUp()
    {
        // set base URL, start web app, and create browser driver
        _baseUrl = "http://127.0.0.1:5099";
        StartWebApplication();
        _driver = CreateWebDriver();
    }
    //clean up
    [TearDown]
    public void TearDown()
    {
        //close browser and stop web app
        _driver?.Quit();
        _driver?.Dispose();
        _driver = null;

        if (_webProcess is { HasExited: false })
        {
            _webProcess.Kill(entireProcessTree: true);
            _webProcess.WaitForExit(5000);
        }
    }
    //varied test inputs for the same test method
    [TestCase("3", "standard", "EUR12.00")]
    [TestCase("10", "standard", "EUR27.00")]
    [TestCase("6", "electric", "EUR12.00")]
    [TestCase("4", "motorbike", "EUR0.00")]
    [TestCase("0", "electric", "EUR0.00")]
    public void Calculator_shows_the_right_fee(string hours, string vehicleType, string expectedFee)
    {
        //simulates user interaction
        _driver!.Navigate().GoToUrl(_baseUrl);
        //opens app, fills in hours and vehicle type then clicks calculate
        _driver.FindElement(By.Id("hoursInput")).Clear();
        _driver.FindElement(By.Id("hoursInput")).SendKeys(hours);
        _driver.FindElement(By.Id("vehicleTypeInput")).Clear();
        _driver.FindElement(By.Id("vehicleTypeInput")).SendKeys(vehicleType);
        _driver.FindElement(By.Id("calculateButton")).Click();
        //wait until result text appears to avoid weird timing issue
        var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        var result = wait.Until(driver => driver.FindElement(By.Id("resultValue")).Text);
        //ensures result matches expected result
        Assert.That(result, Is.EqualTo(expectedFee));
    }
    //launches web process then waits until reachable
    private void StartWebApplication()
    {
        var projectDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "ParkingFeeCalculator.Web"));
        var psi = new ProcessStartInfo("dotnet", $"run --project \"{projectDirectory}\" --no-launch-profile -- --urls {_baseUrl}")
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = projectDirectory
        };

        _webProcess = Process.Start(psi) ?? throw new InvalidOperationException("Unable to start web application process.");
        WaitForApplicationToStart();
    }
    //pings app url until sucess, if not successful throw timeout
    private void WaitForApplicationToStart()
    {
        using var httpClient = new HttpClient();

        for (var attempt = 0; attempt < 40; attempt++)
        {
            try
            {
                var response = httpClient.GetAsync(_baseUrl).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
            catch
            {
            }

            Thread.Sleep(500);
        }

        var standardError = _webProcess?.StandardError.ReadToEnd() ?? string.Empty;
        throw new TimeoutException($"The web application did not start within the expected time. {standardError}");
    }
    //creates and cofigures selenium edge browser instance
    private static IWebDriver CreateWebDriver()
    {
        var options = new EdgeOptions();
        options.AddArgument("headless=new");//runs without opening a visible window
        options.AddArgument("window-size=1400,1200");

        return new EdgeDriver(options);
    }
}
