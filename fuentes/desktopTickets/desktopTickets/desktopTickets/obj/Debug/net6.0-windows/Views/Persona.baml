   M S B A M L   `   `   `  ����I  EdesktopTickets, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:desktopTickets.ViewModelsdesktopTickets.ViewModels  5   	   "    desktopTickets.Views.Persona   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:desktopTickets.ViewModels     5   	     � WindowStyle$  SingleBorderWindow=�5   
    � WindowState$ 	Maximized=�5	   	    � Title$ Login��5
   	    � 
ResizeMode$ NoResize=�6    � DataContext 5      0   *desktopTickets.ViewModels.PersonaViewModel  5   
   ��5      �� 5   
    �� 
StartPoint$	 0,0&�6    �� EndPoint$	 0,1&�6/   .��5      �  � Color$ #FF8C03��6    � Offset$ 0Q�6+   � 5      $ #FF8C03��6   $ 1Q�6+   5      .��5      � $��Center=�6   $��Center=�6&   $	��350��6C   
�������	 J� Effect	 5   
   3  -System.Windows.Media.Effects.DropShadowEffect  5      
   ShadowDepth$
 10Q�6      
BlurRadius$ 15Q�60      Opacity$	 0.3Q�6@    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5       $
��Auto �6    � ColumnDefinitions 5#   
   �� 5$      $
��Auto �6   �� 5%      $��* �6   .+�5(   
   �� $��0��6   $��0��6   $��3��6-   $,��&pack://application:,,,/Images/logo.png��5)   
   $	��100��5*   	   $��10q�6   $��Center=�6"   �� 5-   
   $��Usuario:��6   $��1��6#   $��0��60   $��20,20,10,10q�6@   $��SemiBold"�6U   �� 5.   
   $��1��6   $��1��6   $��10,20q�6.    �� CornerRadius$ 5��6=   ���#c2c2c2$��1q�6d   ._�5/      �� $
��10,5q�60   
������ ��6   �� *
Username+6?   50      �� 53   
   $��Contraseña:��6   $��2��6&   $��0��63   $��20,10,10,10q�6C   $��SemiBold"�6X   �� 54   
   $��2��6   $��1��6   $��10q�6.   $ 5��6:   ���#c2c2c2$��1q�6a   ._�55      G� -   5        $��
pbPassword��55      $
��10,5q�6,   
������ 6U   56      �� 59   
   $��3��6   $��1��6   $��10,20q�6.   $ 5��6=   ���#0078D7._�5:      �� $��Iniciar Sesión��6   $��Center=�6O   $
��15,7q�6l   ���#0078D7
�������
������ ��6.   �� *LoginCommand+6�   5;      5=      5>      