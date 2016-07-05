﻿#pragma checksum "..\..\..\..\MasterData\Views\ItemGroupView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B57838A966CAF341283256A99F834186"
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
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;
using DevExpress.Xpf.Grid.TreeList;
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


namespace Hotel.MasterData.Views {
    
    
    /// <summary>
    /// ItemGroupView
    /// </summary>
    public partial class ItemGroupView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearch;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnReload;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrint;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnHome;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.GridControl dgItemGroup;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Grid.TableView viewItemGroup;
        
        #line default
        #line hidden
        
        
        #line 94 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DevExpress.Xpf.Editors.TextEdit txtCategoryName;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock blkTotal;
        
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
            System.Uri resourceLocater = new System.Uri("/Hotel;component/masterdata/views/itemgroupview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
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
            
            #line 9 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            ((Hotel.MasterData.Views.ItemGroupView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtSearch = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnSearch.Click += new System.Windows.RoutedEventHandler(this.btnSearch_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnReload = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnReload.Click += new System.Windows.RoutedEventHandler(this.btnReload_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnPrint = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnPrint.Click += new System.Windows.RoutedEventHandler(this.btnPrint_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnHome = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.btnHome.Click += new System.Windows.RoutedEventHandler(this.btnHome_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.dgItemGroup = ((DevExpress.Xpf.Grid.GridControl)(target));
            
            #line 67 "..\..\..\..\MasterData\Views\ItemGroupView.xaml"
            this.dgItemGroup.SelectedItemChanged += new DevExpress.Xpf.Grid.SelectedItemChangedEventHandler(this.dgItemGroup_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.viewItemGroup = ((DevExpress.Xpf.Grid.TableView)(target));
            return;
            case 12:
            this.txtCategoryName = ((DevExpress.Xpf.Editors.TextEdit)(target));
            return;
            case 13:
            this.blkTotal = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
