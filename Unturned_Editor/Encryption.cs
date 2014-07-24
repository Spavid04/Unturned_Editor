using System.Text;

public static class Encryption
{
    public static string decrypt(string input)
    {
        //this transforms Default-encoded strings (any string we read from anywhere on Windows) into UTF8 (the thing that is decryptable)
        input = Encoding.UTF8.GetString(Encoding.Default.GetBytes(input));

        //empty to-return string
        string toreturn = "";

        for (int i = input.Length - 1; i >= 0; i--)
        {
            //here is the nice part as every character is shifted by 32 (which, interestingly, is ' ')
            //%255 is there so we can safely transform any int into a character
            toreturn += (char) ((input[i] - 32)%255);
        }

        return toreturn;
    }

    public static string encrypt(string input)
    {
        //empty to-return string
        string toreturn = "";

        for (int i = input.Length - 1; i >= 0; i--)
        {
            //here is the nice part as every character is shifted by 32 (which, interestingly, is ' ') and because we MODed by 255, add 8160 (through observations, it seems that 8160 is what we cut out with MOD 255)
            toreturn += (char) (input[i] + 32 + 8160);
        }

        toreturn = Encoding.Default.GetString(Encoding.UTF8.GetBytes(toreturn));
        return toreturn;
    }
}