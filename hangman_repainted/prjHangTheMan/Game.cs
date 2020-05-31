using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjHangTheMan
{
    public partial class Game : Form
    {
        SoundPlayer player = new SoundPlayer();//Initialize the music player

        /***********************
        **Variables Start Here**
        ***********************/

        Random random = new Random();//Initialize the random object

        String randomWord = "";//To hold our random word that the user must guess
        String lastWord = "";//The word from the previous round
        String letter = "";//Holds whatever letter is chosen        
        String underscoresBefore = "";//Number of underscores before the guessed letter
        String underscoresAfter = "";//Number of underscores after the guessed letter
        String a = "";//These letter variables are used for guessing words
        String b = "";
        String c = "";
        String d = "";
        String e = ""; 
        String f = "";
        String g = "";
        String h = "";
        String i = "";
        String j = "";
        String k = "";
        String l = "";
        String m = "";
        String n = "";
        String o = "";
        String p = "";
        String q = "";
        String r = "";
        String s = "";
        String t = "";
        String u = "";
        String v = "";
        String w = "";
        String x = "";
        String y = "";
        String z = "";

        int guessWrong = 0;//Number of wrong guesses in the current round
        int guessWrongTotal = 0;//Total number of wrong guesses

        Boolean typed = false;//Used to tell whether the user typed the word in the box
        
        /*These 3 variables are declared as public static. The "public" means that they can be accessed in any form, 
         * and the "static" means the variables will always be the same thing in every form.
         * score needs to be public, because after the player loses and returns to the menu, we need to "send" their score
         * to the highscore[] array and check if they got a high score.
         * music is also public, because the music can be turned on or off in either form.*/

        public static int score = 0;//In our game it is possible to get a negative score. It is better that way.
        public static Boolean music = false;//Represents whether the music should be playing or not. 

        /*********************
        **Variables End Here**
        *********************/

        /*********************
        **Arrays Start Here**
        *********************/

        String[] Word = new String[53];//Will hold all of our words.
        String[] loader = new String[53];//Use to load words into the text file.

        /*********************
        **Arrays End Here**
        *********************/

        /***********************
        **Functions Start Here**
        ***********************/

        /**********************************************
        *The following 12 functions have no parameters*
        **********************************************/

        void letterCheckAndReplace()
        {
            /*this function assigns lblGuess.Text to underscoresBefore before the letterReplace function
             and lblGuess.Text to underscoresAfter after the letterReplace function.
             this is to check if the string has changed or not to determine whether the player got a letter in the word or not*/
            underscoresBefore = lblGuess.Text;
            //go to the function for more information
            lblGuess.Text = letterReplace(letter, randomWord, a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z);
            underscoresAfter = lblGuess.Text;
        }

        void completeWord()//The player finished a word. They've won the round!
        {
            lblOutput.Text = "Congragulations, you just completed a word!" + "\n" + "Click 'Continue' to keep playing!";//Output message
            
            if (typed == true)//They've typed the word
            {
                score += 50 * numberUnderscores(underscoresBefore, underscoresAfter);//Give them 50 points per letter that hasn't been already guessed
                lblScore.Text = Convert.ToString(score);//Show the new score
            }
            else//THey've guessed normally - by clicking the individual letters
            {
                score += 50;//Give the player 50 points for finishing the word
                lblScore.Text = Convert.ToString(score);//Show the new score
            }
            buttonsOff();//Disables all the letter buttons to prevent unwanted actions
            lblContinue.Enabled = true;//Enables the Continue button to be able to continue
        }

        void lose()//Either they typed the word and got it wrong, or chose 6 wrong letters. Game over!
        {
            lblOutput.Text = "Dang man, you lose! The word was " + randomWord + ".\n" + "Click 'Menu' to return to the menu.";//Output message
            buttonsOff();//Disables all the letter buttons to prevent unwanted actions
            lblContinue.Enabled = false;//Enables the Continue button to be able to continue
        }

        void buttonsOff()//Disable all of the buttons and textbox
        {
            lblA.Enabled = false;
            lblB.Enabled = false;
            lblC.Enabled = false;
            lblD.Enabled = false;
            lblE.Enabled = false;
            lblF.Enabled = false;
            lblG.Enabled = false;
            lblH.Enabled = false;
            lblI.Enabled = false;
            lblJ.Enabled = false;
            lblK.Enabled = false;
            lblL.Enabled = false;
            lblM.Enabled = false;
            lblN.Enabled = false;
            lblO.Enabled = false;
            lblP.Enabled = false;
            lblQ.Enabled = false;
            lblR.Enabled = false;
            lblS.Enabled = false;
            lblT.Enabled = false;
            lblU.Enabled = false;
            lblV.Enabled = false;
            lblW.Enabled = false;
            lblX.Enabled = false;
            lblY.Enabled = false;
            lblZ.Enabled = false;
            lblGuessWord.Enabled = false;
            txtWordGuess.Enabled = false;
        }

        void buttonsOn()//Enable all the buttons and textbox
        {
            lblA.Enabled = true;
            lblB.Enabled = true;
            lblC.Enabled = true;
            lblD.Enabled = true;
            lblE.Enabled = true;
            lblF.Enabled = true;
            lblG.Enabled = true;
            lblH.Enabled = true;
            lblI.Enabled = true;
            lblJ.Enabled = true;
            lblK.Enabled = true;
            lblL.Enabled = true;
            lblM.Enabled = true;
            lblN.Enabled = true;
            lblO.Enabled = true;
            lblP.Enabled = true;
            lblQ.Enabled = true;
            lblR.Enabled = true;
            lblS.Enabled = true;
            lblT.Enabled = true;
            lblU.Enabled = true;
            lblV.Enabled = true;
            lblW.Enabled = true;
            lblX.Enabled = true;
            lblY.Enabled = true;
            lblZ.Enabled = true;
            lblGuessWord.Enabled = true;
            txtWordGuess.Enabled = true;
        }

        void letterReset()
        {
            a = "";
            b = "";
            c = "";
            d = "";
            e = "";
            f = "";
            g = "";
            h = "";
            i = "";
            j = "";
            k = "";
            l = "";
            m = "";
            n = "";
            o = "";
            p = "";
            q = "";
            r = "";
            s = "";
            t = "";
            u = "";
            v = "";
            w = "";
            x = "";
            y = "";
            z = "";
        }

        void incorrect()//When the guessed letter is wrong
        {
            lblOutput.Text = "Oops, there is no " + letter + "!";//Output message
            score -= 3;//Subtract 3 points
            lblScore.Text = Convert.ToString(score);//Show the new score
            guessWrongTotal += 1;//adds one to the total amount of wrong guesses the player made
            guessWrong += 1;//adds one to the number of wrong guesses the player made for a word
            lblWrong.Text = Convert.ToString(guessWrongTotal);//displays guessWrongTotal
            pbHang.Image = Image.FromFile("Images/HangMan" + guessWrong + ".png");//changes pbHang to the next stage of the "hanging" based from the guessWrong variable
        }

        void correct()//When the guessed letter is right
        {
            lblOutput.Text = "Nice, there is a letter " + letter + "!";//Output message
            score += 5 * numberUnderscores(underscoresBefore, underscoresAfter);//Add points
            lblScore.Text = Convert.ToString(score);//Show the new score
        }        

        void checkGuess()//Checks if the typed word is correct
        {
            String guessedWord = txtWordGuess.Text;//Sets guessWord to the Text of txtWordGuess given by the players input
            guessedWord = guessedWord.ToLower();//Converts all the characters to lowercase
            txtWordGuess.Text = "";//removes the text in txtWordGuess
            if (guessedWord == randomWord)//checks if guessWord is equal to the randomWord
            {               
                String end = "";
                for (int x = 0; x < randomWord.Length; x++)//This loop adds spaces between each letter in the randomWord
                {
                    end += (randomWord.Substring(x, 1) + " ");
                }
                lblGuess.Text = end;//Display the word with spaces between each letter, now it looks the same as if you guessed it normally
                underscoresAfter = lblGuess.Text;//Used for scoring
                completeWord();//
                
            }
            else if (guessedWord == "")
            {
                //Nothing happens if the textbox is blank
                //This makes sure that the player won't lose the game if they accidentaly click the Guess button
            }
            else
            {
                lose();//The player lost. Guessing the word by typing is a gamble, you only get one shot.
            }
        }

        void toggleMusic()
        {
            if (music == true)
            {
                pbMusic.Image = Image.FromFile("Images/musicoff.png");//Change the image
                music = false;
                player.Stop();//Stop the music
            }
            else
            {
                pbMusic.Image = Image.FromFile("Images/musicon.png");//Change the image
                music = true;
                player.PlayLooping();//Play the music
            }
        }

        /****************************
        *This function is recursive!*
        ****************************/
        void loadWords()
        {
            int x = 0;
            string line;
            if (File.Exists("words.txt") == true)
            {
                // Read the file line by line, putting each value into the highScore array
                StreamReader file = new StreamReader("words.txt");
                while ((line = file.ReadLine()) != null)
                {
                    Word[x] = line;
                    x++;
                }
                file.Close();//Close the file. The program will glitch out if we don't.
            }
            else//This means that words.txt doesn't exist. Create it with our default words.
            {
                loader[0] = "nifty";
                loader[1] = "brick";
                loader[2] = "microwave";
                loader[3] = "lucky";
                loader[4] = "bombs";
                loader[5] = "powder";
                loader[6] = "nintendo";
                loader[7] = "mario";
                loader[8] = "fire";
                loader[9] = "spongebob";
                loader[10] = "transcript";
                loader[11] = "klutz";
                loader[12] = "bookkeeper";
                loader[13] = "chophouse";
                loader[14] = "handkerchief";
                loader[15] = "hangman";
                loader[16] = "bamboozled";
                loader[17] = "cycle";
                loader[18] = "cobweb";
                loader[19] = "wizard";
                loader[20] = "twelfth";
                loader[21] = "keyhole";
                loader[22] = "lanschool";
                loader[23] = "jackblack";
                loader[24] = "crypt";
                loader[25] = "bandwagon";
                loader[26] = "blitz";
                loader[27] = "injury";
                loader[28] = "meme";
                loader[29] = "oxygen";
                loader[30] = "subway";
                loader[31] = "strength";
                loader[32] = "stretch";
                loader[33] = "vaporize";
                loader[34] = "abruptly";
                loader[35] = "beekeeper";
                loader[36] = "pepper";
                loader[37] = "salt";
                loader[38] = "jumbo";
                loader[39] = "wavy";
                loader[40] = "scratch";
                loader[41] = "jukebox";
                loader[42] = "yummy";
                loader[43] = "jawbreaker";
                loader[44] = "galaxy";
                loader[45] = "ivy";
                loader[46] = "joking";
                loader[47] = "funny";
                loader[48] = "jazzy";
                loader[49] = "kiwi";
                loader[50] = "jelly";
                loader[51] = "kazoo";
                loader[52] = "zombie";
                StreamWriter file = new StreamWriter("words.txt");
                while (x<loader.Length)
                {
                    file.WriteLine(loader[x]);
                    x++;
                }
                file.Close();
                loadWords();//Load the words into the program
            }
        }

        /********************************************
        **The following 3 functions have parameters**
        ********************************************/

        int numberUnderscores(String underscoresBefore, String underscoresAfter)
        {
            //function gets the difference between the amount of underscores that were in the string before and after the letterReplace function.
            //this will be the number of underscores replaced by the letter.
            //used to update the score (5 times the amount of underscores replaced)
            //Since it is 5 points per letter, this will make sure you don't get 5 points if the letter you guessed appears twice, you'll get the 10 you deserve.
            int numUnderscoresBefore = 0;
            int numUnderscoresAfter = 0;
            int numUnderscoresDifference = 0;
            for (int x = 0; x < underscoresBefore.Length; x++)
            {
                String character = underscoresBefore.Substring(x, 1);
                if (character == "_")
                {
                    numUnderscoresBefore += 1;
                }
            }
            for (int x = 0; x < underscoresAfter.Length; x++)
            {
                String character = underscoresAfter.Substring(x, 1);
                if (character == "_")
                {
                    numUnderscoresAfter += 1;
                }
            }
            numUnderscoresDifference = numUnderscoresBefore - numUnderscoresAfter;
            return numUnderscoresDifference;
        }

        String guessSpaces(String word)
        {
            //The return string
            String underscores = "";
            String FUnderscore = "_";//a string with an underscore for the first iteration of the for loop
            String SUnderscore = " _"; //a string with an underscore and a space for all iterations of the loop except for the first one
            for (int x = 0; x < word.Length; x++)
            {
                //iterates for the first time through the loop
                if (x == 0)
                {
                    underscores = FUnderscore;
                }
                //iterates every time except the first
                else
                {
                    //adds " _" to the return string
                    underscores += SUnderscore;
                }
            }
            return underscores;
        }

        String letterReplace(string letter, string word, string a, string b, string c, string d, string e, string f, string g, string h, string i, string j, string k, string l, string m, string n, string o, string p, string q, string r, string s, string t, string u, string v, string w, string x, string y, string z)
        {
            String replacedWord = "";//The return string
            for (int xx = 0; xx < word.Length; xx++)
            {
                //gets the Nth character of the word for the iteration number
                String wordLetter = word.Substring(xx, 1);
                //for the last character of the word
                if (xx == (word.Length - 1))
                {
                    //checks if the character is the letter that was chosen by the player
                    if (wordLetter == letter)
                    {
                        //adds the letter to the string
                        replacedWord += letter;
                    }
                    else
                    {
                        //this checks if a letter button has already been pressed and will display them in the string
                        if (wordLetter == a)
                        {
                            //adds the letter to the string
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == b)
                        {
                            //etc...
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == c)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == d)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == e)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == f)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == g)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == h)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == i)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == j)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == k)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == l)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == m)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == n)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == o)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == p)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == q)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == r)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == s)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == t)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == u)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == v)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == w)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == x)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == y)
                        {
                            replacedWord += wordLetter;
                        }
                        else if (wordLetter == z)
                        {
                            replacedWord += wordLetter;
                        }
                        else
                        {
                            //adds an underscore to the string
                            replacedWord += "_";
                        }
                    }
                }
                //for all characters except the last character of the word
                else
                {
                    //checks if the character is the letter that was chosen by the player
                    if (wordLetter == letter)
                    {
                        //adds the letter with a space to the string
                        replacedWord += letter + " ";
                    }
                    else
                    {
                        //this checks if a letter button has already been pressed and will display them in the string
                        if (wordLetter == a)
                        {
                            //adds the letter plus a space to the string
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == b)
                        {
                            //etc...
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == c)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == d)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == e)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == f)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == g)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == h)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == i)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == j)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == k)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == l)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == m)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == n)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == o)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == p)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == q)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == r)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == s)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == t)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == u)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == v)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == w)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == x)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == y)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else if (wordLetter == z)
                        {
                            replacedWord += wordLetter + " ";
                        }
                        else
                        {
                            replacedWord += "_ ";
                        }
                    }
                }
            }
            return replacedWord;
        }

        /*********************
        **Functions End Here**
        *********************/

        public Game()
        {
            //Set up the music to play the sound file we want.
            InitializeComponent();
            player.SoundLocation = "Audio/Bob-ombBattlefield.wav";            
        }
        
        private void Form1_Load(object sender, EventArgs evnt)//Code when the form loads
        {
            loadWords();//Loads the guessing words into elements            
            if (music == true)//Check if the music is already playing, change the music button accordingly
            {
                pbMusic.Image = Image.FromFile("Images/musicon.png");
            }
            else
            {
                pbMusic.Image = Image.FromFile("Images/musicoff.png");
            }
            score = 0;//sets score to zero so the you dont go back into the game with your previous score
            randomWord = Word[random.Next(0, 53)];//selects a random number to find a random word from the Word[] array
            lblContinue.Enabled = false; //lblContinue is disabled to prevent unwanted actions
            lblGuess.Text = guessSpaces(randomWord);//lblGuess.Text is assigned underscores with spaces in between them for the number of characters in the word
            lblOutput.Text = "Guess a letter to find my word!";//Output message
        }

        /***********************************************************
        *The following events are for when the user clicks a button*
        ***********************************************************/

        private void pbMusic_Click(object sender, EventArgs e)//The music button
        {
            toggleMusic();//Toggle the music on or off
        }

        private void lblContinue_Click(object sender, EventArgs evnt)//The "Continue" button
        {
            buttonsOn();//Turns all the letter buttons on
            letterReset();//Resets all the letter variables to an empty string to prevent letterReplace from thinking that you already pressed those buttons
            lastWord = randomWord;//Save the round's word before we get a new one
            randomWord = Word[random.Next(0, 53)];//selects a random number to find a random word from the Word[] array
            while (lastWord == randomWord)//Make sure we don't have the same word again
            {
                randomWord = Word[random.Next(0, 53)];//Get a new word
            }
            lblContinue.Enabled = false;//Disables the continue button to prevent a reset at any time
            lblGuess.Text = guessSpaces(randomWord);//lblGuess.Text is assigned underscores with spaces inbetween them for the number of characters in the word
            guessWrong = 0;//resets guessWrong to zero so you have 6 wrong guesses for the new word
            pbHang.Image = Image.FromFile("../../Resources/Images/HangMan0.png");//resets the picture box to the first stage of hangman
            lblOutput.Text = "Guess a letter to find my word";//Output message
        }

        private void lblGuessWord_Click(object sender, EventArgs e)//The "Guess" button
        {
            typed = true;
            underscoresBefore = lblGuess.Text;
            checkGuess();
            typed = false;
        }

        /********************************************
        *Click events for each of the letter buttons*
        ******They're all pretty much the same*******
        ********************************************/

        private void lblA_Click(object sender, EventArgs evnt)
        {

            letter = "a";//the letter to find
            letterCheckAndReplace();//go to the function for more information
            if (underscoresBefore == underscoresAfter)//checks if the string before replacing the underscores matches the string after
            {
                incorrect();//go to the function for more information
                a = "a";//sets the a variable to "a" so the letterReplace function will not remove the letter from lblGuess.Text
                lblA.Enabled = false;//Disables the label for that letter
            }
            else
            {
                correct();//go to the function for more information
                a = "a";//sets the a variable to "a" so the letterReplace function will not remove the letter from lblGuess.Text
                lblA.Enabled = false;//Disables the label for that letter
            }

            if (randomWord == lblGuess.Text.Replace(" ", ""))//checks if the randomWord is the same as lbl guess with the spaces removed to determine if you won
            {
                completeWord();//go to the function for more information
            }

            if (guessWrong == 6)//checks if you made 6 mistakes
            {
                lose();//go to the function for more information
            }
            //This code repeats for each letter label with a few changes to what letter is used.
            //Not going to repeat the documentation for those labels.
        }

        private void lblB_Click(object sender, EventArgs evnt)
        {
            letter = "b";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                b = "b";
                lblB.Enabled = false;
            }
            else
            {
                correct();
                b = "b";
                lblB.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblC_Click(object sender, EventArgs evnt)
        {
            letter = "c";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                c = "c";
                lblC.Enabled = false;
            }
            else
            {
                correct();
                c = "c";
                lblC.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblD_Click(object sender, EventArgs evnt)
        {
            letter = "d";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                d = "d";
                lblD.Enabled = false;
            }
            else
            {
                correct();
                d = "d";
                lblD.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblE_Click(object sender, EventArgs evnt)
        {
            letter = "e";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                e = "e";
                lblE.Enabled = false;
            }
            else
            {
                correct();
                e = "e";
                lblE.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblF_Click(object sender, EventArgs evnt)
        {
            letter = "f";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                f = "f";
                lblF.Enabled = false;
            }
            else
            {
                correct();
                f = "f";
                lblF.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblG_Click(object sender, EventArgs evnt)
        {
            letter = "g";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                g = "g";
                lblG.Enabled = false;
            }
            else
            {
                correct();
                g = "g";
                lblG.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblH_Click(object sender, EventArgs evnt)
        {
            letter = "h";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                h = "h";
                lblH.Enabled = false;
            }
            else
            {
                correct();
                h = "h";
                lblH.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblI_Click(object sender, EventArgs evnt)
        {
            letter = "i";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                i = "i";
                lblI.Enabled = false;
            }
            else
            {
                correct();
                i = "i";
                lblI.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblJ_Click(object sender, EventArgs evnt)
        {
            letter = "j";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                j = "j";
                lblJ.Enabled = false;
            }
            else
            {
                correct();
                j = "j";
                lblJ.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblK_Click(object sender, EventArgs evnt)
        {
            letter = "k";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                k = "k";
                lblK.Enabled = false;
            }
            else
            {
                correct();
                k = "k";
                lblK.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblL_Click(object sender, EventArgs evnt)
        {
            letter = "l";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                l = "l";
                lblL.Enabled = false;
            }
            else
            {
                correct();
                l = "l";
                lblL.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblM_Click(object sender, EventArgs evnt)
        {
            letter = "m";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                m = "m";
                lblM.Enabled = false;
            }
            else
            {
                correct();
                m = "m";
                lblM.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblN_Click(object sender, EventArgs evnt)
        {
            letter = "n";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                n = "n";
                lblN.Enabled = false;
            }
            else
            {
                correct();
                n = "n";
                lblN.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblO_Click(object sender, EventArgs evnt)
        {
            letter = "o";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                o = "o";
                lblO.Enabled = false;
            }
            else
            {
                correct();
                o = "o";
                lblO.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblP_Click(object sender, EventArgs evnt)
        {
            letter = "p";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                p = "p";
                lblP.Enabled = false;
            }
            else
            {
                correct();
                p = "p";
                lblP.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblQ_Click(object sender, EventArgs evnt)
        {
            letter = "q";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                q = "q";
                lblQ.Enabled = false;
            }
            else
            {
                correct();
                q = "q";
                lblQ.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblR_Click(object sender, EventArgs evnt)
        {
            letter = "r";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                r = "r";
                lblR.Enabled = false;
            }
            else
            {
                correct();
                r = "r";
                lblR.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblS_Click(object sender, EventArgs evnt)
        {
            letter = "s";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                s = "s";
                lblS.Enabled = false;
            }
            else
            {
                correct();
                s = "s";
                lblS.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblT_Click(object sender, EventArgs evnt)
        {
            letter = "t";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                t = "t";
                lblT.Enabled = false;
            }
            else
            {
                correct();
                t = "t";
                lblT.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblU_Click(object sender, EventArgs evnt)
        {
            letter = "u";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                u = "u";
                lblU.Enabled = false;
            }
            else
            {
                correct();
                u = "u";
                lblU.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblV_Click(object sender, EventArgs evnt)
        {
            letter = "v";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                v = "v";
                lblV.Enabled = false;
            }
            else
            {
                correct();
                v = "v";
                lblV.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblW_Click(object sender, EventArgs evnt)
        {
            letter = "w";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                w = "w";
                lblW.Enabled = false;
            }
            else
            {
                correct();
                w = "w";
                lblW.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblX_Click(object sender, EventArgs evnt)
        {
            letter = "x";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                x = "x";
                lblX.Enabled = false;
            }
            else
            {
                correct();
                x = "x";
                lblX.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblY_Click(object sender, EventArgs evnt)
        {
            letter = "y";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                y = "y";
                lblY.Enabled = false;
            }
            else
            {
                correct();
                y = "y";
                lblY.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }

        private void lblZ_Click(object sender, EventArgs evnt)
        {
            letter = "z";
            letterCheckAndReplace();
            if (underscoresBefore == underscoresAfter)
            {
                incorrect();
                z = "z";
                lblZ.Enabled = false;
            }
            else
            {
                correct();
                z = "z";
                lblZ.Enabled = false;
            }
            if (randomWord == lblGuess.Text.Replace(" ", ""))
            {
                completeWord();
            }
            if (guessWrong == 6)
            {
                lose();
            }
        }
        /*****************
        *End click events*
        *****************/

        /*****************************************
        *The following events are for the textbox*
        *****************************************/

        private void txtWordGuess_KeyDown(object sender, KeyEventArgs e)//This event occurs when you press any key while in the textbox
        {            
            if (e.KeyCode == Keys.Enter)//If you've pressed enter, do the same thing as the click event for the guess button
            {
                typed = true;//Tell the program you typed the wrod
                underscoresBefore = lblGuess.Text;
                checkGuess();
            }
            typed = false;//Reset the typed variable
        }

        private void txtWordGuess_KeyPress(object sender, KeyPressEventArgs e)//This event occurs when a key is pressed
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))//Check if the character is a letter
            {
                e.Handled = true;
            }
        }

        private void txtWordGuess_TextChanged(object sender, EventArgs e)//This event occurs when the text inside the textbox is changed
        {
            if (Regex.IsMatch(txtWordGuess.Text, "  ^ [a-zA-Z]"))//This prevents any non-letter character from being entered
            {
                txtWordGuess.Text = "";
            }
        }

        /*******************
        *End textbox events*
        *******************/

        /*****************************************************************
        *The following events are for when the mouse hovers over a button*
        *****************************************************************/

        private void lblContinue_MouseEnter(object sender, EventArgs e)
        {
            lblContinue.BackColor = Color.Blue;
        }

        private void lblContinue_MouseLeave(object sender, EventArgs e)
        {
            lblContinue.BackColor = System.Drawing.Color.FromKnownColor(KnownColor.Highlight);//highlight
        }

        private void lblA_MouseEnter(object sender, EventArgs evnt)
        {
            //changes the backColour of the label to deep sky blue when hovering over it
            lblA.BackColor = Color.Teal;
        }

        private void lblA_MouseLeave(object sender, EventArgs evnt)
        {
            //changes the backColour of the label to the original colour when not hovering over it anymore
            lblA.BackColor = Color.Turquoise;
        }

        private void lblB_MouseEnter(object sender, EventArgs evnt)
        {
            lblB.BackColor = Color.Teal;
        }

        private void lblB_MouseLeave(object sender, EventArgs evnt)
        {
            lblB.BackColor = Color.Turquoise;
        }

        private void lblC_MouseEnter(object sender, EventArgs evnt)
        {
            lblC.BackColor = Color.Teal;
        }

        private void lblC_MouseLeave(object sender, EventArgs evnt)
        {
            lblC.BackColor = Color.Turquoise;
        }

        private void lblD_MouseEnter(object sender, EventArgs evnt)
        {
            lblD.BackColor = Color.Teal;
        }

        private void lblD_MouseLeave(object sender, EventArgs evnt)
        {
            lblD.BackColor = Color.Turquoise;
        }

        private void lblE_MouseEnter(object sender, EventArgs evnt)
        {
            lblE.BackColor = Color.Teal;
        }

        private void lblE_MouseLeave(object sender, EventArgs evnt)
        {
            lblE.BackColor = Color.Turquoise;
        }

        private void lblF_MouseEnter(object sender, EventArgs evnt)
        {
            lblF.BackColor = Color.Teal;
        }

        private void lblF_MouseLeave(object sender, EventArgs evnt)
        {
            lblF.BackColor = Color.Turquoise;
        }

        private void lblG_MouseEnter(object sender, EventArgs evnt)
        {
            lblG.BackColor = Color.Teal;
        }

        private void lblG_MouseLeave(object sender, EventArgs evnt)
        {
            lblG.BackColor = Color.Turquoise;
        }

        private void lblH_MouseEnter(object sender, EventArgs evnt)
        {
            lblH.BackColor = Color.Teal;
        }

        private void lblH_MouseLeave(object sender, EventArgs evnt)
        {
            lblH.BackColor = Color.Turquoise;
        }

        private void lblI_MouseEnter(object sender, EventArgs evnt)
        {
            lblI.BackColor = Color.Teal;
        }

        private void lblI_MouseLeave(object sender, EventArgs evnt)
        {
            lblI.BackColor = Color.Turquoise;
        }

        private void lblJ_MouseEnter(object sender, EventArgs evnt)
        {
            lblJ.BackColor = Color.Teal;
        }

        private void lblJ_MouseLeave(object sender, EventArgs evnt)
        {
            lblJ.BackColor = Color.Turquoise;
        }

        private void lblK_MouseEnter(object sender, EventArgs evnt)
        {
            lblK.BackColor = Color.Teal;
        }

        private void lblK_MouseLeave(object sender, EventArgs evnt)
        {
            lblK.BackColor = Color.Turquoise;
        }

        private void lblL_MouseEnter(object sender, EventArgs evnt)
        {
            lblL.BackColor = Color.Teal;
        }

        private void lblL_MouseLeave(object sender, EventArgs evnt)
        {
            lblL.BackColor = Color.Turquoise;
        }

        private void lblM_MouseEnter(object sender, EventArgs evnt)
        {
            lblM.BackColor = Color.Teal;
        }

        private void lblM_MouseLeave(object sender, EventArgs evnt)
        {
            lblM.BackColor = Color.Turquoise;
        }

        private void lblN_MouseEnter(object sender, EventArgs evnt)
        {
            lblN.BackColor = Color.Teal;
        }

        private void lblN_MouseLeave(object sender, EventArgs evnt)
        {
            lblN.BackColor = Color.Turquoise;
        }

        private void lblO_MouseEnter(object sender, EventArgs evnt)
        {
            lblO.BackColor = Color.Teal;
        }

        private void lblO_MouseLeave(object sender, EventArgs evnt)
        {
            lblO.BackColor = Color.Turquoise;
        }

        private void lblP_MouseEnter(object sender, EventArgs evnt)
        {
            lblP.BackColor = Color.Teal;
        }

        private void lblP_MouseLeave(object sender, EventArgs evnt)
        {
            lblP.BackColor = Color.Turquoise;
        }

        private void lblQ_MouseEnter(object sender, EventArgs evnt)
        {
            lblQ.BackColor = Color.Teal;
        }

        private void lblQ_MouseLeave(object sender, EventArgs evnt)
        {
            lblQ.BackColor = Color.Turquoise;
        }

        private void lblR_MouseEnter(object sender, EventArgs evnt)
        {
            lblR.BackColor = Color.Teal;
        }

        private void lblR_MouseLeave(object sender, EventArgs evnt)
        {
            lblR.BackColor = Color.Turquoise;
        }

        private void lblS_MouseEnter(object sender, EventArgs evnt)
        {
            lblS.BackColor = Color.Teal;
        }

        private void lblS_MouseLeave(object sender, EventArgs evnt)
        {
            lblS.BackColor = Color.Turquoise;
        }            

        private void lblT_MouseEnter(object sender, EventArgs evnt)
        {
            lblT.BackColor = Color.Teal;
        }

        private void lblT_MouseLeave(object sender, EventArgs evnt)
        {
            lblT.BackColor = Color.Turquoise;
        }

        private void lblU_MouseEnter(object sender, EventArgs evnt)
        {
            lblU.BackColor = Color.Teal;
        }

        private void lblU_MouseLeave(object sender, EventArgs evnt)
        {
            lblU.BackColor = Color.Turquoise;
        }

        private void lblV_MouseEnter(object sender, EventArgs evnt)
        {
            lblV.BackColor = Color.Teal;
        }

        private void lblV_MouseLeave(object sender, EventArgs evnt)
        {
            lblV.BackColor = Color.Turquoise;
        }

        private void lblW_MouseEnter(object sender, EventArgs evnt)
        {
            lblW.BackColor = Color.Teal;
        }

        private void lblW_MouseLeave(object sender, EventArgs evnt)
        {
            lblW.BackColor = Color.Turquoise;
        }

        private void lblX_MouseEnter(object sender, EventArgs evnt)
        {
            lblX.BackColor = Color.Teal;
        }

        private void lblX_MouseLeave(object sender, EventArgs evnt)
        {
            lblX.BackColor = Color.Turquoise;
        }

        private void lblY_MouseEnter(object sender, EventArgs evnt)
        {
            lblY.BackColor = Color.Teal;
        }

        private void lblY_MouseLeave(object sender, EventArgs evnt)
        {
            lblY.BackColor = Color.Turquoise;
        }

        private void lblZ_MouseEnter(object sender, EventArgs evnt)
        {
            lblZ.BackColor = Color.Teal;
        }

        private void lblZ_MouseLeave(object sender, EventArgs evnt)
        {
            lblZ.BackColor = Color.Turquoise;
        }

        private void lblGuessWord_MouseEnter(object sender, EventArgs e)
        {
            lblGuessWord.BackColor = Color.Teal;
        }

        private void lblGuessWord_MouseLeave(object sender, EventArgs e)
        {
            lblGuessWord.BackColor = Color.Turquoise;
        }

        private void txtWordGuess_MouseEnter(object sender, EventArgs e)
        {
            txtWordGuess.BackColor = Color.Turquoise;
        }

        private void txtWordGuess_MouseLeave(object sender, EventArgs e)
        {
            txtWordGuess.BackColor = Color.Teal;
        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Menu Menu = new Menu();//Load the Menu form
            Menu.Show();//Show the Menu form
            this.Visible = false;//Hide the Game form
        }

        /*****************
        *End hover events*
        *****************/
    }
}