namespace UnitTests;

public class Tests
{
    [TestFixture]
    public class BirthdayCheckerTests
    {
        [Test]
        public void today_is_birthday()
        {
            Assert.True(true);
        }

        [Test]
        public void today_is_not_birthday()
        {
            Assert.True(true);
        }
    }
}

public class BirthdayChecker
{
    public bool IsBirthday()
    {
        //todo: 完成兩個測試案例
        var today = DateTime.Today;
        if (today.Month == 10 && today.Day == 10)
        {
            return true;
        }
        return false;
    }
}