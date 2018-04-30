//AUTHOR: croneber
//FILENAME: EncryptWord.cs
//DATE: 4/29/2018
//REVISION HISTORY: second iteration
//REFERENCES: Reused some documentation and class design from the
//C++ version of this project I produced for CPSC 5011.

//Description: This class takes in a string and shifts all characters in the
//string by a constant amount. Guesses can be made as to what the shift is
//and statistics about these guesses will be stored in the object
//Dependencies: None
//Anticipated Use: Demonstrating abstraction via attribute hiding 
//and encapsulation.
//Assumptions: Legal input will be any string of standard alphabetical
//characters in upper or lowercase form, with no spaces and no punctuation
//characters. Input must be at least 4 characters long. Shifts will always be
//a positive integer amount between 1-25, inclusive.
//Strings stored in encryptWord will always be stored in all lowercase.
//States/Transitions: An encryptWord object is considered to be in the "on"
//state when encrypt is called and can only be reverted to the "off" state
//by calling the reset function. On construction, encryptWord will be 
//in the "off" state and will not transition into the "on" state until
//the encrypt function is called. The decrypt function will not change
//the state of the object. "On" and "off" are the only allowable valid states.
//Class invariants: encryptWord will never be in an invalid state--it can only
//be "on" or "off". Randomly generated shifts will never be negative or zero.
//Once a shift is generated, it will remain the same for
//the life of the object.
//Interface invariants: Cannot call encrypt functionality for a word that
//is already in the "on" state. Doing so will lead to undefined behavior.
//Cannot directly query the random shift value, but user can guess values and
//see if they are higher, lower or equal to the undisclosed shift.
//Cannot call getAvgValGuesses if no guesses have been made previously.
using System;
using System.Text;

public class EncryptWord
{
    private string word; //word to shift
    private int shift; //shift amount
    private int numQueries; //number of total guesses
    private int highGuesses; //number of high guesses
    private int lowGuesses; //number of low guesses
    private int guessValTotal; //sum of guessed values
    private Boolean isEncrypted; //state of the object
    private string originalWord; //inital word passed in for reset purposes


    //Constructor, takes in word to shift, generates a random shift value,
    //initializes numQueries, highGuesses, lowGuesses, and guessValTotal
    //to 0, sets state to "off. Sets word and originalWord to same string.
    //Pre: wordToEncrypt must follow input conventions described above
    //for valid input.
    //Post: encryptWord will be in an "off" state and will remain so
    //until encrypt is called. originalWord and word will also be the same
    //after construction.

    public EncryptWord(string wordToEncrypt)
    {
        word = wordToEncrypt.ToLower();
        originalWord = wordToEncrypt.ToLower();
        numQueries = 0;
        highGuesses = 0;
        lowGuesses = 0;
        guessValTotal = 0;
        isEncrypted = false;
        shift = generateRandomShift();
    }

    //Adds an constant, undisclosed shift amount to every character
    //in the stored word to create a stored, encrypted version of the word.
    //This encrypted version is also returned as a string to the user.
    //Pre: Object must be in "off" state prior to calling encrypt.
    //Post: word field will now be in encrypted form and object will
    //transition to the "on" state. 
    public string encrypt()
    {
        int currentCharAscii;
        StringBuilder sb = new StringBuilder();
        String encryptedWord;
        for (int i = 0; i < word.Length; i++)
        {
            currentCharAscii = (int)word[i];
            if (currentCharAscii + shift >= 123)
            {
                currentCharAscii = ((currentCharAscii + shift) % 123) + 97;
            }
            else
            {
                currentCharAscii += shift;
            }
            sb.Append((char)(currentCharAscii));

        }
        encryptedWord = sb.ToString();
        word = encryptedWord;
        isEncrypted = true;
        return encryptedWord;
    }

    //Checks the user guess against the undisclosed shift value and returns
    //an integer code indicating information about the guess relative to the
    //shift: -1 means guess was too low, 0 means guess was correct, 1 means
    //guess was too high.
    //Pre: Guess must be in the range of 1-25, inclusive
    //Post: None
    public int testGuess(int guess)
    {
        numQueries++;
        guessValTotal += guess;
        if (guess == shift)
        {
            return 0;
        }
        else if (guess > shift)
        {
            highGuesses += 1;
            return 1;
        }
        else
        {
            lowGuesses += 1;
            return -1;
        }
    }

    //Returns total number of guesses
    //Pre: None
    //Post: None
    public int getNumQueries()
    {
        return numQueries;
    }

    //Returns number of high guesses
    //Pre: None
    //Post: None
    public int getNumHighGuesses()
    {
        return highGuesses;
    }

    //Returns number of low guesses
    //Pre: None
    //Post: None
    public int getNumLowGuesses()
    {
        return lowGuesses;
    }

    //Returns average value of all guesses
    //Pre: Must have made some nonzero amount of guesses previously
    //Post: None
    public double getAvgValGuesses()
    {
        return guessValTotal / numQueries;
    }

    //Returns a string of the unencrypted word without changing
    //the internally stored word
    //Pre: None
    //Post: None
    public string decrypt()
    {
        return originalWord;
    }
    //Returns true if the object is in the "on" state, false if the object
    //is in the "off" state.
    //Pre: None
    //Post: None
    public Boolean isOn()
    {
        return isEncrypted;
    }

    //Reverts the state of the object to what it was immediately after
    //construction. All guess fields are reverted to zero, word field is
    //reverted to its unencrypted state and state is changed to "off".
    //Pre: None
    //Post: Object will be in "off" state with all fields reset to their
    //state immediately after constructor was called.
    public void reset()
    {
        word = originalWord;
        numQueries = 0;
        highGuesses = 0;
        lowGuesses = 0;
        guessValTotal = 0;
        isEncrypted = false;

    }

    //Generates random number for shift between 1-25, inclusive
    private int generateRandomShift()
    {
        Random rand = new Random();
        int randShift = rand.Next(1, 26);
        return randShift;
    }
}
