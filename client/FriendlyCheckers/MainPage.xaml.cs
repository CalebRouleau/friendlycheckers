using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace FriendlyCheckers
{
    public enum GameState { OUT_OF_GAME, END_GAME, OPTIONS, ABOUT, MENU, SINGLE_PLAYER, LOCAL_MULTI, WAITING_FOR_COMPUTER };
    public partial class MainPage : PhoneApplicationPage
    {

        public static int w = 400, h = 400;
        public static Boolean FORCE_JUMP = false, DIFFICULT = false;
        public static GameState game_state = GameState.MENU;

        public MainPage()
        {
            InitializeComponent();
        }
        private void SinglePlayer_Setup(object sender, RoutedEventArgs e)
        {
            game_state = GameState.SINGLE_PLAYER;
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/GamePage.xaml", UriKind.Relative)); 
        }
        private void Local_Multi_Setup(object sender, RoutedEventArgs e)
        {
            game_state = GameState.LOCAL_MULTI;
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate( new Uri("/GamePage.xaml", UriKind.Relative));
        }
        private Boolean InLocalGame()
        {
            return (game_state == GameState.SINGLE_PLAYER || game_state==GameState.LOCAL_MULTI);
        }
        private Boolean MenuState()
        {
            return (game_state == GameState.MENU || game_state == GameState.ABOUT || game_state == GameState.OPTIONS);
        }
        private void Show_Options(object sender, RoutedEventArgs e)
        {
            game_state = GameState.OPTIONS;
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
        private void Show_About(object sender, RoutedEventArgs e)
        {
            game_state = GameState.ABOUT;
            (App.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
        public GameState getGameType()
        {
            return game_state;
        }
        
      
    }
}