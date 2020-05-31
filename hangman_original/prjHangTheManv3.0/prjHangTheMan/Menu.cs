using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prjHangTheMan
{
    public partial class Menu : Form
    {
        SoundPlayer player = new SoundPlayer();//Initialize the music player

        /***********************
        **Variables Start Here**
        ***********************/

        //use public static so it doesn't reset when changing forms
        public static int[] highScore = new int[6];
        public static Boolean loadOnce = true;//Only read the scores once, we don't want to overwrite them.
        DialogResult dr;//Store the result of a dialog box

        /*********************
        **Variables End Here**
        *********************/


        /*********************
        *Functions Start Here*
        *********************/
        void readScores()
        {
            int x = 0;
            string line;
            if (File.Exists("scores.txt") == true)//Check if there are any highscores saved. If there are, load them
            {
                // Read the file line by line, putting each value into the highScore array
                StreamReader file = new StreamReader("scores.txt");
                while ((line = file.ReadLine()) != null)
                {
                    highScore[x] = Convert.ToInt32(line);
                    x++;
                }
                file.Close();
            }
        }

        /******************************************
        *This function makes use of bubble sorting*
        ******************************************/
        void checkLastScore()
        {
            //sets the score from the other form to highScore[5]
            //this makes bubble sorting the scores much easier
            highScore[5] = Game.score;
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    //checks if highScore is less than the highScore above it in the array
                    if (highScore[y] < highScore[y + 1])
                    {
                        //highscores switch places
                        int temp = 0;
                        temp = highScore[y];
                        highScore[y] = highScore[y + 1];
                        highScore[y + 1] = temp;

                    }
                }
            }
            //sets the highscores to the labels
            lblHighscore1.Text = Convert.ToString(highScore[0]);
            lblHighscore2.Text = Convert.ToString(highScore[1]);
            lblHighscore3.Text = Convert.ToString(highScore[2]);
            lblHighscore4.Text = Convert.ToString(highScore[3]);
            lblHighscore5.Text = Convert.ToString(highScore[4]);
        }

        void saveScores()
        {
            //Output the scores to a text file.
            StreamWriter file = new StreamWriter("scores.txt");
            for (int x = 0; x < highScore.Length; x++)
            {
                file.WriteLine(highScore[x]);                
            }
            file.Close();
        }

        void clearScores()
        {           
            //Set all of the labels to read 0
            lblHighscore1.Text = "0";
            lblHighscore2.Text = "0";
            lblHighscore3.Text = "0";
            lblHighscore4.Text = "0";
            lblHighscore5.Text = "0";
            StreamWriter file = new StreamWriter("scores.txt");//Write 0s to the scores.txt file
            for (int x = 0; x < highScore.Length; x++)
            {
                file.WriteLine(0);
            }
            file.Close();            
        }

        void toggleMusic()
        {
            if (Game.music == true)
            {
                pbMusic.Image = Image.FromFile("Images/musicoff.png");
                Game.music = false;
                player.Stop();//Stop the music
            }
            else
            {
                pbMusic.Image = Image.FromFile("Images/musicon.png");
                Game.music = true;
                player.PlayLooping();//Play the music
            }
        }

        /*********************
        **Functions End Here**
        *********************/


        public Menu()
        {
            InitializeComponent();
            player.SoundLocation = "Audio/Bob-ombBattlefield.wav";//Tell the music player what file we want to play
        }

        private void Menu_Load(object sender, EventArgs e)//Code when the form loads
        {
            if (loadOnce == true)//Only read the scores once, we don't want to overwrite them.
            {
                readScores();
                loadOnce = false;//Make sure we don't overwrite the scores
            }

            checkLastScore();//Check the score from the previous game

            if (Game.music == true)//Check if the music was already playing
            {
                pbMusic.Image = Image.FromFile("Images/musicon.png");
            }
            else
            {
                pbMusic.Image = Image.FromFile("Images/musicoff.png");
            }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)//Code when the form closes
        {
            saveScores();//Save the current highscores list
            Application.Exit();//Close the program   
        }

        /************************************************************************
        *The following events take place when the user clicks one of the buttons*
        ************************************************************************/

        private void lblCreators_Click(object sender, EventArgs e)//The "Creators" button
        {
            MessageBox.Show("Created By: Kevin, Owen, and Jatin\nJune 1st, 2017");//Show the creators of the game            
        }

        private void pbMusic_Click(object sender, EventArgs e)//The music button
        {
            toggleMusic();//Toggle the music on or off
        }

        private void lblClearScores_Click(object sender, EventArgs e)//The "Clear Scores" button
        {
            dr = MessageBox.Show("Are you sure?", "Clear Scores", MessageBoxButtons.YesNo);//Show a mesage to warn the user of the severity of their actions.
            if (dr == DialogResult.Yes)
            {
                clearScores();//If they chose "Yes", clear the scores and reset them.
                readScores();
            }
        }

        private void lblScoringInfo_Click(object sender, EventArgs e)//The "Scoring Info" button
        {
            MessageBox.Show("Score gained per letter:  5\nScore gained when completed a word:  50\nScore gained when solved with text per letter:  50\n\nIf you guess wrong more than six times, our poor stickman volunteer is a goner!*\nPlease note that if you guess the word by typing but guess wrong, its game over!\n\n\n*No stickmen were harmed in the making of this game.");
            //Display information about scoring
        }

        private void lblWebsite_Click(object sender, EventArgs e)//The "Visit Website" button
        {
            Process.Start("http://the404.net16.net/software/hangman/index.html");//Opens our website
        }

        private void lblExit_Click(object sender, EventArgs e)//The "Exit" button
        {
            saveScores();
            Application.Exit();//Close the program
        }

        private void lblPlay_Click(object sender, EventArgs e)//The "Click to Play" button
        {
            //Start the game
            Game Game = new Game();//Load the Game form
            Game.Show();//Show the Game form
            this.Visible = false;//hide the Menu form
        }

        /*****************
        *End click events*
        *****************/

        /************************************************************************
        *The following events change the colour of the buttons when hovered over*
        ************************************************************************/

        private void lblPlay_MouseEnter(object sender, EventArgs e)
        {
            lblPlay.BackColor = Color.Gray;            
        }

        private void lblPlay_MouseLeave(object sender, EventArgs e)
        {
            lblPlay.BackColor = Color.FromArgb(42, 49, 50);
        }

        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.BackColor = Color.Gray;
        }

        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.BackColor = Color.FromArgb(42, 49, 50);
        }

        private void lblWebsite_MouseEnter(object sender, EventArgs e)
        {
            lblWebsite.BackColor = Color.Gray;
        }

        private void lblWebsite_MouseLeave(object sender, EventArgs e)
        {
            lblWebsite.BackColor = Color.FromArgb(42, 49, 50);
        }
        
        private void lblScoringInfo_MouseEnter(object sender, EventArgs e)
        {
            lblScoringInfo.BackColor = Color.Gray;
        }

        private void lblScoringInfo_MouseLeave(object sender, EventArgs e)
        {
            lblScoringInfo.BackColor = Color.FromArgb(42, 49, 50);
        }

        private void lblClearScores_MouseEnter(object sender, EventArgs e)
        {
            lblClearScores.BackColor = Color.Gray;
        }

        private void lblClearScores_MouseLeave(object sender, EventArgs e)
        {
            lblClearScores.BackColor = Color.FromArgb(42, 49, 50);
        }

        private void lblCreators_MouseLeave(object sender, EventArgs e)
        {
            lblCreators.BackColor = Color.FromArgb(51, 107, 135);
        }

        private void lblCreators_MouseEnter(object sender, EventArgs e)
        {
            lblCreators.BackColor = Color.DeepSkyBlue;
        }
        /*********************
        *End mouseover events*
        *********************/
    }
}