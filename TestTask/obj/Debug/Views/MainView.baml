   M S B A M L   `   `   `  ????O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      C ?TestTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:TestTaskTestTask 5	      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5
         TestTask.MainView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      aPresentationOptionsAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/options     5      Bcol2clr-namespace:System.Collections;assembly=mscorlib     5      (localclr-namespace:TestTask     5	      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5
      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      $
??1000??5      $	??500??5        ? 
ResizeMode$  NoResize=?5       ? Title$ Chart??5       ? 	Resources 5       ?? MergedDictionaries ?? Source?? 5   
    5      ?? 5      $# /Resources/Styles/Button.xaml??6%   ?? 5      $% /Resources/Styles/CheckBox.xaml??6%   ?? 5      $% /Resources/Styles/ComboBox.xaml??6%   %    5       ? DataContext 5        TestTask.MainViewModel  5   
    J? InputBindings 5       ?? 5!   
   -   5        SpecialKeyCopy?? ?? 	Modifiers$
 Ctrlf?5!   -   
 ?? Key$ C??6>   	 ?? Command	 6F   ?? *CopyCommand+?? 5"   
   -   5        SpecialKeyPaste??$
 Ctrlf?5"   .   $ V??6?   	 6G   ?? *PasteCommand+?? 5#   
   -   5        SpecialKeySave??$
 Ctrlf?5#   -   $ S??6>   	 6F   ?? *SaveCommand+?? 5$   
   -   5        SpecialKeySaveAs??$ 
Ctrl+Shiftf?5$   /   $ S??6F   	 6N   ?? *SaveAsCommand+.??5'      ?    WindowBackgroundBrush#??[  5(   	   
 ? ColumnDefinitions
 5)   
   ?? 5*      $	??10* ?5+      ?? 5,      $
??110* ?5-      ?? 5.      $	??10* ?5/      ?? 50      $
??620* ?51      ?? 52      $	??10* ?53      ?? 54      $
??115* ?55      ?? 56      $
??115* ?57      ?? 58      $	??10* ?59       ? RowDefinitions 5<   
   ?? 5=      $	??20* ?5>      ?? 5?      $	??10* ?5@      ?? 5A      $	??35* ?5B      ?? 5C      $
??300* ?5D      ?? 5E      $	??10* ?5F      ?? 5G      $	??40* ?5H      ?? 5I      $	??20* ?5J      ?? 5K      $	??45* ?5L      ?? 5M      $	??10* ?5N      .+?5Q   
   o? $??0??5R      $??0??5S      $??8??5T        BackgroundBrush#??[ 5U        ForegroundBrush#??[ 5V      .@?5W      m? ??5X      ?? *	FileTitle+.@?5Y      m?   ForegroundBlackBrush#??[ 5[      ??5Z      ?? *	OpenTitle+ m? Command 5\      ?? *OpenCommand+m? 5]      #??[ 5_      ??5^      ?? *	SaveTitle+ 5`      ?? *SaveCommand+m? 5a      #??[ 5c      ??5b      ?? *SaveAsTitle+ 5d      ?? *SaveAsCommand+m? 5e      #??[ 5g      ??5f      ?? *
CloseTitle+ 5h      ?? *CloseCommand+5i      m? 5j      ??5k      ?? *	ExitTitle+ 5l      ?? *ExitCommand+5m      ?? 5o   
   $??2??5p      $??6??5q      $??3??5r       ?? CornerRadius$ 5??5s        
WhiteBrush#??[ 5t       J? SnapsToDevicePixels . ._?5v      #  LiveCharts.Wpf.CartesianChart  -   5          Name$ Chart??5w      &   LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Right=?5x         Series 5y      ?? *Chart+   AxisX 5{        LiveCharts.Wpf.Axis     Title 5|      ?? *TemperatureTitle+   AxisY 5         5?      ?? *AbsoluteMarkTitle+5?      5?      ?? 5?   
   -   5         ??Name$ InverseChartCheckBox??5?      $??2??5?      $??1??5?        CustomCheckBoxStyle#??[ 5?      ??5?      ?? *InverseTitle+ d? 	IsChecked 5?      ?? *IsInverseChartCheckBoxChecked+&   System.Windows.Controls.DataGrid  5?   
   -   5          Name$ ChartDataGrid??5?      $??2??5?      $??2??5?      $??5??5?      $??2??5?         FrozenColumnCount$ 2??5?         MinColumnWidth$	 115Q?5?      ??5?      ?? *FunctionValues+ ?? SelectedItem 5?      ?? *SelectedNode+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5?      +  %Microsoft.Xaml.Behaviors.EventTrigger  5?         	EventName$ RowEditEnding??5?      *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5?      2	  ,Microsoft.Xaml.Behaviors.InvokeCommandAction	   	  Command 5?      ?? *RowEditEndingCommand+5?      5?      ?? 5?   
   -   5         ??Name$ 	buttonAdd??5?      $??5??5?      $??5??5?      $??2??5?        CustomButtonStyle#??[ 5?      ??5?      ?? *
AddTitle+??5?      ?? *ButtonAddCommand+?? 5?   
   -	   5          ??Name$  comboBoxFiles??5?      $??7??5?      $??5??5?      $??2??5?        CustomComboBoxStyle#??[ 5?      ??5?      ?? *Models+ 5?      ?? *SelectedModel+5?      5?      