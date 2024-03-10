class CaesarCipher
{
    private const int LOWERCASE_STARTING = 'a';
    private const int LOWERCASE_ENDING = 'z';
    private const int UPPERCASE_STARTING = 'A';
    private const int UPPERCASE_ENDING = 'Z';

    public static string FromWord(string text, int shiftCount)
    {
        string word = "";
        foreach (char c in text.ToCharArray())
        {
            int asciiInt = c;
            if (asciiInt <= LOWERCASE_ENDING && asciiInt >= LOWERCASE_STARTING)
            {
                int value = (asciiInt - LOWERCASE_STARTING + shiftCount) % 26;
                word += (char) (value + LOWERCASE_STARTING);
            } else if (asciiInt <= UPPERCASE_ENDING && asciiInt >= UPPERCASE_STARTING) {
                int value = (asciiInt - UPPERCASE_STARTING + shiftCount) % 26;
                word += (char) (value + UPPERCASE_STARTING);
            }
            else {
                word += c;
            }
        } 
        return word;
    }
}