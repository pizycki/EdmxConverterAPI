using System.Text;

public static class HtmlHelpers
{
    public static string EncodeHtml(string html)
    {
        if (string.IsNullOrEmpty(html))
            throw new ArgumentException(nameof(html));

        var sb = new StringBuilder(html);
        sb.Replace("<", "&lt;");
        sb.Replace(">", "&gt;");
        return sb.ToString();
    }
}