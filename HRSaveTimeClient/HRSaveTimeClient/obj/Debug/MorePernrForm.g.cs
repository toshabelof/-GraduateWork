﻿#pragma checksum "..\..\MorePernrForm.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E4705554B74CDB6C89B814D3216C2DD43A8B7FB0"
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
    /// MorePernrForm
    /// </summary>
    public partial class MorePernrForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\MorePernrForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid OkMorePernrReportsButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\MorePernrForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CencelMorePernrReportsButton;
        
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
            System.Uri resourceLocater = new System.Uri("/HRSaveTimeClient;component/morepernrform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MorePernrForm.xaml"
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
            
            #line 6 "..\..\MorePernrForm.xaml"
            ((System.Windows.Controls.DataGrid)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.DataGrid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.OkMorePernrReportsButton = ((System.Windows.Controls.Grid)(target));
            
            #line 12 "..\..\MorePernrForm.xaml"
            this.OkMorePernrReportsButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.OkMorePernrReportsButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 12 "..\..\MorePernrForm.xaml"
            this.OkMorePernrReportsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.OkMorePernrReportsButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 12 "..\..\MorePernrForm.xaml"
            this.OkMorePernrReportsButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OkMorePernrReportsButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CencelMorePernrReportsButton = ((System.Windows.Controls.Grid)(target));
            
            #line 16 "..\..\MorePernrForm.xaml"
            this.CencelMorePernrReportsButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CencelMorePernrReportsButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MorePernrForm.xaml"
            this.CencelMorePernrReportsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CencelMorePernrReportsButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 16 "..\..\MorePernrForm.xaml"
            this.CencelMorePernrReportsButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.CencelMorePernrReportsButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

