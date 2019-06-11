﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Monopoly
{
    public partial class UI : Form
    {

        private Game game;
        private List<Button> players;
        private List<Bitmap> profilePics;

        public UI()
        {
            game = new Game();
            players = new List<Button>();
            profilePics = new List<Bitmap>();

            game.player = 0;
            
            InitializeComponent();
            
            AddGameSquares();
            AddPlayers();
            this.Size = new Size(1050, 750);
        }

        private void AddPlayers()
        {
            players.Add(new Button());
            players[0].Size = new Size(12, 12);
            players[0].BackColor = Color.Blue;
            players[0].Location = GetBoardLocation(game.Players[0].Position, game.Gameboard.BoardSideLength, 12, 28);
            profilePics.Add(Properties.Resources.playing_tokensP1);
            
            players.Add(new Button());
            players[1].Size = new Size(12, 12);
            players[1].BackColor = Color.Red;
            players[1].Location = GetBoardLocation(game.Players[0].Position, game.Gameboard.BoardSideLength, 24, 28);
            profilePics.Add(Properties.Resources.playing_tokensP2);

            this.Controls.Add(players[0]);
            players[0].BringToFront();

            this.Controls.Add(players[1]);
            players[1].BringToFront();


        }

        private void AddGameSquares()
        {
          foreach(GameSquare square in game.Gameboard.Squares)
            {
                Button property = new Button();
                property.Location = GetBoardLocation(square.SquareId, game.Gameboard.BoardSideLength, 0, 28);
                property.ForeColor = Color.FromArgb(square.Color.R, square.Color.G, square.Color.B);
                property.Size = new Size(50, 50);
                if (square is RealEstate)
                {
                    property.Text = (square as RealEstate).Name + "\n" + (square as RealEstate).Price;
                }
                else
                {
                    property.Text = square.Name;
                }
                property.Click += (o, d) => { MessageBox.Show(square.Name + "\n" + square.Info); };
                this.Controls.Add(property);

                Label level = new Label();
                level.Size = new Size(12, 12);
                level.Location = GetBoardLocation(square.SquareId, game.Gameboard.BoardSideLength, 0, 28);
                level.Text = "5";
                level.BackColor = Color.Transparent;
                this.Controls.Add(level);
                level.BringToFront();
            }  
        }
        /// <summary>
        /// 
        /// contains a array of size 2 wich contains a board size and length
        /// </summary>
        /// <param name="orderNumber"></param>
        /// <param name="boardSize"></param>
        /// <returns></returns>
        private Point GetBoardLocation(int orderNumber, int boardSideLength)
        {
            Point loc = new Point();
            switch (orderNumber / boardSideLength)
            {
                case (3):
                   loc.X = 0;
                    loc.Y = ((boardSideLength * 50) + 12) - (orderNumber - (boardSideLength * 3)) * 50;
                    break;
                case (2):
                    loc.X = (boardSideLength * 50) - (orderNumber - (boardSideLength * 2)) * 50;
                    loc.Y = ((boardSideLength * 50) + 12);
                    break;
                case (1):
                    loc.X = 50 * boardSideLength;
                    loc.Y = 12 + ((orderNumber - boardSideLength) * 50);
                    break;
                case (0):
                    loc.X = (orderNumber * 50); 
                    loc.Y = 12;
                    break;
                default:
                    break;
            }
            return loc;
        }

        private Point GetBoardLocation(int orderNumber, int boardSideLength, int offsetX, int offsetY)
        {
            Point loc = new Point();
            switch (orderNumber / boardSideLength)
            {
                case (3):
                    loc.X = 0 + offsetX;
                    loc.Y = ((boardSideLength * 50) + 12) - (orderNumber - (boardSideLength * 3)) * 50 + offsetY;
                    break;
                case (2):
                    loc.X = (boardSideLength * 50) - (orderNumber - (boardSideLength * 2)) * 50 + offsetX;
                    loc.Y = ((boardSideLength * 50) + 12) + offsetY;
                    break;
                case (1):
                    loc.X = 50 * boardSideLength + offsetX;
                    loc.Y = 12 + ((orderNumber - boardSideLength) * 50) + offsetY;
                    break;
                case (0):
                    loc.X = (orderNumber * 50) + offsetX;
                    loc.Y = 12 + offsetY;
                    break;
                default:
                    break;
            }
            return loc;
        }
        
        private void btnRollP1_Click(object sender, EventArgs e)
        {
            Roll();
            GameSquare square = game.GetGameSquare(game.Players[game.player].Position);
            Buyable(square, game.player);

            panel1.BackColor = Color.FromArgb(square.Color.R, square.Color.G, square.Color.B);
            lblNamePropertyP1.Text = square.Name;
            if (square is RealEstate)
            {
                lblRentP1.Text =  (square as RealEstate).Price.ToString("C", CultureInfo.CurrentCulture);
            }
            else
            {
                lblRentP1.Text = "";
            }
        }

        private void Buyable(GameSquare square, int player)
        {
            if (square is RealEstate)
            {
                btnBuyP1.Enabled = true;
                btnP1Upgrade.Enabled = false;

                if (square is Street)
                {
                    foreach (RealEstate estate in game.Players[player].RealEstates)
                    {
                        if (square == estate)
                        {
                            btnP1Upgrade.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                btnBuyP1.Enabled = false;
                btnP1Upgrade.Enabled = false;
            }
        }

        private void Roll()
        {
            game.Roll(game.player);


            players[game.player].Location = GetBoardLocation(game.Players[game.player].Position, game.Gameboard.BoardSideLength, 12 + game.player * 12, 28);
            btnP1profile.Image = profilePics[game.player];
            btnP1profile.BackgroundImageLayout = ImageLayout.Stretch;
            lblCash.Text = "Cash: " + Convert.ToString(game.Players[game.player].Cash);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            game.fileHandler.Save();
        }

        private void tsLoad_Click(object sender, EventArgs e)
        {
            game.fileHandler.Load();
        }

        private void btnMortage_Click(object sender, EventArgs e)
        {

        }

        private void btnBuyP1_Click(object sender, EventArgs e)
        {
            if (game.Buy())
            {
                //lbP1Properties.Items.Add(game.GetGameSquare(players[game.player].))
            }
        }
    }
}
