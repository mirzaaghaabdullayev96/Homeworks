namespace homework_5_methods
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //task 1 
            //=====
            //SumVoid(9);
            //Console.WriteLine(Sum(9));
            //=====

            //task 2
            //=====
            //Console.WriteLine(Remove("  Code  Academy     "));
            //=====

            //task3
            //=====
            //FirstLetter(" Hello World $@ 2 4 my class   ");
            //=====

        }

        #region task 1 

        /*static int Sum(int x)
        {
            int sum = 0;

            for (int i = 2; i <= x; i += 2)
            {
                sum += i;
            }

            return sum;
        }

        static void SumVoid(int x)
        {
            int sum = 0;

            for (int i = 2; i <= x; i += 2)
            {
                sum += i;
            }

            Console.WriteLine(sum);
        }*/

        #endregion


        #region task 2


        /*static string Remove (string word)
        {
            string myNewWord = "";
            for (int i = 0; i < word.Length; i++)
            {
                if (word[i] ==' ')
                {
                    continue;
                }
                myNewWord += word[i];
            }
            return myNewWord;
        }*/


        #endregion


        #region task 3


        /*static void FirstLetter(string myWord)
        {
            //Checking first position
            if (!CheckLetter(myWord[0]))
                Console.Write(myWord[0] + " ");

            //Checking remainig positions 
            for (int i = 1; i < myWord.Length; i++)
            {
                if (!CheckLetter(myWord[i]) && CheckLetter(myWord[i - 1]))
                    Console.Write(myWord[i] + " ");
            }
        }


        //checking if myWord[i] is letter
        static bool CheckLetter(char myWord)
        {
            char[] letters = { ' ', '-', '%', '#', '@', '!', '&', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '$' }; //we can add any symbol here

            for (int i = 0; i < letters.Length; i++)
            {
                if (letters[i] == myWord)
                    return true;
            }
            return false;
        }*/


        #endregion



    }
}
