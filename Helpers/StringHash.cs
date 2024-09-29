using System.Security.Cryptography;

namespace zeno_copenhagen.Helpers;

public static class StringHash
{
    private readonly static MD5 _md5;

    static StringHash()
    {
        _md5 = MD5.Create();
    }

    [DebuggerStepThrough]
    public static Guid Hash(string input)
    {

        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hashBytes = _md5.ComputeHash(inputBytes);

        return new Guid(hashBytes);
    }
}
