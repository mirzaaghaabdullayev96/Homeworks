namespace homework_6_Ref_Out_String
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //task 1
            //===========
            //Console.WriteLine(ValidatePassword("Mdsdmki_!45"));
            //===========

            string str1 = "  Hello    World";

            //task2
            //===========
            //Replace
            //need to specify one letter to be replaced.
            //Console.WriteLine(Replace(str1, 'b', 'a'));

            //Substring            
            //Console.WriteLine(SubString(str1,5));

            //Trim
            //Console.WriteLine(Trim(str1));
            //===========









        }


        #region task 1
        // 1. ValidatePassword(string password) - methodunuz olur, parameter olaraq qebul etdiyi stringin uzunlugu minimum 8, böyük hərflə başlamalı,
        // tərkibində minimum 1 rəqəm olmalı və minimum 1 karakter(hərf və ya rəqəm olmayan: misal?, !, @) olmalıdır.
        // Bu hallar ödənirsə true, ödənmirsə false qaytarmalıdır.

        /*static bool ValidatePassword(string password)
        {
            if (password.Length < 8)
                return false;

            if (!char.IsLetter(password[0]) || !char.IsUpper(password[0]))
                return false;

            if (!password.Any(char.IsDigit))
                return false;

            if (!password.Any(char.IsLetterOrDigit))
                return false;

            return true;

        }*/


        #endregion

        #region task 2 

        //2. String`in Replace(), Substring(),Trim() methodlarını custom şəkildə yazmaq.
        //Yəni sizin custom yazdığınız methodlarla stringin methodları eyni işi görməlidir.

        /*static string Replace(string myWord, char a, char newChar)
        {
            string newString = "";

            for (int i = 0; i < myWord.Length; i++)
            {
                if (myWord[i] == a)
                    newString += newChar;
                else
                    newString += myWord[i];
            }
            return newString;
        }

        static string SubString(string myWord, int a)
        {
            string newString = "";

            for (int i = a; i < myWord.Length; i++)
                newString += myWord[i];

            return newString;
        }

        static string Trim(string myWord)
        {
            int indexOfFirst = 0;
            int indexOfLast = 0;
            for (int i = 0; i < myWord.Length; i++)
            {
                if (myWord[i] !=' ')
                {
                    indexOfFirst = i;
                    break;
                }
            }

            for (int j = myWord.Length - 1; j > 0; j--)
            {
                if (myWord[j] != ' ')
                {
                    indexOfLast = j;
                    break;
                }
            }

            return myWord.Substring(indexOfFirst, indexOfLast - indexOfFirst+1);
        }*/






        #endregion


    }
}
