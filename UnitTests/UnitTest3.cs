namespace UnitTests;

[TestFixture]
public class AuthenticationServiceTests
{
    [Test]
    public void isValid_true_log_should_not_execute()
    {
        var target = new AuthenticationService();

        var actual = target.IsValid("titansoft", "10314159");

        //always failed
        Assert.IsTrue(actual);
    }

    [Test]
    public void isValid_false_log_should_execute()
    {
        var target = new AuthenticationService();

        var actual = target.IsValid("titansoft", "10314159");

        //always failed
        Assert.IsTrue(actual);
    }
}

public class AuthenticationService
{
    public bool IsValid(string account, string passCode)
    {
        // 根據 account 取得自訂密碼
        var profileDao = new ProfileDao();
        var passwordFromDao = profileDao.GetPassword(account);

        // 根據 account 取得 RSA token 目前的亂數
        var rsaToken = new RsaTokenDao();
        var randomCode = rsaToken.GetRandom(account);

        // 驗證傳入的 passCode 是否等於自訂密碼 + RSA token亂數
        var validPasscode = passwordFromDao + randomCode;
        var isValid = passCode == validPasscode;

        if (!isValid)
        {
            // todo, 如何驗證當有非法登入的情況發生時，有正確地記錄log？
            var content = string.Format("account:{0} try to login failed", account);
            Console.Write(content);
        }

        return isValid;

    }
}

public class ProfileDao
{
    public string GetPassword(string account)
    {
        return Context.GetPassword(account);
    }
}

public static class Context
{
    public static Dictionary<string, string> profiles;

    static Context()
    {
        profiles = new Dictionary<string, string>();
        profiles.Add("titansoft", "10");
    }

    public static string GetPassword(string key)
    {
        return profiles[key];
    }
}

public class RsaTokenDao
{
    public string GetRandom(string account)
    {
        var seed = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        var result = seed.Next(0, 999999).ToString("000000");
        Console.WriteLine("randomCode:{0}", result);

        return result;
    }
}


