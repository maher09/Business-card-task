namespace Business_Card.Utilities
{
    public class Base64Helper
    {
        public static bool IsValidBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return false;

            Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
            return Convert.TryFromBase64String(base64String, buffer, out int bytesParsed);
        }

      
        public static int GetBase64DecodedSize(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return 0;

            // Remove any whitespace or line breaks
            base64String = base64String.Trim();

            // Calculate padding
            int padding = 0;
            if (base64String.EndsWith("=="))
                padding = 2;
            else if (base64String.EndsWith("="))
                padding = 1;

            // Calculate the length in bytes
            return (base64String.Length * 3) / 4 - padding;
        }
    }
}
