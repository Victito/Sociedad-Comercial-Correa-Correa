   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5      O KSociedadCorreaCorrea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:SociedadCorreaCorrea.ViewsSociedadCorreaCorrea.Views 5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5
      1   +SociedadCorreaCorrea.Views.GraficosFacturas   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      :local(clr-namespace:SociedadCorreaCorrea.Views     5      Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5
      ;controls,http://metro.mahapps.com/winfx/xaml/controls  5      ^ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am	 iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag
 cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae  aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag! cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al" hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai# eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al$ hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai% eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah& dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai' eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj( fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag) cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af* bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj+ fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak, gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah- dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks&  	 
                        ! " # $ % & ' ( ) * + , - 5        � Title$  	Principal��5      $
��1300��5      $	��700��5       � WindowStyle$
 None=�5       � WindowStartupLocation$ CenterScreen=�5      (  "MahApps.Metro.Controls.MetroWindow   ShowTitleBar$ False��5         ShowCloseButton$ False��5         ShowMinButton$ False��5         ShowMaxRestoreButton$ False��5      
������  � AllowsTransparency$
 True��5      .��5      ��  �� CornerRadius$ 10��6   $��1q�5      ��5   
   �� 5      	 �� 
StartPoint$		 0,0&�6"   
 �� EndPoint$	
 1,1&�63   .��5      �  � Color$ #3393DF��6    � Offset$ 0Q�6/   � 5       $ #007AFF��6   $ 1Q�6/   5!      ._�5$   
   �  � ColumnDefinitions 5%      �� 5'      $	��250 �6#   �� 5)      $��* �6#   .+�5-      �� $ 	10,0,0,10��6   $��0��6.   ��5.      �� 5/      $		 0,0&�6*   $
 1,0.7&�6;   .��50      � $ #023E8A��6'   $ 0Q�67   � 51      $ #045DE9��6'   $ 1Q�67   52      ._�55      �� .+�57      ��  �� Orientation$ 
Horizontal=�6!   $��35��6:   $��
10,25,0,40q�6F   .+�58      �� $8��2/Resources/Logo_Final_cliente-removebg-preview.png��6    $��30��6\   �� 59      $��Farmacia��6$   
�������$��20(�5;   $   $��
Montserrat)�5<   $   $��Medium"�5=   $   $��Center=�5>   $   $��10,0,0,0q�5?   $   5@      	� 5C      -   5           
menuButton#��[  5C   "   
 � Tag$ #ffffff��5D   "   .��5F      �� $ 
Horizontal=�6%   .+�5G      .  (MahApps.Metro.IconPacks.PackIconMaterial     Kind$
 Home=�69     menuButtonIcon#��[ 6E   �� 5H      $��Inicio��6(     menuButtonText#��[ 66   5I      5J      	� 5K      -   5        #��[  5K   "   $ #ffffff��5L      .��5N      �� $ 
Horizontal=�6%   .+�5O        $ FileDocumentOutline=�69   #��[ 6T   �� 5P      $��Ingresar Facturas��6(   #��[ 6A   5Q      5R      	� 5S      -   5        #��[  5S   "   $ #ffffff��5T      .��5V      �� $ 
Horizontal=�6%   .+�5W        $ History=�69   #��[ 6H   �� 5X      $��Historial de Facturas��6(   #��[ 6E   5Y      5Z      	� 5[      -   5        #��[  5[   "   $ #ffffff��5\      .��5]      �� $ 
Horizontal=�6%   .+�5^        $ ChartPieOutline=�69   #��[ 6P   �� 5_      $��Dashboards de Facturas��6(   #��[ 6F   5`      5a      	� 5b      -   5        #��[  5b   "   $ #ffffff��6F   .��5c      �� $ 
Horizontal=�6%   .+�5d        $ AccountOutline=�69   #��[ 6O   �� 5e      $��Administrar Trabajadores��6(   #��[ 6H   5f      5g      	� 5i      -   5        #��[  5i   "   $ #ffffff��6F   .��5j      �� $ 
Horizontal=�6%   .+�5k        $ BellOutline=�69   #��[ 6L   �� 5l      $��	Servicios��6(   #��[ 69   5m      5n      	� 5o      -   5        #��[  5o   "   $ #ffffff��6F   .��5p      �� $ 
Horizontal=�6%   .+�5q        $ GoogleDrive=�69   #��[ 6L   �� 5r      $��Drive��6(   #��[ 65   5s      5t      5u      5v      �� 5y      $ 	0,10,10,0��6   $��1��6.   ��5z      �� 5{      $		 1,1&�6*   $	
 0,0&�6;   .��5|      � $ #045DE9��6'   $ 0Q�67   � 5}      $ #09C6F9��6'   $	 0.7Q�67   5~      ._�5�      �  � RowDefinitions 5�      �� 5�      $��25 �6(   �� 5�      $��35 �6(   �� 5�      $��* �6(   .+�5�      �� -   5         ��Name$ pnlControlBar��5�   !   $��0��5�   !   $ 
Horizontal=�5�   !   $��RightToLeft=�5�   !   
������ $��0,0,5,0q�5�   !   .+�5�      �� -	   5         ��Name$ btnClose��5�   !     controlButton#��[ 5�   !    
 color4# [ 5�   !   .��5�        $ WindowClose=�69     controlButtonIcon#��[ 6L   5�      �� 5�      -
   5        $ btnMaximizar��5�   !     controlButton2#��[ 5�   !   # [ 5�   !   .��5�        $ WindowMaximize=�69   #��[ 6O   5�      �� 5�      -   5        $ btnMinimizar��5�   !   #��[ 5�   !   # [ 5�   !   .��5�        $ WindowMinimize=�69   #��[ 6O   5�      5�      � 5�      $��1��6    5�      �� 5�      �� 5�      .+�5�      �� $��0��6%   $ 
Horizontal=�65   $��Center=�6N   .+�5�        $ ChartPieOutline=�69   $��20��5�   9   $��20��5�   9     titleColor2#��[ 5�   9   $��	35,0,10,0q�5�   9   �� 5�      $��
Dashboards��6(   #��[ 5�   (   $��16(�5�   (   $��
Montserrat)�5�   (   $��Medium"�5�   (   $��Center=�5�   (   5�      �� 5�      $��1��6%   $ 
Horizontal=�5�      $��RightToLeft=�5�      $��Center=�5�      $��0,0,10,0q�5�      .+�5�      �� $��28��6'   
������ 
������ $��Center=�5�      #��[ 5�      ��5�   "   �� 5�   &    �� 
TargetType" ��66   .R�5�   *   � .+�5�   .   d�  d�Name$ ToggleButton��6;   $|�False��5�      
������ 
������  d� 	IsChecked 5�      ��  �� Path$ IsDropDownOpen� �� Mode$ TwoWay=� �� RelativeSource �� *TemplatedParent+.��5�   2   �� $ 
Horizontal=�6=   .+�5�   6     $ ChevronDown=�6R   $��12��5�   -   $��12��5�   -     iconButtonIcon#��[ 5�   -   #��[ 5�   -   5�   3   5�   /   � 5�   .    �Name$ Popup��64   $��Bottom=�5�       � AllowsTransparency$
 True��5�      $|�False��5�      $��Slide=�5�      ��5�      �� $ IsDropDownOpen� �� *TemplatedParent+.��5�   2   �� 
�������$ 4��5�      
�������$��1q�5�      ._�5�   6   �� .+�5�   :   �� -   5        $��Cerrar sesión��5�   D    �� Padding$ 10q�5�       $��12(�5�        � Cursor$
 Hand��5�       
���   �5�   8   5�   7   5�   3   5�   /   5�   +   5�   '   5�      �� 5�      $��username��6(   #��[ 5�      $��
Montserrat)�5�      $��12(�5�      $��Center=�5�      A� 5�      $��35��6&   $��35��62   ���#ffffff$��2��5�      $��	10,0,10,0q�5      5     5     �� 5     $��2��6%   $��0,7,0,0q�62   .��5     �� 
������ $��Hidden=�6@   .��5     � $  ��6#    5  "   �� 5	  &   $��* �67   �� 5
  &   $	��208 �67   �� 5  &   $��* �67    5  "   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5  &   $
��Auto �64   �� 5   &   $
��Auto �64   �� 5!  &   $
��Auto �64   �� 5"  &   $
��Auto �64   �� 5#  &   $
��Auto �64   �� 5$  &   $
��Auto �64   �� 5%  &   $
��Auto �64   .+�5)  "   �� $��0��6-   $��0��6:   .+�5*  &   #   LiveCharts.Wpf.CartesianChart  -   5          Name$( "CartesianChartFacturasPorCategoria��5,  )   $��15,0,0,0q�5+  )   &    LiveCharts.Wpf.Charts.Base.Chart   LegendLocation$ Bottom=�5.  )   ���#FAFAFA
���   �$
��auto��52  )   $	��270��53  )       Series  5-  )   �� *SeriesFacturasPorCategoria+!   AxisX! 55  .      LiveCharts.Wpf.Axis  "   
Foreground" �#2F4F4F#   Title$# Categoría de Servicio��6M   $   Labels$ 6k   �� *LabelsCategorias+%   AxisY% 58  .     " �#2F4F4F$# Cantidad��6L   &   LabelFormatter& 6]   �� *	Formatter+5:  '   �� 5;  &   ���#ffffff$��Facturas por Categoría��6E    	 TextGeneral#��[	 6c   $��Center=�5<      $
��Bold"�6=   $	��0,5q�6O   5=  #   �� 5>  "   $��1��6-   $��0��6:   .+�5?  &     -   5        $% CartesianChartTotalPorProveedor��5?  9   $ Bottom=�5@  =   $
��auto��5A  =   $	��270��6J   $��	15,10,0,0q�5B  =   ���#FAFAFA  5?  b   �� *SeriesTotalPorProveedor+! 5E  .     " �#2F4F4F$# Proveedores��6L   $ 6`   �� *LabelsProveedores+% 5H  .     " �#2F4F4F$# Total Facturado ($)��6L   & 6h   �� *	Formatter+5J  '   �� 5L  &   #��[	 63   ���#ffffff$#��Total Facturado por Proveedor��6m   $��Center=�6�   $
��Bold"�6�   $��0,0,0,0q�6�   5M  #   �� 5N  "   $��0��6-   $��2��6:   $��0,0,10,0q�6J   .+�5O  &     -   5        $&  CartesianChartFacturacionMensual��5O  O   ���#FAFAFA$
��auto��5Q  )   $	��270��5R     $ Bottom=�6     5P  )   �� *SeriesFacturacionMensual+! 5T  .     " �#2F4F4F$# Meses��6L   $ 6Z   �� *LabelsMeses+% 5W  .     " �#2F4F4F$# Facturación ($)��6L   & 6d   �� *	Formatter+5Y  '   �� 5Z  &   $$��Facturación Mensual Acumulada��60   ���#ffffff#��[	 6j   $��Center=�5[      $
��Bold"�6=   $	��0,5q�6O   5\  #   �� 5]  "   $��1��6-   $��2��6:   $��	0,10,10,0q�6J   .+�5^  &     -   5        $( "CartesianChartPromedioPorProveedor��5^  N   ���#FAFAFA$
��auto��5`  )   $	��270��5a     $ Bottom=�6     5_  )   �� *SeriesPromedioPorProveedor+! 5d  .     " �#2F4F4F$# Proveedores��6L   $ 6`   �� *LabelsProveedores+% 5g  .     " �#2F4F4F$# Promedio ($)��6L   & 6a   �� *	Formatter+5i  '   �� 5j  &   ���#ffffff$��Promedio por Proveedor��6E   #��[	 6c   $��Center=�5k      $
��Bold"�6=   $	��0,5q�6O   5l  #   �� 5m  "   $��1��6-   $ Vertical=�6>   $��23��6U   $
��auto��6g   $��Center=�6u   $��1,0,1,0q�6�   ���#E0E0E0.+�5n  &   �� $ Vertical=�61   $��10q�6H   .+�5o  *   �� $��15(�65   $��Flitro por Proveedor:��6C   $��0,0,0,5q�6`   
���   �#��[	 6�   �� 5p  *    
 RoundedComboBox#��[
 63   $	��165��5q     $��0,0,0,20q�6+   ��5p  \   �� *Proveedores+' �� SelectedItem' 6�   �� *FiltroProveedor+5q  +   5r  '   �� 5t  &   $ Vertical=�61   $��10q�6H   .+�5u  *   �� $��15(�66   $��Flitrar por Fecha Inicio:��6D   $��0,0,0,5q�6e   
���   �#��[	 6�   (  "System.Windows.Controls.DatePicker  5v  *     RoundedDatePicker#��[ 65   $��0,0,0,20q�6�   $	��155��6�   (   SelectedDate( 6`   �� *FechaInicio+6�   5w  '   �� 5y  &   $ Vertical=�61   $��10q�6H   .+�5z  *   �� $��Filtrar por Fecha Fin:��64   $��15(�6R   $��
Montserrat)�6`   $��0,0,0,5q�6x   
���   �#��[	 6�     5{  *   #��[ 65   $	��155��6�   ( 6`   �� *
FechaFin+6�   5|  '   �� 5~  &     StyleButtonFor2#��[ 6P   $��Aplicar Filtros��5     $	��120��63   $��40��6?   $��	0,20,0,20q�6K   ��5~  -   �� *FiltrarCommand+5  K   �� 5�  &   -   5        $��Exportar PDF��5�  -   $��
Montserrat)�6D   #��[ 5�     $	��150��5�     $��50��6%   $
��0,10q�61   5�  #   5�     5�     5�     5�     5�     5�     5�     5�     