   M S B A M L   `   `   `  ����I  EdesktopTickets, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:desktopTickets.ViewsdesktopTickets.Views  5   	   .    (desktopTickets.Views.RevisarTicketWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:desktopTickets.Views     5   	     � Title$  Tickets��5   	   $	��450��6   $	��800��6&   ���#FF8C03.��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $	��150��6   $��10q�6'   ��5      �� *EstadosDisponibles+ �� SelectedItem 5      �� *SelectedLineaAviso.Estado+ �� UpdateSourceTrigger$ PropertyChanged=��� 5      $��Detalles del Aviso��6   $
��Bold"�65   $��5q�6G   �� 5      $	��300��6   $��10q�6&   $��Right=�6�   ��62   �� *SelectedLineaAviso.Detalles+$ PropertyChanged=�6�   �� 5      $��Guardar Cambios��6   $��5q�6]   ��63   �� *GuardarCambiosCommand+6]   5      5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns$ False��6p      CanUserAddRows$ False��6�   ��6    �� *LineasAviso+ 6D   �� *SelectedLineaAviso+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$
	 Auto 6H   
   
IsReadOnly$

 True��6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *ID+6U     5      $ Número Línea��6%   $
	 Auto 6[   $

 True��6h    6;   �� *NumeroLinea+6h     5      $ Detalles��6%   $	 * 6T   $

 True��6^    67   �� *
Detalles+6^   5!      5#      5$      