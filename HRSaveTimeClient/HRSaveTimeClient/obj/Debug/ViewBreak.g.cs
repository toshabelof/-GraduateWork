﻿#pragma checksum "..\..\ViewBreak.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "78DBE83F329C50EA41434042FDC9ADF9"
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
    /// ViewBreak
    /// </summary>
    public partial class ViewBreak : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\ViewBreak.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid OkMoreBreakButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ViewBreak.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid CencelMoreBreakButton;
        
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
            System.Uri resourceLocater = new System.Uri("/HRSaveTimeClient;component/viewbreak.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ViewBreak.xaml"
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
            this.OkMoreBreakButton = ((System.Windows.Controls.Grid)(target));
            
            #line 15 "..\..\ViewBreak.xaml"
            this.OkMoreBreakButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.OkMoreBreakButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\ViewBreak.xaml"
            this.OkMoreBreakButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.OkMoreBreakButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 15 "..\..\ViewBreak.xaml"
            this.OkMoreBreakButton.PreviewMouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.OkMoreBreakButton_PreviewMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CencelMoreBreakButton = ((System.Windows.Controls.Grid)(target));
            
            #line 19 "..\..\ViewBreak.xaml"
            this.CencelMoreBreakButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.CencelMoreBreakButton_MouseEnter);
            
            #line default
            #line hidden
            
            #line 19 "..\..\ViewBreak.xaml"
            this.CencelMoreBreakButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.CencelMoreBreakButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 19 "..\..\ViewBreak.xaml"
            this.CencelMoreBreakButton.PreviewMouseRightButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.CencelMoreBreakButton_PreviewMouseRightButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

