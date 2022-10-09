namespace Domain.Users.SharedKernel
{
    public class KeyProvider
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public const string Key = "vc*T5EB9Y%2++cbAMcQfTjWnZr4u7w!z";

        public static string CreateNewSalt(int maxIvLength = 16)
        {
            Random random = new();
            return new string(Enumerable.Repeat(chars, maxIvLength).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
