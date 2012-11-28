﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace FriendlyCheckers
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static Color DarkRed;
        public static Color DarkGrey;
        public static Color HighlightRed;
        public static Color HighlightGrey;
        public static Checker HIGHLIGHTED_PIECE;
        public static int checkerX, checkerY;
        private static Canvas mainCanvas;
        private GameLogic logic;
        private Rectangle[,] spaces;
        private Checker[,] pieces;
        private int row_W = 8;
        private int w = 400, h = 400;
        private int tapX, tapY;

        public enum GameType { OUT_OF_GAME, SINGLE_PLAYER, ONLINE_MULTI, LOCAL_MULTI };
        public static GameType game_type = GameType.OUT_OF_GAME;

        public MainPage()
        {
            InitializeComponent();
            tapX = checkerX = tapY = checkerY = -1;
            Color shade = new Color();
            shade.R = shade.G = shade.B = 0;
            shade.A = 150;
            Shader.Fill = new SolidColorBrush(shade);
            ContentPanel.Children.Remove(Shader);

            HighlightRed = new Color();
            HighlightRed.R = 255;
            HighlightRed.G = 100;
            HighlightRed.B = 100;
            HighlightRed.A = 255;

            HighlightGrey = new Color();
            HighlightGrey.R = 100;
            HighlightGrey.G = 100;
            HighlightGrey.B = 100;
            HighlightGrey.A = 255;

            DarkRed = new Color();
            DarkRed.R = 50;
            DarkRed.G = 0;
            DarkRed.B = 0;
            DarkRed.A = 255;

            DarkGrey = new Color();
            DarkGrey.R = DarkGrey.G = DarkGrey.B = 20;
            DarkGrey.A = 255;

            RemoveInGameStats();
            mainCanvas = new Canvas();
            mainCanvas.Width = w;
            mainCanvas.Height = h;
            ContentPanel.Children.Add(mainCanvas);
            createBoard();
            createPieces();
            scramble();
        }
        private void createPieces()
        {
            logic = new GameLogic(row_W, row_W);
            pieces = new Checker[8,8];
            int row = 0, col = 0;
            for (int k = 0; k < 24; k++)
            {
                row = (k / 4) + ((k>=12)? 2 : 0);
                col = 2*(k % 4) + (row%2==0?0:1);
                pieces[col,row] = new Checker(col, row, (k < 12) ? Colors.Red : DarkGrey,
                                                (k < 12) ? DarkRed : Colors.Black);
                Vector vect = new Vector(row, col);
                Piece piece = new Piece((k < 12) ? PieceColor.RED: PieceColor.BLACK,vect,PieceType.REGULAR);
                logic.addPiece(piece);
                mainCanvas.Children.Add(pieces[col,row].getEl2());
                mainCanvas.Children.Add(pieces[col,row].getEl1());
            }
        }
        private void createBoard()
        {
            Color Brown = new Color();
            Brown.R = 120;
            Brown.G = 40;
            Brown.B = 10;
            Brown.A = 255;

            Color Sand = new Color();
            Sand.R = 200;
            Sand.G = 180;
            Sand.B = 90;
            Sand.A = 255;

            spaces = new Rectangle[row_W, row_W];
            int size = 55;
            for (int k = 0; k < row_W; k++)
            {
                for (int i = 0; i < row_W; i++)
                {
                    spaces[k, i] = new Rectangle();
                    spaces[k, i].MinWidth = size;
                    spaces[k, i].MinHeight = size;
                    spaces[k, i].Width = size;
                    spaces[k, i].Height = size;
                    spaces[k, i].MouseLeftButtonUp += Action;
                    spaces[k, i].Fill = new SolidColorBrush(((i + k) % 2 == 0) ? Sand : Brown);
                    int lm = (k * size) - 20;
                    int tm = (i * size) - 115;
                    spaces[k, i].Margin = new Thickness(lm, tm, w - lm, h - tm);
                    mainCanvas.Children.Add(spaces[k, i]);
                }
            }
        }
        private void ClearMenu()
        {
            ContentPanel.Children.Remove(singleplayer);
            ContentPanel.Children.Remove(multiplayer_local);
            ContentPanel.Children.Remove(multiplayer_online);
            LayoutRoot.Children.Remove(TitlePanel);
        }
        private void RemoveInGameStats()
        {
            HiddenPanel.Children.Remove(Timer);
            HiddenPanel.Children.Remove(Moves);
            HiddenPanel.Children.Remove(Versus);
            ContentPanel.Children.Remove(quit);
            ContentPanel.Children.Remove(Shader);
            ContentPanel.Children.Remove(Search);
        }
        private void AddInGameStats()
        {
            HiddenPanel.Children.Add(Versus);
            HiddenPanel.Children.Add(Timer);
            HiddenPanel.Children.Add(Moves);
            ContentPanel.Children.Add(quit);
        }
        private void scramble()
        {
            for (int k = 0; k < 1000; k++)
            {
                Random rand = new Random();
                int row1 = rand.Next(0, row_W);
                int col1 = rand.Next(0, row_W);
                int row2 = rand.Next(0, row_W);
                int col2 = rand.Next(0, row_W);
                move(row1, col1, row2, col2);
            }
        }
        private bool validMove(int x1, int y1, int x2, int y2)
        {
            if (x1 > row_W || x2 > row_W || y1 > row_W || y2 > row_W) return false;
            if (pieces[x1, y1] == null) return false;
            if (pieces[x2, y2] != null) return false;

            return true;
        }
        private void move(int x1, int y1, int x2, int y2)
        {
            if (!validMove(x1, y1, x2, y2)) return;

            Checker temp = delete(x1, y1);
            pieces[x2, y2] = new Checker(x2, y2, temp.getColor(),temp.getBG());

            mainCanvas.Children.Add(pieces[x2, y2].getEl2());
            mainCanvas.Children.Add(pieces[x2, y2].getEl1());
        }
        private void SinglePlayer_Setup(object sender, RoutedEventArgs e)
        {
            ClearMenu();
            Versus.Text = "Player 1 vs. Computer";
            AddInGameStats();
            game_type = GameType.SINGLE_PLAYER;
            resetBoard();
        }
        private void Local_Multi_Setup(object sender, RoutedEventArgs e)
        {
            ClearMenu();
            Versus.Text = "Player 1 vs. Player 2";
            AddInGameStats();
            game_type = GameType.LOCAL_MULTI;
            resetBoard();
        }
        private void Online_Multi_Setup(object sender, RoutedEventArgs e)
        {
            ClearMenu();
            Versus.Text = "Player 1 vs. [Searching...]";
            AddInGameStats();
            game_type = GameType.ONLINE_MULTI;
            resetBoard();
            ContentPanel.Children.Add(Shader);
            ContentPanel.Children.Add(Search);
        }
        private void Menu_Setup(object sender, RoutedEventArgs e)
        {
            RemoveInGameStats();
            LayoutRoot.Children.Add(TitlePanel);
            ContentPanel.Children.Add(singleplayer);
            ContentPanel.Children.Add(multiplayer_local);
            ContentPanel.Children.Add(multiplayer_online);
            game_type = GameType.OUT_OF_GAME;
            scramble();
        }
        private void resetBoard()
        {
            for (int k = 0; k < row_W; k++)
            {
                for (int j = 0; j < row_W; j++)
                {
                    if (pieces[k, j] == null) continue;
                    delete(k, j);
                }
            }
            createPieces();
        }
        private void Action(object o, MouseButtonEventArgs e)
        {
            if (game_type == GameType.OUT_OF_GAME || checkerX==-1 || checkerY==-1) return;
            for (int k = 0; k < row_W; k++)
            {
                for (int i = 0; i < row_W; i++)
                {
                    if (spaces[k, i].GetHashCode().Equals(o.GetHashCode()))
                    {
                        Move m;
                        try
                        {
                            int locX = checkerX;
                            int locY = checkerY;

                            // Unhighlight the selected piece
                            handleHighlighting(HIGHLIGHTED_PIECE);

                            m = logic.makeMove(locY, locX, i, k);
                            handleMove(m);
                            //MessageBox.Show("checkerX: " + checkerX + "  checkerY: " + checkerY );
                        }
                        catch (PieceWrongColorException){ MessageBox.Show("You cannot move your opponent's pieces!"); }
                        catch (InvalidMoveException){MessageBox.Show("Invalid move.");}
                        catch (GameLogicException) {MessageBox.Show("A logic exception has occurred.");}
                        break;
                    }
                }
            }
            checkerX = checkerY = -1;
        }
        private void handleMove(Move move)
        {
            List<Piece> added = move.getAdditions();
            List<Piece> removed = move.getRemovals();

            foreach (Piece p in removed)
            {
                Vector co = p.getCoordinates();
                delete(co.getX(),co.getY());
            }

            foreach (Piece p in added)
            {
                Vector co = p.getCoordinates();

                int col = co.getX();
                int row = co.getY();
                Checker c = new Checker(col, row,p.getColor() == PieceColor.BLACK ? DarkGrey : Colors.Red,
                                                 p.getColor() == PieceColor.BLACK ? Colors.Black : DarkRed);
                pieces[col, row] = c;
                mainCanvas.Children.Add(pieces[col, row].getEl2());
                mainCanvas.Children.Add(pieces[col, row].getEl1());
            }
        }
        private Checker delete(int x, int y)
        {
            Checker temp = pieces[x, y];
            mainCanvas.Children.Remove(temp.getEl2());
            mainCanvas.Children.Remove(temp.getEl1());
            pieces[x, y] = null;

            return temp;
        }
        // The highlighted piece is also the selected piece.
        // Ie, checkerX = HIGHLIGHTED_PIECE.getX(), and checkerY = HIGHLIGHTED_PIECE.getY()
        public static void handleHighlighting(Checker c)
        {
            if(HIGHLIGHTED_PIECE!=null)
                HIGHLIGHTED_PIECE.toggleHighlight();

            //if the alrady highlighted piece is the same as the one being clicked
            if (HIGHLIGHTED_PIECE!=null && HIGHLIGHTED_PIECE.Equals(c)) 
            {
                checkerX = checkerY = -1;
                HIGHLIGHTED_PIECE = null;
                return;
            }
            else //otherwise, a piece is either being clicked for the first time or is switching highlights.
            {
                checkerX = c.getX();
                checkerY = c.getY();
                HIGHLIGHTED_PIECE = c;
                HIGHLIGHTED_PIECE.toggleHighlight();
            }
        }
        public GameType getGameType()
        {
            return game_type;
        }
    }
    public class Checker
    {
        private Ellipse el1, el2;
        private int offsetx = -111, offsety = 19, marginx = 55, marginy = 55, x, y;
        private Color color, bg, high;
        private bool col, lit;
        public Checker(int x, int y, Color color, Color bg)
        {
            el1 = new Ellipse();
            el2 = new Ellipse();
            this.x = x;
            this.y = y;
            this.lit = false;
            this.color = color;
            this.col = (color.Equals(MainPage.DarkGrey) ? false : true);
            this.bg = bg;
            this.high = !col ? MainPage.HighlightGrey : MainPage.HighlightRed;

            el1.Width = 50;
            el1.MinWidth = 50;
            el1.Height = 50;
            el1.MinHeight = 50;
            el1.Fill = new SolidColorBrush(color);
            el1.Margin = new Thickness(x * marginx - offsety, y * marginy - 2 + offsetx,
                400 - x * marginx + offsety, 400 - y * marginy + 2 - offsetx);
            el2.Width = 50;
            el2.MinWidth = 50;
            el2.Height = 50;
            el2.MinHeight = 50;
            el2.Margin = new Thickness(x * marginx - offsety + 2, y * marginy + offsetx,
                400 - x * marginx + offsety - 2, 400 - y * marginy - offsetx);
            el2.Fill = new SolidColorBrush(bg);
            el1.MouseLeftButtonUp += ellipse_MouseUp;
            el2.MouseLeftButtonUp += ellipse_MouseUp;
        }
        public Ellipse getEl1() { return el1; }
        public Ellipse getEl2() { return el2; }
        public int getX() { return this.x; }
        public int getY() { return this.y; }
        public Color getColor() { return this.color; }
        public Color getBG() { return this.bg; }
        public Boolean Equals(Checker c) { return (c.getX() == this.getX() && c.getY() == this.getY()); }
        public void toggleHighlight()
        {
            if (!lit)
                el1.Fill = new SolidColorBrush(high);
            else
                el1.Fill = new SolidColorBrush(color);
            lit = !lit;
        }
        private void ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MainPage.game_type == MainPage.GameType.OUT_OF_GAME) return;
            //toggleHighlight();
            MainPage.handleHighlighting(this);
        }
    }
}