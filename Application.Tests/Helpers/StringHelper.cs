using System.Text;

namespace Application.Tests.Helpers;

internal static class StringHelper
{
    public static string GetStringWithLength(int length)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            sb.Append("x");
        }

        return sb.ToString();
    }
}
