﻿#pragma checksum "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "67D636E3A1EC5A2CB6999F2564FE9213"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using DevExpress.Core;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.DataSources;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Core.ServerMode;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.DataPager;
using DevExpress.Xpf.Editors.DateNavigator;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Flyout;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Popups.Calendar;
using DevExpress.Xpf.Editors.RangeControl;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Settings.Extension;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Scheduler;
using DevExpress.Xpf.Scheduler.Commands;
using DevExpress.Xpf.Scheduler.Reporting;
using DevExpress.Xpf.Scheduler.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Hotel.Shared.Windows {
    
    
    /// <summary>
    /// SystemParameterWindow
    /// </summary>
    public partial class SystemParameterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSave;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnClose;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit txtName;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit txtAddress;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit txtDescription;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit chTime;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.DateEdit txtCheckInTime1;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.DateEdit txtCheckInTime2;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.DateEdit txtCheckOutTime1;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.DateEdit txtCheckOutTime2;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.CheckEdit chPet;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit txtPet;
        
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
            System.Uri resourceLocater = new System.Uri("/Hotel;component/shared/windows/systemparameterwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
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
            
            #line 6 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            ((Hotel.Shared.Windows.SystemParameterWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 6 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            ((Hotel.Shared.Windows.SystemParameterWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnSave = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.btnSave.Click += new System.Windows.RoutedEventHandler(this.btnSave_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnClose = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.btnClose.Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtName = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 5:
            this.txtAddress = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 6:
            this.txtDescription = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 7:
            this.chTime = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 33 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.chTime.Checked += new System.Windows.RoutedEventHandler(this.CheckEdit_Checked);
            
            #line default
            #line hidden
            
            #line 33 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.chTime.Unchecked += new System.Windows.RoutedEventHandler(this.CheckEdit_Unchecked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.txtCheckInTime1 = ((DevExpress.Xpf.Editors.DateEdit)(target));
            
            #line 37 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.txtCheckInTime1.EditValueChanged += new DevExpress.Xpf.Editors.EditValueChangedEventHandler(this.txtCheckInTime1_EditValueChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.txtCheckInTime2 = ((DevExpress.Xpf.Editors.DateEdit)(target));
            return;
            case 10:
            this.txtCheckOutTime1 = ((DevExpress.Xpf.Editors.DateEdit)(target));
            return;
            case 11:
            this.txtCheckOutTime2 = ((DevExpress.Xpf.Editors.DateEdit)(target));
            return;
            case 12:
            this.chPet = ((DevExpress.Xpf.Editors.CheckEdit)(target));
            
            #line 63 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.chPet.Checked += new System.Windows.RoutedEventHandler(this.CheckEdit_Checked_1);
            
            #line default
            #line hidden
            
            #line 63 "..\..\..\..\Shared\Windows\SystemParameterWindow.xaml"
            this.chPet.Unchecked += new System.Windows.RoutedEventHandler(this.CheckEdit_Unchecked_1);
            
            #line default
            #line hidden
            return;
            case 13:
            this.txtPet = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

