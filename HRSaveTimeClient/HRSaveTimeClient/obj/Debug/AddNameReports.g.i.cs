﻿#pragma checksum "..\..\AddNameReports.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E5FBBE5C3C8019A722049365833FECAF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace HRSaveTimeClient {
    
    
    /// <summary>
    /// AddNameReports
    /// </summary>
    public partial class AddNameReports : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\AddNameReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid SaveReportsButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\AddNameReports.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CencelReportsButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/HRSaveTimeClient;component/addnamereports.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddNameReports.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 4 "..\..\AddNameReports.xaml"
            ((HRSaveTimeClient.AddNameReports)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SaveReportsButton = ((System.Windows.Controls.Grid)(target));
            
            #line 10 "..\..\AddNameReports.xaml"
            this.SaveReportsButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.SaveReportsButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 10 "..\..\AddNameReports.xaml"
            this.SaveReportsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.SaveReportsButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 10 "..\..\AddNameReports.xaml"
            this.SaveReportsButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.SaveReportsButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CencelReportsButton = ((System.Windows.Controls.Grid)(target));
            
            #line 14 "..\..\AddNameReports.xaml"
            this.CencelReportsButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CencelReportsButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 14 "..\..\AddNameReports.xaml"
            this.CencelReportsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CencelReportsButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 14 "..\..\AddNameReports.xaml"
            this.CencelReportsButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.CencelReportsButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

