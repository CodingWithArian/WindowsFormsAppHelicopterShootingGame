using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace WindowsFormsAppHelicopterShootingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //songplayer.SoundLocation = "E:\\flash cut\\07-Solito y Sin Ti (Tribal)";
        }
        bool goup, godown, shot, gameover;
        int score = 0;
        int speed = 8;
        int ufoSpeed = 10;
        Random rand = new Random();
        int playerSpeed = 7;
        int index = 0;

        private void maintimerevents(object sender, EventArgs e)
        {
            textScore.Text = "Score: " + score;
            if(goup==true && player.Top>0)
            {
                player.Top -= playerSpeed;
            }
            if(godown==true && player.Top + player.Height<this.ClientSize.Height)
            {
                player.Top += playerSpeed;
            }

            ufo.Left -= ufoSpeed;
            if(ufo.Left + ufo.Width<0)
            {
                changeUfo();
            }
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && (string)x.Tag=="pillar")
                {
                    x.Left -= speed;

                    if(x.Left<-200)
                    {
                        x.Left = 1000;
                    }
                    if(player.Bounds.IntersectsWith(x.Bounds))
                    {
                        GameOver();
                    }
                }
                if(x is PictureBox && (string)x.Tag=="bullet")
                {
                    x.Left += 25;
                    if(x.Left>850)
                    {
                        removeBullets(((PictureBox)x));
                    }
                    if(ufo.Bounds.IntersectsWith(x.Bounds))
                    {
                        removeBullets(((PictureBox)x));
                        score++;
                        changeUfo();
                    }
                }
            }
            if(player.Bounds.IntersectsWith(ufo.Bounds))
            {
                GameOver();
            }
            if(score>10)
            {
                speed = 12;ufoSpeed = 18;
            }
        }

        private void ufo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up)
            {
                goup = true;
            }
            if(e.KeyCode==Keys.Down)
            {
                godown = true;
            }
            if (e.KeyCode == Keys.Space && shot == false)
            {
                makeBullet();
                shot = true;
            }
        }

        private void ufo_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Up)
            {
                goup = false;
            }
            if(e.KeyCode==Keys.Down)
            {
                godown = false;
            }
            if(shot==true)
            {
                shot = false;
            }
            if(e.KeyCode==Keys.Enter && gameover==true)
            {
                restartGame();
            }
        }
        private void restartGame()
        {
            goup = false;
            godown = false;
            shot = false;
            gameover = false;
            score = 0;
            speed = 8;
            ufoSpeed = 10;
            textScore.Text = "Score: " + score;
            changeUfo();
            player.Top = 81;
            pillar1.Left = 511;
            pillar2.Left = 329;
            GameTimer.Start();


        }
        private void GameOver()
        {
            GameTimer.Stop();
            textScore.Text = "Score: " + score + " Game Over,Press enter to retry!";
            gameover = true;
        }







      

        private void removeBullets(PictureBox bullets)
        {
            this.Controls.Remove(bullets);
            bullets.Dispose();
        }
        private void makeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.BackColor = Color.Maroon;
            bullet.Height = 5;
            bullet.Width = 10;
            bullet.Left = player.Left + player.Width;
            bullet.Top = player.Top + player.Height / 2;
            bullet.Tag = "bullet";
            this.Controls.Add(bullet);
        }
        private void changeUfo()
        {
            if(index>3)
            {
                index = 1;
            }
            else
            {
                index += 1;
            }
            switch(index)
            {
                case 1:
                    ufo.Image = Properties.Resources.alien1;
                    break;
                case 2:
                    ufo.Image = Properties.Resources.alien2;
                    break;
                case 3:
                    ufo.Image = Properties.Resources.alien3;
                    break;
            }
            ufo.Left = 1000;
            ufo.Top = rand.Next(20, this.ClientSize.Height - ufo.Height);
        }
    }
}
