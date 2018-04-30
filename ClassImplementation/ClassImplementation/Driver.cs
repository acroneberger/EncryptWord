using System;


public class Driver
{
    private EncryptWord ew;
    private String userWord;
    //Encapsulates EncryptWord object
    //Pre: None
    //Post: None
    public Driver(String userWord)
    {
        ew = new EncryptWord(userWord);
        this.userWord = userWord;
    }

    //Tests functionality of EncryptWord
    //Pre: None
    //Post: None
    public void testFunctions()
    {
        Console.WriteLine("Is on? " + ew.isOn());
        Console.WriteLine("Encrypting " + userWord);
        String encrypted = ew.encrypt();
        Console.WriteLine("Is on? " + ew.isOn());
        Console.WriteLine("Word is now: " + encrypted);
        testGuessing();
        double q = ew.getAvgValGuesses();
        int n = ew.getNumQueries();
        Console.WriteLine("Average query value: " + q.ToString());
        Console.WriteLine("Number of guesses: " + n.ToString());
        
        Console.WriteLine("Original word: " + ew.decrypt());
        Console.WriteLine("Resetting");
        ew.reset();
        Console.WriteLine("Is on? " + ew.isOn());
        Console.WriteLine("Number of guesses: " + n.ToString());
        Console.WriteLine("End of test");
    }
    //Tests guessing functionality
    //Pre: none
    //Post: none
    private void testGuessing()
    {
        int result;
        for (int i = 1; i < 26; i++)
        {
            Console.WriteLine("Testing " + i);
            result = ew.testGuess(i);
            if (result == 0)
            {
                Console.WriteLine("Correct Shift");
            }
            else if (result == 1)
            {
                Console.WriteLine("Too High");
            }
            else
            {
                Console.WriteLine("Too Low");
            }
        }

    }

}