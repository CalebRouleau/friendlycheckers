﻿#pragma checksum "C:\Users\HP\Documents\GitHub\networkless\friendlycheckers\client\FriendlyCheckers\SettingsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CF80E9B047104DE6D77B1025A0DBCA05"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace FriendlyCheckers {
    
    
    public partial class SettingsPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRootSettings;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.StackPanel OptionsPanel;
        
        internal System.Windows.Controls.TextBlock GameOptions;
        
        internal System.Windows.Controls.CheckBox Op_ForceJump;
        
        internal System.Windows.Controls.TextBlock Difficulty;
        
        internal System.Windows.Controls.CheckBox Op_DiffHard;
        
        internal System.Windows.Controls.CheckBox Op_DiffEasy;
        
        internal System.Windows.Controls.TextBlock PageTitleSettings;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/FriendlyCheckers;component/SettingsPage.xaml", System.UriKind.Relative));
            this.LayoutRootSettings = ((System.Windows.Controls.Grid)(this.FindName("LayoutRootSettings")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.OptionsPanel = ((System.Windows.Controls.StackPanel)(this.FindName("OptionsPanel")));
            this.GameOptions = ((System.Windows.Controls.TextBlock)(this.FindName("GameOptions")));
            this.Op_ForceJump = ((System.Windows.Controls.CheckBox)(this.FindName("Op_ForceJump")));
            this.Difficulty = ((System.Windows.Controls.TextBlock)(this.FindName("Difficulty")));
            this.Op_DiffHard = ((System.Windows.Controls.CheckBox)(this.FindName("Op_DiffHard")));
            this.Op_DiffEasy = ((System.Windows.Controls.CheckBox)(this.FindName("Op_DiffEasy")));
            this.PageTitleSettings = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitleSettings")));
        }
    }
}

