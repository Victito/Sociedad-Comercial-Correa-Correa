   M S B A M L   `   `   `  ����O  KSociedadCorreaCorrea, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:prueba.Vistaprueba.Vista  5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5   	       prueba.Vista.LoginVista   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:prueba.Vista     5   	   Kmah;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   ^ ZMahApps.Metro.IconPacks, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.BootstrapIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag	 cMahApps.Metro.IconPacks.BoxIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj
 fMahApps.Metro.IconPacks.CircumIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Codicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Coolicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ac _MahApps.Metro.IconPacks.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Entypo, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.EvaIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak gMahApps.Metro.IconPacks.FeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.FileIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.Fontaudio, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.FontAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Fontisto, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.ForkAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah dMahApps.Metro.IconPacks.GameIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Ionicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.JamIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Lucide, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137am iMahApps.Metro.IconPacks.MaterialDesign, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al hMahApps.Metro.IconPacks.MaterialLight, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj fMahApps.Metro.IconPacks.MemoryIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af bMahApps.Metro.IconPacks.Microns, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ae aMahApps.Metro.IconPacks.Modern, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag  cMahApps.Metro.IconPacks.Octicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al! hMahApps.Metro.IconPacks.PhosphorIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai" eMahApps.Metro.IconPacks.PicolIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137al# hMahApps.Metro.IconPacks.PixelartIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai$ eMahApps.Metro.IconPacks.RadixIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah% dMahApps.Metro.IconPacks.RemixIcon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ai& eMahApps.Metro.IconPacks.RPGAwesome, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj' fMahApps.Metro.IconPacks.SimpleIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag( cMahApps.Metro.IconPacks.Typicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137af) bMahApps.Metro.IconPacks.Unicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137aj* fMahApps.Metro.IconPacks.VaadinIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ak+ gMahApps.Metro.IconPacks.WeatherIcons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ah, dMahApps.Metro.IconPacks.Zondicons, Version=5.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a�	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks&   	 
                        ! " # $ % & ' ( ) * + , 5   
     � Title$  
LoginVista��5   	   $	��550��5   	   $	��800��5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   (  "MahApps.Metro.Controls.MetroWindow   ShowTitleBar$ False��5   	      ShowCloseButton$ False��5   	   
������    AccentColorBrush   	GlowBrush# �   5   	    � 	Resources 5       �� 
TargetType	 �� Property
 �� Value��%�   '��       �� 5   
   " ��6   .�5      �� 	 � ��$
 25��6&   �� 5      	 � ��$
 25��6'   �� 5      	 � ��$
 16(�6)   5      5      .��5       ��  �� CornerRadius$ 12��6   ._�5!   
   �� $ 10��6   $��5q�6#    J� Opacity$	 0.9Q�67   ��5"      �� 5#       �� 
StartPoint$	 0,0&�6&    �� EndPoint$	 1,1&�67   .��5$      �  � Color$ #3393DF��6#    � Offset$ 0Q�63   � 5%      $ #007AFF��6#   $ 1Q�63   5&      ��5)      �� 5*      $	 0,1&�6&   $	 1,0&�67   .��5+      � $ #007AFF��6#   $ 0Q�63   � 5,      $ #3393DF��6#   $ 1Q�63   5-      ._�51      �  � RowDefinitions 53      �� 54      $��30 �6$   �� 56      $
��Auto �6$   .+�5;      � -   5        $��0��5;       � ColumnDefinitions 5<      �� 5=      �� 5>      $��25 �6+   �� 5?      $��25 �6+   �� 5@      $��5 �6+   .+�5C      �� 
�������$��10(�5D       $��
Montserrat)�5E       $��0��5F       $��Center=�5G       $��10,0,0,0q�5H       INICIO DE SESION6D   �� 5J      -   5         ��Name$ btnMinimizar��5J      $��0q�5K      $��-��5L      
�������$��16(�5N      $��
Montserrat)�5O       � Cursor$
 Hand��5P      $��1��5Q      ��5U      �� 5V      " ��6$   .�5W   "   �� 	 � ��
 �#C0C0C06?    �� Triggers 5X   "   P� 5Y   &    P� Property �@J�IsMouseOver P� Value$
 True��6E   . �5Z   *   �� 	 � ��
 �#F0F0F06G   5[   '   5]      ��5`      �� 5a       �� 
TargetType" ��6.   .R�5b   "   �� $��18��6)   $��18��64   $ 9��5c   )   #��z��5d   )   ._�5e   &   �� $��Center=�67   $��Center=�6R   5f   #   5g      5i      �� 5k      -   5        $ 	btnCerrar��5k      $��0q�5l      $��x��5m      
�������$��16(�5o      $��
Montserrat)�5p      $
 Hand��5q      $��2��5r      $��30��6-   $��Center=�69   $��Right=�6T   $��25��6p   ��5v      �� 5w      " ��6$   .�5x   "   �� 	 � ��

 �  ��6?    5y   "   P� 5z   &    �@J�IsMouseOver$
 True��6E   . �5{   *   �� 	 � ��
 �#FFA5006G   5|   '   5~      ��5�      �� 5�      " ��6.   .R�5�   "   �� $��18��6)   $��18��64   $ 9��5�   )   #��z��5�   )   ._�5�   &   �� $��Center=�67   $��Center=�5�   7   5�   #   5�      5�      5�      �� 5�      $	��250��6   $��1��5�       �� Orientation$ Vertical=�5�      $��0,35,0,0q�5�      .+�5�      �� $8��2/Resources/Logo_Final_cliente-removebg-preview.png��6   $	��100��6X   $	��100��6d   $��0,0,0,5q�6q   �� 5�      $��Farmacia��6    
�������$��25(�5�       $��Medium"�5�       $��
Montserrat)�5�       $��Center=�5�       �� 5�      $�� ��6    
�������$��25(�5�       $��Medium"�5�       $��
Montserrat)�5�       $��Center=�5�       $
��Wrap=�5�        �� TextAlignment$ Justify=�5�       $��0,5,0,0q�5�       �� 5�      $��Nombre de usuario��6    
�������$��12(�5�       $��Medium"�5�       $��
Montserrat)�5�       $��0,35,0,0q�5�       �� 5�      �	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks&   	 
                        ! " # $ % & ' ( ) * + , 5�      -   5         ��Name$ 
txtusuario��5�      $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
������� �� 
CaretBrush
 �����
�������$��0,0,0,1q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��30,0,0,0q�5�      ��5�      �� *Nombre+ �� UpdateSourceTrigger$ PropertyChanged=�.��6[   ��5�      �� 5�       �� ImageSource$ \Resources\User.png��6)    j� Stretch$
 None=�6K     j� 
AlignmentX$
  left=�6Z   5�      .��5�   .   5�      �� 5�      $��Contraseña��6    
�������$��12(�5�       $��Medium"�5�       $��
Montserrat)�5�       $��0,15,0,0q�5�       G� 5�      -   5        ! G�Name$! txtContraseña��5�   "   $��13(�5�      $��Medium"�5�      $��
Montserrat)�5�      
�������" G� 
CaretBrush
" �����
�������$��0,0,0,1q�5�      $��28��5�      $��Center=�5�      $��0,5,0,0q�5�      $��30,0,0,0q�5�      ��5�      �� 5�      $ \Resources\Key.png��6)   $
 None=�6J   $
  left=�6Y   5�      �� 5�      -   5        $ btnIngresar��5�      $��0q�5�      $��Ingresar��5�      
�������$��16(�5�      $��
Montserrat)�5�      $
 Hand��5�      $��2��5�      $��0,50,0,0q�5�      ��5�      �� *IniciarSesionCommand+��5�      �� 5�      " ��6$   .�5�   "   �� 	 � ��
 �#40E0D06?    5�   "   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   �� 	 � ��
 �#41CD656G   5�   '   5�      ��5�      �� 5�      " ��6.   .R�5�   "   �� $	��150��6)   $��40��65   $ 20��5�   %   #��z��5�   %   ._�5�   &   �� $��Center=�67   $��Center=�5�   %   5�   #   5�      5�      5�      5�      5�      5�      5�      